using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ScreenManager : MonoBehaviour
{
    public enum ScreenState
    {
        Title,
        Playing,
        End
    }

    [Header("Screen Objects (UI)")]
    [SerializeField] private GameObject titleScreen; // 一张图片的GameObject（建议是UI Image在Canvas里）
    [SerializeField] private GameObject endScreen;   // 一张图片的GameObject（建议是UI Image在Canvas里）

    [Header("End Screen Score Display")]
    [SerializeField] private TextMeshProUGUI endScreenScoreText; // 显示"You served X samurai dogs"的TMP文本组件

    [Header("Optional UI/HUD Root")]
    [SerializeField] private GameObject hudRoot;     // 计分/错过等HUD的根节点（可选）

    private GameManager gameManager;
    private BGMPlayer bgmPlayer;
    private ScreenState state = ScreenState.Title;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        bgmPlayer = FindFirstObjectByType<BGMPlayer>();
    }

    private void Start()
    {
        GoToTitle();
    }

    private void Update()
    {
        // 任何时候按 R：回到 Title Screen（重新开始）
        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            GoToTitle();
            return;
        }

        // Title Screen 按空格开始
        if (state == ScreenState.Title &&
            Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        state = ScreenState.Playing;

        SetScreenObjects(title: false, end: false, hud: true);

        if (gameManager != null)
        {
            gameManager.StartNewRun();
        }

        if (bgmPlayer != null)
        {
            bgmPlayer.PlayBGM();
        }

        Time.timeScale = 1f;
    }

    public void GoToTitle()
    {
        state = ScreenState.Title;

        // 停止时间与音乐
        Time.timeScale = 0f;
        if (bgmPlayer != null) bgmPlayer.StopBGM();

        // 清理场上狗狗
        ClearAllDogs();

        // 重置游戏计数/状态
        if (gameManager != null)
        {
            gameManager.ResetToTitle();
        }

        SetScreenObjects(title: true, end: false, hud: false);
    }

    public void ShowEndScreen()
    {
        state = ScreenState.End;

        Time.timeScale = 0f;
        if (bgmPlayer != null) bgmPlayer.StopBGM();

        // 显示最终分数
        if (gameManager != null && endScreenScoreText != null)
        {
            int finalScore = gameManager.GetFinalScore();
            endScreenScoreText.text = "You served " + finalScore + " samurai dogs";
        }

        SetScreenObjects(title: false, end: true, hud: true);
    }

    private void SetScreenObjects(bool title, bool end, bool hud)
    {
        if (titleScreen != null) titleScreen.SetActive(title);
        if (endScreen != null) endScreen.SetActive(end);
        if (hudRoot != null) hudRoot.SetActive(hud);
    }

    private void ClearAllDogs()
    {
        DogController[] dogs = FindObjectsByType<DogController>(FindObjectsSortMode.None);
        foreach (DogController dog in dogs)
        {
            if (dog != null)
            {
                Destroy(dog.gameObject);
            }
        }
    }
}

