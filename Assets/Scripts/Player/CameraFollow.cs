using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float followSpeed = 8f;

    bool gameOver = false;
    float inertiaTime = 0.4f;
    float timer = 0f;

    void OnEnable()
    {
        GameEvents.OnGameOver += OnGameOver;
    }

    void OnDisable()
    {
        GameEvents.OnGameOver -= OnGameOver;
    }

    void LateUpdate()
    {
        if (!gameOver)
        {
            Vector3 desiredPos = target.position + offset;
            transform.position = Vector3.Lerp(
                transform.position,
                desiredPos,
                followSpeed * Time.deltaTime
            );
        }
        else
        {
            if (timer < inertiaTime)
            {
                transform.Translate(Vector3.forward * 5f * Time.deltaTime);
                timer += Time.deltaTime;
            }
        
        }
    }

    void OnGameOver()
    {
        gameOver = true;
        timer = 0f;
    }
}
