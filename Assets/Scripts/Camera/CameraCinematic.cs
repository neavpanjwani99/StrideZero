using UnityEngine;
using System.Collections;

public class CameraCinematic : MonoBehaviour{
    public Transform target;
    public Vector3 offset;
    public float followSmooth = 5f;

    // (attribute) header ke jese kaam karega (inspector use ke liye not for main logic(code))
    [Header("Shake")]
    // camera hit shake ke liye 
    public float hitShakeDuration = 0.18f;
    public float hitShakeIntensity = 0.08f;
    // death ke liye shake (camera)
    public float deathShakeDuration = 0.35f;
    public float deathShakeIntensity = 0.12f;
    Vector3 velocity; // speed track karne ke liye
    bool isGameOver; // game over = true/false
    bool isShaking; // phele se hi tho shake nahi ho raha hai yeh check karne ke liye 

// check that kabhi game over nahi hua hai aur kabhi shake nahi ho raha hai
    void OnEnable(){
        
        isGameOver = false;
        isShaking = false;

        GameEvents.OnPlayerHit += OnPlayerHit;
        GameEvents.OnGameOver += OnGameOver;
    }

    void OnDisable(){
        GameEvents.OnPlayerHit -= OnPlayerHit;
        GameEvents.OnGameOver -= OnGameOver;
    }

// update ki jagah late update use karenge taaki player movement ke baad camera move kare
    void LateUpdate(){
        if (target == null || isGameOver) return;

        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPos,
            ref velocity,
            1f / followSmooth
        );
    }

// player ke hit pe camera shake karega 
    void OnPlayerHit()
    {
        if (isGameOver || isShaking) return;

        StartCoroutine(CameraShake(hitShakeDuration, hitShakeIntensity));
    }
//game khatam hone pe camera shake hoga (hard intensity se game over pe)
    void OnGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        StopAllCoroutines();
        StartCoroutine(CameraShake(deathShakeDuration, deathShakeIntensity));
    }

    // finally used to shake the camera (coroutine)
    IEnumerator CameraShake(float duration, float intensity)
    {
        isShaking = true;

        Vector3 originalPos = transform.position;
        float t = 0f;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;

            float damper = 1f - (t / duration);
// damper use karenge taaki shake gradually kam ho jaye jaise hi time badhta hai, shake intensity kam hoti jaye
// random.range ae -1 se 1 ke biche ki value dega..
            float x = Random.Range(-1f, 1f) * intensity * damper; // random shake ke liye x axis shake karenge
            float y = Random.Range(-1f, 1f) * intensity * damper; // random shake ke liye y axis bhi shake karenge

            transform.position = originalPos + new Vector3(x, y, 0f);

            yield return null;
        }

        transform.position = originalPos;
        isShaking = false;
    }

}
