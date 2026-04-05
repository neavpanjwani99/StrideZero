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
            Vector3 desiredPos = target.position + offset; // relative position calculate karne ke liye target ke position me offset add karna hai
            // smooth follow karne ke liye Lerp function ka use karenge, jisme current position, desired position aur follow speed ko use karenge
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
                transform.Translate(Vector3.forward * 5f * Time.deltaTime); // game over hone ke baad camera ko thoda aage move karne ke liye, taki player ke Hit hone ka effect zyada dramatic lage
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
