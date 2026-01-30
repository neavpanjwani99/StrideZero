using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEvents.OnCoinCollected?.Invoke(coinValue);
            Debug.Log("+ " + coinValue + " COIN");
            Destroy(gameObject);
        }
    }
}