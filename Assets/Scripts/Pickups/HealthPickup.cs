using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healAmount = 0.5f;
    public float drainPauseTime = 6f; 

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.Heal(healAmount);

            
            EnergyDrainSystem drain = FindObjectOfType<EnergyDrainSystem>();
            if (drain != null)
                drain.PauseDrain(drainPauseTime);

            Destroy(gameObject);
        }
    }
}
