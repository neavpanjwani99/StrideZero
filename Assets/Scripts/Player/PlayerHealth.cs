using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float lowHealthThreshold = 1f;
    public HeartAnimator heartAnimator;

    public float maxHealth = 4f;
    public float hitCooldown = 0.6f;

    public float CurrentHealth => currentHealth;

    float currentHealth;
    bool isDead = false;
    bool canTakeDamage = true;

    HealthUIController healthUI;

    void Awake()
    {
        currentHealth = maxHealth;

        healthUI = FindObjectOfType<HealthUIController>();
        if (healthUI != null)
            healthUI.UpdateHealth(currentHealth);
    }

    public void TakeDamage(float damage)
    {
        if (isDead || !canTakeDamage) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0f);

        if (healthUI != null)
            healthUI.UpdateHealth(currentHealth);

        if (heartAnimator != null)
            heartAnimator.AnimateDecrease();

        if (currentHealth <= 0f)
        {
            Die();
            return;
        }

        StartCoroutine(ApplyIdleAfterDelay());
        GameEvents.OnPlayerHit?.Invoke();
        StartCoroutine(DamageCooldown());
    }

    public void Heal(float amount)
    {
        if (isDead) return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        if (healthUI != null)
            healthUI.UpdateHealth(currentHealth);

        if (heartAnimator != null)
            heartAnimator.AnimateIncrease();

        StartCoroutine(ApplyIdleAfterDelay());
    }

    IEnumerator ApplyIdleAfterDelay()
    {
        yield return new WaitForSeconds(0.25f); // animation finish hone de

        if (heartAnimator == null || isDead) yield break;

        if (currentHealth > 0f && currentHealth <= lowHealthThreshold)
        {
            heartAnimator.StartLowHealthBlink();
        }
        else if (currentHealth >= maxHealth)
        {
            heartAnimator.StartMaxHealthPulse();
        }
        else
        {
            heartAnimator.StopIdleEffects();
        }
    }

    IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(hitCooldown);
        canTakeDamage = true;
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        if (healthUI != null)
            healthUI.ClearAll();

        if (heartAnimator != null)
            heartAnimator.StopIdleEffects();

        GameEvents.OnGameOver?.Invoke();
    }
}
