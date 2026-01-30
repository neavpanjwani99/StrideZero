using UnityEngine;

public class DistanceScoreSystem : MonoBehaviour
{
    public float scorePerSecond = 1.2f;

    float score;
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

        score += scorePerSecond * Time.deltaTime;

        
        if (Mathf.FloorToInt(score) % 10 == 0)
        {
            Debug.Log("Distance Score: " + Mathf.FloorToInt(score));
        }
    }

    void OnGameOver()
    {
        isGameOver = true;
        Debug.Log("FINAL DISTANCE SCORE: " + Mathf.FloorToInt(score));
    }
}
