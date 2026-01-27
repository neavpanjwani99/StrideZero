using UnityEngine;

public class EnergyDrainSystem : MonoBehaviour
{
    public PlayerHealth playerHealth;

    [Header("Energy Drain Settings")]
    public float initialDelay = 30f;    
    public float drainInterval = 35f;   
    public float drainAmount = 0.5f;    
    float drainPauseTimer = 0f;
public float drinkGraceTime = 6f;


    float timer;
    bool isGameOver = false;
    bool drainStarted = false;

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
    if (drainPauseTimer > 0f)
{
    drainPauseTimer -= Time.deltaTime;
    return;
}

    if (isGameOver || playerHealth == null) return;

    // 🔥 ADD THIS
    if (playerHealth.CurrentHealth <= 0f) return;

    timer += Time.deltaTime;

    if (!drainStarted)
    {
        if (timer >= initialDelay)
        {
            timer = 0f;
            drainStarted = true;
        }
        return;
    }

    if (timer >= drainInterval)
    {
        timer = 0f;
        playerHealth.TakeDamage(drainAmount);
    }
}
public void PauseDrain(float time)
{
    drainPauseTimer = time;
}



    void OnGameOver()
    {
        isGameOver = true;
    }
}
