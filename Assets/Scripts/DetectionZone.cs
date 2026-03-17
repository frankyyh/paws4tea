using UnityEngine;
using System.Collections.Generic;

public class DetectionZone : MonoBehaviour
{
    [Header("Zone Settings")]
    [SerializeField] private float zoneWidth = 2f;
    [SerializeField] private float zoneHeight = 2f;
    [SerializeField] private bool autoPositionAtStart = false; // 是否在开始时自动定位到屏幕中间
    
    private List<DogController> dogsInZone = new List<DogController>();
    private List<DogController> dogsThatEnteredZone = new List<DogController>(); // 记录进入过区域的狗狗
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        
        // 如果启用自动定位，则设置检测区域在屏幕中间（上半部分）
        if (autoPositionAtStart)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                // 计算屏幕中间偏上的位置（上半部分）
                float centerX = Screen.width / 2f;
                float centerY = Screen.height * 0.75f; // 屏幕上半部分的75%位置
                
                Vector3 centerPosition = mainCamera.ScreenToWorldPoint(new Vector3(centerX, centerY, mainCamera.nearClipPlane + 10f));
                centerPosition.z = 0f;
                transform.position = centerPosition;
            }
        }
        // 如果不启用自动定位，使用GameObject的Transform位置（可以在Inspector中手动设置）
    }

    private void Update()
    {
        // 检查是否在游戏进行中（Title/End 时停止检测）
        if (gameManager != null && !gameManager.IsGameActive())
        {
            return;
        }

        // 检查哪些狗狗在区域内
        CheckDogsInZone();
    }

    private void CheckDogsInZone()
    {
        // 获取所有狗狗（不需要排序，使用None模式更快）
        DogController[] allDogs = FindObjectsByType<DogController>(FindObjectsSortMode.None);
        List<DogController> currentDogsInZone = new List<DogController>();

        foreach (DogController dog in allDogs)
        {
            if (IsDogInZone(dog.transform.position))
            {
                if (!dogsInZone.Contains(dog))
                {
                    // 狗狗刚进入区域
                    dog.SetHasEnteredZone(true);
                    if (!dogsThatEnteredZone.Contains(dog))
                    {
                        dogsThatEnteredZone.Add(dog);
                    }
                }
                currentDogsInZone.Add(dog);
            }
            else
            {
                // 检查狗狗是否刚离开区域
                if (dogsInZone.Contains(dog) && dog.HasEnteredZone() && !dog.IsHappy())
                {
                    // 狗狗离开了区域但没有被敬茶，让它生气
                    dog.MakeAngry();
                    if (gameManager != null)
                    {
                        gameManager.OnDogMissed();
                    }
                }
            }
        }

        dogsInZone = currentDogsInZone;
    }

    private bool IsDogInZone(Vector3 position)
    {
        Vector3 zonePos = transform.position;
        float halfWidth = zoneWidth / 2f;
        float halfHeight = zoneHeight / 2f;

        return position.x >= zonePos.x - halfWidth &&
               position.x <= zonePos.x + halfWidth &&
               position.y >= zonePos.y - halfHeight &&
               position.y <= zonePos.y + halfHeight;
    }

    public bool HasDogsInZone()
    {
        return dogsInZone.Count > 0;
    }

    public List<DogController> GetDogsInZone()
    {
        return new List<DogController>(dogsInZone);
    }

    private void OnDrawGizmos()
    {
        // 在Scene视图中显示检测区域
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(zoneWidth, zoneHeight, 0.1f));
    }
}
