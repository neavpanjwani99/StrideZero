using UnityEngine;

public class EnvironmentMove : MonoBehaviour
{
    public GameConfig config;

    float speed;
    float elapsedTime;
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
        if (isGameOver)
        {
            speed = Mathf.Lerp(speed, 0f, Time.deltaTime * 2f);
        }
        else
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / 90f); // 0 → 1 in 90 sec
            float curveMultiplier = Mathf.Lerp(0.5f, 2.2f, t * t);

            speed += config.speedIncrease * curveMultiplier * Time.deltaTime;
            speed = Mathf.Min(speed, config.maxSpeed);
        }

        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    void OnGameOver()
    {
        isGameOver = true;
    }
}
