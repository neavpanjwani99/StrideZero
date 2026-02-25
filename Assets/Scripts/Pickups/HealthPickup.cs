using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healAmount = 0.5f;
    public float drainPauseTime = 6f;
    public AudioClip healthSound;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Player")
                        .GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health == null) return;

        health.Heal(healAmount);

        EnergyDrainSystem drain = FindFirstObjectByType<EnergyDrainSystem>();
        if (drain != null)
            drain.PauseDrain(drainPauseTime);

        if (audioSource != null && healthSound != null)
        {
            audioSource.PlayOneShot(healthSound);
        }

        gameObject.SetActive(false);
    }
}