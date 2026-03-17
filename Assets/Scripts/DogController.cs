using UnityEngine;

public class DogController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 2f;
    
    [Header("Dog Sprite Sets")]
    [SerializeField] private DogSpriteSet[] dogSpriteSets; // 多套不同肤色的sprite
    
    [Header("Audio")]
    [SerializeField] private AudioClip happySoundEffect; // 成功喝茶时的音效
    
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private DogSpriteSet currentSpriteSet; // 当前使用的sprite套
    private bool isHappy = false;
    private bool isAngry = false;
    private bool hasEnteredZone = false; // 是否进入过检测区域
    
    // 移动和转折相关
    private Vector3 moveDirection = Vector3.left; // 当前移动方向（初始从右向左）
    private float turnPointX = float.MaxValue; // 转折点X坐标
    private Vector3 turnDirection = Vector3.zero; // 转折后的方向
    private bool hasTurned = false; // 是否已经转折

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        
        // 获取或添加AudioSource组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // 随机选择一套sprite
        SelectRandomSpriteSet();
    }

    private void SelectRandomSpriteSet()
    {
        if (dogSpriteSets == null || dogSpriteSets.Length == 0)
        {
            Debug.LogWarning("DogSpriteSets数组为空！请在Inspector中分配sprite套组。");
            return;
        }

        // 随机选择一套sprite
        int randomIndex = Random.Range(0, dogSpriteSets.Length);
        currentSpriteSet = dogSpriteSets[randomIndex];
        
        // 设置初始sprite
        if (currentSpriteSet != null && currentSpriteSet.normalSprite != null)
        {
            spriteRenderer.sprite = currentSpriteSet.normalSprite;
        }
        else
        {
            Debug.LogWarning($"DogSpriteSet[{randomIndex}]的normalSprite为空！");
        }
    }

    private void Update()
    {
        // 检查是否在游戏进行中（Title/End/GameOver 时停止移动）
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager != null && !gameManager.IsGameActive())
        {
            return;
        }

        // 检查是否到达转折点
        if (!hasTurned && turnDirection != Vector3.zero && transform.position.x <= turnPointX)
        {
            // 到达转折点，改变方向
            moveDirection = turnDirection;
            hasTurned = true;
        }

        // 按照当前方向移动
        transform.Translate(moveDirection * speed * Time.deltaTime);
        
        // 如果移出屏幕，销毁对象（根据移动方向判断）
        if (moveDirection.x < 0 && transform.position.x < -15f) // 向左移出
        {
            Destroy(gameObject);
        }
        else if (moveDirection.x > 0 && transform.position.x > 15f) // 向右移出
        {
            Destroy(gameObject);
        }
        else if (moveDirection.y < 0 && transform.position.y < -15f) // 向下移出
        {
            Destroy(gameObject);
        }
        else if (moveDirection.y > 0 && transform.position.y > 15f) // 向上移出
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void MakeHappy()
    {
        if (!isHappy && !isAngry && currentSpriteSet != null && currentSpriteSet.happySprite != null)
        {
            isHappy = true;
            spriteRenderer.sprite = currentSpriteSet.happySprite;
            
            // 播放成功喝茶的音效
            if (audioSource != null && happySoundEffect != null)
            {
                audioSource.PlayOneShot(happySoundEffect);
            }
        }
    }

    public void MakeAngry()
    {
        if (!isHappy && !isAngry && currentSpriteSet != null && currentSpriteSet.angrySprite != null)
        {
            isAngry = true;
            spriteRenderer.sprite = currentSpriteSet.angrySprite;
        }
    }

    public bool IsHappy()
    {
        return isHappy;
    }

    public bool IsAngry()
    {
        return isAngry;
    }

    public void SetHasEnteredZone(bool entered)
    {
        hasEnteredZone = entered;
    }

    public bool HasEnteredZone()
    {
        return hasEnteredZone;
    }

    public void SetTurnPoint(float turnX, Vector3 newDirection)
    {
        turnPointX = turnX;
        turnDirection = newDirection;
        hasTurned = false;
    }
}
