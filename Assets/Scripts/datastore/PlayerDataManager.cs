using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Debug.Log("PlayerDataManager STARTED");
    }

    // Optional helper (safe read)
    public string GetPlayerName()
    {
        return PlayerPrefs.GetString("PlayerName", "Player");
    }
}