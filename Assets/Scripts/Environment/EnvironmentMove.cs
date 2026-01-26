using UnityEngine;

public class EnvironmentMove : MonoBehaviour
{
    public GameConfig config;

    float speed;
    float timer;
    bool isGameOver = false;

    void Start(){
        speed = config.startSpeed;
    }

    void OnEnable(){
        GameEvents.OnGameOver += OnGameOver;
    }

    void OnDisable(){
        GameEvents.OnGameOver -= OnGameOver;
    }

    void Update(){
        if (isGameOver){
            speed = Mathf.Lerp(speed, 0f, Time.deltaTime * 2f);
        }
        else{
            speed = Mathf.Min(speed + config.speedIncrease * Time.deltaTime, config.maxSpeed);
        }
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
    void OnGameOver(){
        isGameOver = true;
    }
}
