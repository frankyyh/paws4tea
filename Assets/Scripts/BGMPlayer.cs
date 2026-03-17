using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [Header("BGM Settings")]
    [SerializeField] private AudioClip bgmClip; // 背景音乐
    
    private AudioSource audioSource;
    private static BGMPlayer instance;

    private void Awake()
    {
        // 单例模式，确保只有一个BGM播放器
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 可选：如果切换场景时保持BGM
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 获取或添加AudioSource组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 设置AudioSource属性
        audioSource.loop = true; // 循环播放
        audioSource.playOnAwake = false; // 不自动播放，等游戏开始
    }

    private void Start()
    {
        // 不在这里自动播放：由 ScreenManager 在“开始游戏”时调用 PlayBGM()
    }

    public void PlayBGM()
    {
        if (audioSource != null && bgmClip != null)
        {
            audioSource.clip = bgmClip;
            audioSource.Play();
        }
        else if (bgmClip == null)
        {
            Debug.LogWarning("BGM Clip未分配！请在Inspector中分配背景音乐。");
        }
    }

    public void StopBGM()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public bool IsPlaying()
    {
        return audioSource != null && audioSource.isPlaying;
    }
}
