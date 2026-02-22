using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject panel;              
    public GameObject leaderboardPanel;   
    public GameObject gameOverPanel;      

    [Header("Texts")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;

    public void Show(int score, int coins)
    {
        panel.SetActive(true);
        scoreText.text = "Score: " + score;
        coinsText.text = "Coins: " + coins;
        Time.timeScale = 0f;
    }

    public void OpenLeaderboard()
    {
        gameOverPanel.SetActive(false);
        leaderboardPanel.SetActive(true);
    }

    public void CloseLeaderboard()
    {
        leaderboardPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}