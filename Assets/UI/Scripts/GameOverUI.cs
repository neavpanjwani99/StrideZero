using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;

    public void Show(int score, int coins)
    {
        panel.SetActive(true);
        scoreText.text = "Score: " + score;
        coinsText.text = "Coins: " + coins;
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
