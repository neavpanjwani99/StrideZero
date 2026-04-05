using UnityEngine;
/*
 hardcoded scoring system nahi hai means isme hum distance per second ke hissab se calculate kar rahe hai score, 
 isliye humne scorePerSecond variable banaya hai jise hum inspector se adjust kar sakte hain, aur game over hone 
 par final distance score ko print karenge
*/
public class DistanceScoreSystem : MonoBehaviour
{
    public float scorePerSecond = 1.2f; // 1.2 frames per seconds ke hissab se socre aayega 

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
