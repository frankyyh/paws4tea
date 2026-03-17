using UnityEngine;

public class DogController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 2f;
    
    [Header("Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite happySprite; // 成功敬茶后的sprite
    [SerializeField] private Sprite angrySprite; // 生气时的sprite
    
    private SpriteRenderer spriteRenderer;
    private bool isHappy = false;
    private bool isAngry = false;
    private bool hasEnteredZone = false; // 是否进入过检测区域

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        
        if (normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    private void Update()
    {
        // 检查游戏是否结束，如果结束则停止移动
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager != null && gameManager.IsGameOver())
        {
            return; // 游戏结束，停止移动
        }

        // 从右向左移动
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        
        // 如果移出屏幕左侧，销毁对象
        if (transform.position.x < -15f)
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
        if (!isHappy && !isAngry && happySprite != null)
        {
            isHappy = true;
            spriteRenderer.sprite = happySprite;
        }
    }

    public void MakeAngry()
    {
        if (!isHappy && !isAngry && angrySprite != null)
        {
            isAngry = true;
            spriteRenderer.sprite = angrySprite;
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
}
