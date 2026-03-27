using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    bool isGameOver = false;
    public GameOverUI gameOverUI;

    public bool IsGameOver => isGameOver;

    void Start()
    {
        // GAME START SIGNAL dega jaise hi game start hoga (game manager ke start hone pe)
        StartCoroutine(FireGameStart());
    }

    IEnumerator FireGameStart()
    {
        yield return null;
        GameEvents.OnGameStart?.Invoke();
    }

    void OnEnable()
    {
        GameEvents.OnGameOver += HandleGameOver;
    }

    void OnDisable()
    {
        GameEvents.OnGameOver -= HandleGameOver;
    }

    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    void HandleGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f;

        int finalScore = ScoreManager.Instance.GetFinalScore();
        int finalCoins = FindObjectOfType<CoinManager>().totalCoins;
float survivalTime = Time.timeSinceLevelLoad;

RunData run = new RunData(
    PlayerPrefs.GetString("PlayerName", "Player"),
    finalScore,
    finalCoins,
    survivalTime
);

LeaderboardManager.Instance.AddRun(run);
        gameOverUI.Show(finalScore, finalCoins);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMenu()
{
    Time.timeScale = 1f;
    SceneManager.LoadScene("SplashScene");
}
}
