using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;
    public AudioClip coinSound;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Player")
                        .GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEvents.OnCoinCollected?.Invoke(coinValue);

            if (audioSource != null && coinSound != null)
            {
                audioSource.PlayOneShot(coinSound);
            }

            gameObject.SetActive(false);
        }
    }
}