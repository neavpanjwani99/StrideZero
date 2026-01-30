using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    public int currentLevel = 1;
    public float levelDuration = 60f; // 1 level = 60 sec

    float timer;
    bool isGameOver = false;

    void OnEnable()
    {
        GameEvents.OnGameOver += OnGameOver;
    }

    void OnDisable()
    {
        GameEvents.OnGameOver -= OnGameOver;
    }

    void Update()
    {
        if (isGameOver) return;

        timer += Time.deltaTime;

        if (timer >= levelDuration)
        {
            timer = 0f;
            currentLevel++;

            Debug.Log("LEVEL UP → " + currentLevel);
            GameEvents.OnLevelUp?.Invoke(currentLevel);
        }
    }

    void OnGameOver()
    {
        isGameOver = true;
    }
}
