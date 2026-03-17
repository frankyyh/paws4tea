using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private int maxMissedDogs = 3; // 最多可以错过的狗狗数量
    
    private DetectionZone detectionZone;
    private UIManager uiManager;
    private BGMPlayer bgmPlayer;
    private ScreenManager screenManager;
    private static GameManager instance;
    
    private int successfulBows = 0; // 成功敬茶的数量
    private int missedDogs = 0; // 错过的狗狗数量
    private bool isGameOver = false;
    private bool isGameActive = false; // Title/End时为false

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        detectionZone = FindFirstObjectByType<DetectionZone>();
        if (detectionZone == null)
        {
            Debug.LogError("DetectionZone not found in scene!");
        }
        
        uiManager = FindFirstObjectByType<UIManager>();
        if (uiManager != null)
        {
            uiManager.UpdateScore(successfulBows, missedDogs, maxMissedDogs);
        }
        
        bgmPlayer = FindFirstObjectByType<BGMPlayer>();
        screenManager = FindFirstObjectByType<ScreenManager>();
    }

    public void OnPlayerBow()
    {
        if (!IsGameActive() || detectionZone == null) return;

        // 检查是否有狗狗在检测区域内
        if (detectionZone.HasDogsInZone())
        {
            List<DogController> dogsInZone = detectionZone.GetDogsInZone();
            
            // 让所有在区域内的狗狗变开心
            foreach (DogController dog in dogsInZone)
            {
                if (!dog.IsHappy() && !dog.IsAngry())
                {
                    dog.MakeHappy();
                    successfulBows++;
                    
                    if (uiManager != null)
                    {
                        uiManager.UpdateScore(successfulBows, missedDogs, maxMissedDogs);
                    }
                    
                    Debug.Log($"成功对狗狗敬茶！总数: {successfulBows}");
                }
            }
        }
    }

    public void OnDogMissed()
    {
        if (!IsGameActive()) return;
        
        missedDogs++;
        
        if (uiManager != null)
        {
            uiManager.UpdateScore(successfulBows, missedDogs, maxMissedDogs);
        }
        
        Debug.Log($"错过了一只狗狗！错过数: {missedDogs}/{maxMissedDogs}");
        
        // 检查是否游戏结束
        if (missedDogs >= maxMissedDogs)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        isGameOver = true;
        isGameActive = false;
        
        // 停止生成新的狗狗
        DogSpawner spawner = FindFirstObjectByType<DogSpawner>();
        if (spawner != null)
        {
            spawner.enabled = false;
        }
        
        // 停止BGM
        if (bgmPlayer != null)
        {
            bgmPlayer.StopBGM();
        }

        // 切换到结束画面（图片）
        if (screenManager != null)
        {
            screenManager.ShowEndScreen();
        }
        
        Debug.Log($"游戏结束！你成功给 {successfulBows} 只狗狗敬茶了！");
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public bool IsGameActive()
    {
        return isGameActive && !isGameOver;
    }

    // 获取最终分数（用于End Screen显示）
    public int GetFinalScore()
    {
        return successfulBows;
    }

    // Title 按空格开始时调用
    public void StartNewRun()
    {
        isGameOver = false;
        isGameActive = true;
        successfulBows = 0;
        missedDogs = 0;

        // 重新启用生成器
        DogSpawner spawner = FindFirstObjectByType<DogSpawner>();
        if (spawner != null)
        {
            spawner.enabled = true;
        }

        if (uiManager != null)
        {
            uiManager.UpdateScore(successfulBows, missedDogs, maxMissedDogs);
        }
    }

    // 任何时候按 R 回到 Title 时调用
    public void ResetToTitle()
    {
        isGameActive = false;
        isGameOver = false;
        successfulBows = 0;
        missedDogs = 0;

        // 停止生成器（防止在Title时误生成）
        DogSpawner spawner = FindFirstObjectByType<DogSpawner>();
        if (spawner != null)
        {
            spawner.enabled = false;
        }

        if (uiManager != null)
        {
            uiManager.UpdateScore(successfulBows, missedDogs, maxMissedDogs);
        }
    }
}
