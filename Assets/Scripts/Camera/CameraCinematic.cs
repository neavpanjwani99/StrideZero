using UnityEngine;
using System.Collections;

public class CameraCinematic : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float followSmooth = 5f;

    [Header("Shake")]
    public float hitShakeDuration = 0.18f;
    public float hitShakeIntensity = 0.08f;

    public float deathShakeDuration = 0.35f;
    public float deathShakeIntensity = 0.12f;

    Vector3 velocity;

    bool isGameOver;
    bool isShaking;

    void OnEnable()
    {
        
        isGameOver = false;
        isShaking = false;

        GameEvents.OnPlayerHit += OnPlayerHit;
        GameEvents.OnGameOver += OnGameOver;
    }

    void OnDisable()
    {
        GameEvents.OnPlayerHit -= OnPlayerHit;
        GameEvents.OnGameOver -= OnGameOver;
    }

    void LateUpdate()
    {
        if (target == null || isGameOver) return;

        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPos,
            ref velocity,
            1f / followSmooth
        );
    }

    void OnPlayerHit()
    {
        if (isGameOver || isShaking) return;

        StartCoroutine(CameraShake(hitShakeDuration, hitShakeIntensity));
    }

    void OnGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        StopAllCoroutines();
        StartCoroutine(CameraShake(deathShakeDuration, deathShakeIntensity));
    }

    IEnumerator CameraShake(float duration, float intensity)
{
    isShaking = true;

    Vector3 originalPos = transform.position;
    float t = 0f;

    while (t < duration)
    {
        t += Time.unscaledDeltaTime;  
        
        float damper = 1f - (t / duration);

        float x = Random.Range(-1f, 1f) * intensity * damper;
        float y = Random.Range(-1f, 1f) * intensity * damper;

        transform.position = originalPos + new Vector3(x, y, 0f);

        yield return null;
    }

    transform.position = originalPos;
    isShaking = false;
}

}
