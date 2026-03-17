using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite bowingSprite; // 鞠躬敬茶的sprite
    
    [Header("Animation")]
    [SerializeField] private float bowDuration = 0.5f;
    
    [Header("Audio")]
    [SerializeField] private AudioClip bowSoundEffect; // 按空格时的音效
    
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool isBowing = false;
    private float bowTimer = 0f;
    private GameManager gameManager;

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
        
        if (normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        // 检查是否在游戏进行中（Title/End 时不接受输入）
        if (gameManager != null && !gameManager.IsGameActive())
        {
            return;
        }

        // 处理鞠躬动画
        if (isBowing)
        {
            bowTimer -= Time.deltaTime;
            if (bowTimer <= 0f)
            {
                isBowing = false;
                if (normalSprite != null)
                {
                    spriteRenderer.sprite = normalSprite;
                }
            }
        }

        // 检测空格键输入（使用新的Input System）
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            PerformBow();
        }
    }

    private void PerformBow()
    {
        if (isBowing) return; // 如果正在鞠躬，忽略输入

        // 播放音效
        if (audioSource != null && bowSoundEffect != null)
        {
            audioSource.PlayOneShot(bowSoundEffect);
        }

        // 切换到鞠躬sprite
        if (bowingSprite != null)
        {
            spriteRenderer.sprite = bowingSprite;
        }
        
        isBowing = true;
        bowTimer = bowDuration;

        // 通知GameManager玩家按了空格
        if (gameManager != null)
        {
            gameManager.OnPlayerBow();
        }
    }
}
