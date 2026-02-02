using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int totalCoins = 0;

    public void AddCoin(int amount)
    {
        totalCoins += amount;
        Debug.Log("TOTAL COINS: " + totalCoins); 
    }

    void OnEnable()
{
    GameEvents.OnCoinCollected += AddCoin;
}

void OnDisable()
{
    GameEvents.OnCoinCollected -= AddCoin;
}

}
