using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Coin Collected");
            GameEvents.OnCoinCollected?.Invoke(coinValue);
            Debug.Log("+ " + coinValue + " COIN");
            gameObject.SetActive(false);
        }
    }
}