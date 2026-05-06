using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float lowHealthThreshold = 1f;
    public HeartAnimator heartAnimator; // heart animation controller reference, jise hum inspector se assign karenge, taki health ke hisab se heart animation ko control kar sake

    public float maxHealth = 4f;
    public float hitCooldown = 0.6f; // 0.6 sec ke baad player ko phir se demage milega 

    [Header("Audio")] // audio related variables ke liye header jho inspector me alag se dikhai de jab script attach karenge
    public AudioClip hitSound; // hit pe jho audio play ho raha hai uska reference, jise hum inspector se assign karenge, taki player hit hone pe sound play ho sake

    AudioSource audioSource;

    public float CurrentHealth => currentHealth;

    float currentHealth;
    bool isDead = false;
    bool canTakeDamage = true;

    HealthUIController healthUI;

    void Awake()
    {
        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();

        healthUI = FindObjectOfType<HealthUIController>();
        if (healthUI != null)
            healthUI.UpdateHealth(currentHealth);
    }

    public void TakeDamage(float damage)
    {
        if (isDead || !canTakeDamage) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0f);

        // HIT SOUND
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

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

        StartCoroutine(ApplyIdleAfterDelay()); // coroutine to apply idle effects after a short delay, taki health increase hone ke baad heart animation me changes dikhai de
    }

    IEnumerator ApplyIdleAfterDelay()
    {
        yield return new WaitForSeconds(0.25f);

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

    // void Die()
    // {
    //     if (isDead) return;

    //     isDead = true;

    //     if (healthUI != null)
    //         healthUI.ClearAll();

    //     if (heartAnimator != null)
    //         heartAnimator.StopIdleEffects();

    //     GameEvents.OnGameOver?.Invoke();
    // }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        Vector3 deathPosition = transform.position;
        Quaternion deathRotation = transform.rotation;

        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.detectCollisions = false;
        }

        transform.position = deathPosition;
        transform.rotation = deathRotation;

        if (heartAnimator != null)
            heartAnimator.StopIdleEffects();

        Animator animator = GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.applyRootMotion = false;
            animator.SetBool("IsJumping", false);
            animator.SetTrigger("Die");
        }

        StartCoroutine(GameOverAfterDeathAnimation(deathPosition, deathRotation));
    }

    IEnumerator GameOverAfterDeathAnimation(Vector3 deathPosition, Quaternion deathRotation)
    {
        float timer = 0f;
        float deathAnimTime = 3.2f;

        while (timer < deathAnimTime)
        {
            transform.position = deathPosition;
            transform.rotation = deathRotation;

            timer += Time.deltaTime;
            yield return null;
        }

        if (healthUI != null)
            healthUI.ClearAll();

        GameEvents.OnGameOver?.Invoke();
    }
}