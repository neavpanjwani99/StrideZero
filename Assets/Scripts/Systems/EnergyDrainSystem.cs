using UnityEngine;

public class EnergyDrainSystem : MonoBehaviour
{
    public PlayerHealth playerHealth;

    [Header("Drain Settings")]
    public float startDelay = 25f;      
    public float drainInterval = 35f;   
    public float drainAmount = 0.5f;

    float baseDrainInterval;
    float timer;

    bool isGameOver = false;
    bool isPaused = false;
    bool drainEnabled = false;

    void Start()
    {
        baseDrainInterval = drainInterval;

       
        Invoke(nameof(EnableDrain), startDelay);
    }

    void EnableDrain()
    {
        drainEnabled = true;
        timer = 0f;
        Debug.Log("Energy drain started");
    }

    void OnEnable()
    {
        GameEvents.OnGameOver += OnGameOver;
        GameEvents.OnLevelUp += OnLevelUp;
    }

    void OnDisable()
    {
        GameEvents.OnGameOver -= OnGameOver;
        GameEvents.OnLevelUp -= OnLevelUp;
    }

    void Update()
    {
        if (isGameOver || isPaused || !drainEnabled || playerHealth == null)
            return;

        timer += Time.deltaTime;

        if (timer >= drainInterval)
        {
            timer = 0f;
            playerHealth.TakeDamage(drainAmount);
        }
    }

    void OnLevelUp(int level)
    {
        drainInterval = Mathf.Max(
            15f,
            baseDrainInterval - (level - 1) * 3f
        );

        Debug.Log("Drain Interval Now: " + drainInterval);
    }

    void OnGameOver()
    {
        isGameOver = true;
    }

    public void PauseDrain(float duration)
    {
        if (!gameObject.activeInHierarchy) return;
        StartCoroutine(PauseRoutine(duration));
    }

    System.Collections.IEnumerator PauseRoutine(float duration)
    {
        isPaused = true;
        yield return new WaitForSeconds(duration);
        isPaused = false;
    }
}
