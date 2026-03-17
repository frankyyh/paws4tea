using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private int maxMissedDogs = 3; // 最多可以错过的狗狗数量
    
    private DetectionZone detectionZone;
    private UIManager uiManager;
    private static GameManager instance;
    
    private int successfulBows = 0; // 成功敬茶的数量
    private int missedDogs = 0; // 错过的狗狗数量
    private bool isGameOver = false;

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
    }

    public void OnPlayerBow()
    {
        if (isGameOver || detectionZone == null) return;

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
        if (isGameOver) return;
        
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
        
        // 停止生成新的狗狗
        DogSpawner spawner = FindFirstObjectByType<DogSpawner>();
        if (spawner != null)
        {
            spawner.enabled = false;
        }
        
        if (uiManager != null)
        {
            uiManager.ShowGameOver(successfulBows);
        }
        
        Debug.Log($"游戏结束！你成功给 {successfulBows} 只狗狗敬茶了！");
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
