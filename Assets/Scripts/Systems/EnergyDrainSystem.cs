using UnityEngine;

public class EnergyDrainSystem : MonoBehaviour
{
    public PlayerHealth playerHealth;

    [Header("Drain Settings")]
    public float startDelay = 25f;  // game start -> uske 25 sec baad health drain start hoga
    public float drainInterval = 35f;  // after 25 sec -> har 35 sec me player ko damage hoga  
    public float drainAmount = 0.5f; // 0.5 se demage aaye player ko 

    float baseDrainInterval;
    float timer;
    bool isGameOver = false; // flag check karne ke liye over hua hai ki nahi game 
    bool isPaused = false; // game pause check 
    bool drainEnabled = false; // drain system enable karna hai ki nahi

    // start game ke barabar drain system ko chalu karna hai 
    void Start()
    {
        baseDrainInterval = drainInterval;
        Invoke(nameof(EnableDrain), startDelay);
    }

    // Drain system ko start karne ke liye 
    void EnableDrain()
    {
        drainEnabled = true;
        timer = 0f;
        Debug.Log("Energy drain started"); //check karne ke liye game ke console pe print hona chahiye 
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

    // is method me hum check karenge ki game ka frame kya state hai pause | over | drain enabled hai ki nahi, agar drain enabled hai toh timer ko update karenge aur jab timer drain interval ke barabar ya usse zyada ho jaye toh player ko damage denge aur timer reset kar denge
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

    // Level up hone par drain interval ko adjust karna
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

    // game pause karne pe drain system bhi pause ho jaye 
    System.Collections.IEnumerator PauseRoutine(float duration)
    {
        isPaused = true;
        yield return new WaitForSeconds(duration);
        isPaused = false;
    }
}
