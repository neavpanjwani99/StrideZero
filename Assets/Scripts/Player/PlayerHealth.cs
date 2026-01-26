using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public float hitCooldown = 0.6f;

    int currentHealth;
    bool isDead = false;
    bool canTakeDamage = true;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead || !canTakeDamage) return;

        currentHealth -= damage;
        Debug.Log("Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            return;
        }
        GameEvents.OnPlayerHit?.Invoke();
        StartCoroutine(DamageCooldown());
    }

    IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(hitCooldown);
        canTakeDamage = true;
    }

    void Die()
    {
        isDead = true;
        GameEvents.OnGameOver?.Invoke();
    }
}
