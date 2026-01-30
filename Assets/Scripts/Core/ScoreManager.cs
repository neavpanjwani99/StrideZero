using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public float distanceScoreMultiplier = 1f; // tuning ke liye
    int coinScore = 0;
    float distanceScore = 0f;
    bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void OnEnable()
    {
        GameEvents.OnCoinCollected += OnCoinCollected;
        GameEvents.OnGameOver += OnGameOver;
    }

    void OnDisable()
    {
        GameEvents.OnCoinCollected -= OnCoinCollected;
        GameEvents.OnGameOver -= OnGameOver;
    }

    void Update()
    {
        if (isGameOver) return;

        distanceScore += Time.deltaTime * distanceScoreMultiplier;
    }

    void OnCoinCollected(int amount)
    {
        coinScore += amount;
    }

    void OnGameOver()
    {
        isGameOver = true;

        int finalScore = GetFinalScore();
        Debug.Log("Coins: " + coinScore);
        Debug.Log("Distance Score: " + Mathf.FloorToInt(distanceScore));
        Debug.Log("FINAL SCORE: " + finalScore);
    }

    public int GetFinalScore()
    {
        return Mathf.FloorToInt(distanceScore) + (coinScore * 10);
    }
}
