using UnityEngine;
using TMPro;

public class CoinUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;

    int currentCoins = 0;

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

    void Start()
    {
        UpdateUI();
    }

    void OnCoinCollected(int amount)
    {
        currentCoins += amount;
        UpdateUI();
    }

    void OnGameOver()
    {
        // freeze UI, nothing to update
    }

    void UpdateUI()
    {
        coinText.text = "Coins: " + currentCoins;
    }
}
