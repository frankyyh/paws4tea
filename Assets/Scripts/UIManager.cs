using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text missedText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text finalScoreText;

    private void Start()
    {
        // 如果没有分配UI元素，尝试自动查找
        if (scoreText == null)
        {
            GameObject scoreObj = GameObject.Find("ScoreText");
            if (scoreObj != null) scoreText = scoreObj.GetComponent<Text>();
        }
        
        if (missedText == null)
        {
            GameObject missedObj = GameObject.Find("MissedText");
            if (missedObj != null) missedText = missedObj.GetComponent<Text>();
        }
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void UpdateScore(int successfulBows, int missedDogs, int maxMissedDogs)
    {
        if (scoreText != null)
        {
            scoreText.text = "成功敬茶: " + successfulBows;
        }
        
        if (missedText != null)
        {
            missedText.text = "错过: " + missedDogs + "/" + maxMissedDogs;
        }
    }

    public void ShowGameOver(int finalScore)
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        
        if (gameOverText != null)
        {
            gameOverText.text = "游戏结束！";
        }
        
        if (finalScoreText != null)
        {
            finalScoreText.text = "你一共给 " + finalScore + " 只狗狗成功敬茶了！";
        }
    }
}
