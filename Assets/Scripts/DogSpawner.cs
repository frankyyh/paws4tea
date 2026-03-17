using UnityEngine;

public class DogSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject dogPrefab;
    [SerializeField] private float minSpawnInterval = 1f; // 最小生成间隔
    [SerializeField] private float maxSpawnInterval = 3f; // 最大生成间隔
    [SerializeField] private float initialSpeed = 2f;
    [SerializeField] private float speedIncreasePerDog = 0.2f;
    [SerializeField] private float maxSpeed = 10f;
    
    [Header("Spawn Position")]
    [SerializeField] private float spawnX = 10f; // 屏幕右侧
    [SerializeField] private float spawnY = 2f; // 屏幕上半边
    
    private float currentSpeed;
    private float nextSpawnTime;
    private int dogCount = 0;

    private void Start()
    {
        currentSpeed = initialSpeed;
        // 初始生成时间使用随机间隔
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    private void Update()
    {
        // 检查游戏是否结束
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager != null && gameManager.IsGameOver())
        {
            return; // 游戏结束，停止生成
        }

        if (Time.time >= nextSpawnTime)
        {
            SpawnDog();
            // 使用随机间隔生成下一只狗狗
            nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    private void SpawnDog()
    {
        if (dogPrefab == null)
        {
            Debug.LogWarning("Dog Prefab is not assigned!");
            return;
        }

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        GameObject newDog = Instantiate(dogPrefab, spawnPosition, Quaternion.identity);
        
        // 设置狗狗的速度
        DogController dogController = newDog.GetComponent<DogController>();
        if (dogController != null)
        {
            dogController.SetSpeed(currentSpeed);
        }

        // 增加速度（但不超过最大速度）
        currentSpeed = Mathf.Min(currentSpeed + speedIncreasePerDog, maxSpeed);
        dogCount++;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
