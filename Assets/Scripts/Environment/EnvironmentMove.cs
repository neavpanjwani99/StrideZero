using UnityEngine;

public class EnvironmentMove : MonoBehaviour
{
    public GameConfig config;

    float speed;
    float timer;
    bool isGameOver = false;

    void Start()
    {
        speed = config.startSpeed;
    }

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

        transform.Translate(Vector3.back * speed * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer >= 5f && speed < config.maxSpeed)
        {
            timer = 0f;
            speed += config.speedIncrease;
        }
    }

    void OnGameOver()
    {
        isGameOver = true;
    }
}
