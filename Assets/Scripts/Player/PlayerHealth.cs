using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 4f;          
    public float hitCooldown = 0.6f;
public float CurrentHealth => currentHealth;

    float currentHealth;
    bool isDead = false;
    bool canTakeDamage = true;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
{
    if (isDead || !canTakeDamage) return;

    currentHealth -= damage;
    currentHealth = Mathf.Max(currentHealth, 0f); // 🔥 CLAMP FIRST

    Debug.Log("Health: " + currentHealth);

    if (currentHealth <= 0f)
    {
        Die();
        return;
    }

    GameEvents.OnPlayerHit?.Invoke();
    StartCoroutine(DamageCooldown());
}


    public void Heal(float amount)
    {
        if (isDead) return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log("Healed. Health: " + currentHealth);
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
