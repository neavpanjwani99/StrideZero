using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameConfig config;

    Rigidbody rb;
    bool isGrounded;
    bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isDead) return;

        float x = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector3(
            x * config.sideSpeed,
            rb.linearVelocity.y,
            0
        );

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * config.jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -config.laneLimit, config.laneLimit);
        transform.position = pos;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
            isGrounded = true;

        if (col.gameObject.CompareTag("Obstacle"))
        {
            isDead = true;
            GameEvents.OnGameOver?.Invoke();
        }
    }
}
