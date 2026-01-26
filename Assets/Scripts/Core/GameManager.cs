using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool isGameOver = false;

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
        Debug.Log("GAME OVER");
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
