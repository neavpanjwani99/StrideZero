using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameConfig config;

    [SerializeField] float speedBoostPerMission = 1.8f;
    [SerializeField] float maxSideSpeed = 15f;

    Rigidbody rb;
    PlayerHealth health;
    Animator animator;

    bool isGrounded;
    bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.enabled = false;
        }
    }

    void OnEnable()
    {
        GameEvents.OnMissionCompleted += BoostSpeed;
        GameEvents.OnGameOver += OnGameOver;
        GameEvents.OnGameStart += OnGameStart;
    }

    void OnDisable()
    {
        GameEvents.OnMissionCompleted -= BoostSpeed;
        GameEvents.OnGameOver -= OnGameOver;
        GameEvents.OnGameStart -= OnGameStart;
    }

    void Update()
    {
        if (isDead) return;

        float x = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(
            x * config.sideSpeed,
            rb.velocity.y,
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


    void OnGameStart()
    {
        if (animator != null)
            animator.enabled = true;
    }

    void OnGameOver()
    {
        isDead = true;

        if (animator != null)
            animator.enabled = false;
    }

    void BoostSpeed()
    {
        config.sideSpeed = Mathf.Min(
            config.sideSpeed + speedBoostPerMission,
            maxSideSpeed
        );
    }

    void OnCollisionEnter(Collision col)
    {
        if (isDead) return;

        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (col.gameObject.CompareTag("Obstacle"))
        {
            health.TakeDamage(1);
        }
    }

    public float GetCurrentSpeed()
    {
        return config.sideSpeed;
    }
}
