using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameConfig config;
    float currentSideSpeed;
    float forwardSpeed;
    [SerializeField] float speedBoostPerMission = 1.8f;
    [SerializeField] float maxSideSpeed = 15f;

    Rigidbody rb;
    PlayerHealth health;
    Animator animator;

    bool isGrounded;
    bool isDead = false;

    void Start()
    {
        currentSideSpeed = config.sideSpeed;
        forwardSpeed = config.startSpeed;
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

    float horizontalInput;
    Vector2 startTouchPos;
    bool isSwiping = false;
    // void Update()
    // {
    //     if (isDead) return;

    //float x = Input.GetAxis("Horizontal");

    // rb.linearVelocity = new Vector3(
    //     x * config.sideSpeed,
    //     rb.linearVelocity.y,
    //     0
    // );

    // if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    // {
    //     rb.AddForce(Vector3.up * config.jumpForce, ForceMode.Impulse);
    //     isGrounded = false;
    // }

    // Vector3 pos = transform.position;
    // pos.x = Mathf.Clamp(pos.x, -config.laneLimit, config.laneLimit);
    // transform.position = pos;
    // }

    void Update()
    {
        if (isDead) return;

#if UNITY_EDITOR || UNITY_STANDALONE
    // PC / Laptop input
    horizontalInput = Input.GetAxis("Horizontal");
#endif

#if UNITY_ANDROID || UNITY_IOS
    // Mobile Swipe Input
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            startTouchPos = touch.position;
            isSwiping = true;
        }
        else if (touch.phase == TouchPhase.Moved && isSwiping)
        {
            float swipeDelta = touch.position.x - startTouchPos.x;

            horizontalInput = swipeDelta / Screen.width * 5f; // sensitivity

            horizontalInput = Mathf.Clamp(horizontalInput, -1f, 1f);
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            horizontalInput = 0f;
            isSwiping = false;
        }
    }
#endif

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * config.jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // Mobile Tap Jump
#if UNITY_ANDROID || UNITY_IOS
if (Input.touchCount > 0)
{
    Touch touch = Input.GetTouch(0);

    if (touch.phase == TouchPhase.Began && isGrounded)
    {
        rb.AddForce(Vector3.up * config.jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }
}
#endif
    }

    // updating logic as per senior's suggestion to make it more smooth and responsive
    // void FixedUpdate()
    // {
    //     if (isDead) return;
    //     //Debug.Log("Z: " + transform.position.z);
    //     float x = Input.GetAxis("Horizontal");

    //     rb.linearVelocity = new Vector3(
    //         x * currentSideSpeed,
    //         rb.linearVelocity.y,
    //         forwardSpeed
    //     );

    //     Vector3 pos = transform.position;
    //     pos.x = Mathf.Clamp(pos.x, -config.laneLimit, config.laneLimit);
    //     transform.position = pos;
    // }

    // void FixedUpdate()
    // {
    //     if (isDead) return;

    //     float x = Input.GetAxis("Horizontal");

    //     Vector3 velocity = rb.linearVelocity;
    //     velocity.x = x * currentSideSpeed;
    //     velocity.z = forwardSpeed;

    //     rb.linearVelocity = velocity;
    // }

    void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = rb.linearVelocity;
        velocity.x = horizontalInput * currentSideSpeed;
        velocity.z = forwardSpeed;

        rb.linearVelocity = velocity;
    }

    void LateUpdate()
    {
        Vector3 pos = rb.position;
        pos.x = Mathf.Clamp(pos.x, -config.laneLimit, config.laneLimit);
        rb.position = pos;
    }

    void OnGameStart()
    {
        if (animator != null)
            animator.enabled = true;
        //Debug.Log(transform.position.z);
    }

    void OnGameOver()
    {
        isDead = true;

        if (animator != null)
            animator.enabled = false;
    }

    void BoostSpeed()
    {
        // config.sideSpeed = Mathf.Min(
        //     config.sideSpeed + speedBoostPerMission,
        //     maxSideSpeed
        // );

        currentSideSpeed = Mathf.Min(
            currentSideSpeed + speedBoostPerMission,
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
        // return config.sideSpeed;
        return currentSideSpeed;
    }
}
