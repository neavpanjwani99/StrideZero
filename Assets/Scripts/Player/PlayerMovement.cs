using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameConfig config;
    float currentSideSpeed;
    float forwardSpeed;
    float speedTimer = 0f;
    [SerializeField] float speedBoostPerMission = 1.8f; //  yeh extra variable isliye banaya hai taki har mission complete hone par player ki speed kitni badhegi usko control kar sake, aur isse hum inspector se adjust kar sakte hain
    [SerializeField] float maxSideSpeed = 15f; // yeh variable side speed ka maximum limit set karne ke liye hai

    Rigidbody rb;
    PlayerHealth health;
    Animator animator; // it was implemented when i was implementing human player as a runner 

    bool isGrounded;
    bool isDead = false;
    bool jumpLocked = false;
    [SerializeField] float jumpInputCooldown = 0.35f;

    void Start()
    {
        currentSideSpeed = config.sideSpeed;
        forwardSpeed = config.startSpeed;
        rb = GetComponent<Rigidbody>();
        health = GetComponent<PlayerHealth>();
        // animator = GetComponent<Animator>();
        // man animator (player ke ander child me rakha hai man_full ko )
        animator = GetComponentInChildren<Animator>();

        if (animator != null)
        {
            animator.enabled = false;
            animator.applyRootMotion = false;
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

    float minSwipeDistance = 80f;
    Vector2 swipeStartPos;
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
        // Speed progression system
        speedTimer += Time.deltaTime;

        forwardSpeed += config.speedIncrease * Time.deltaTime;
        forwardSpeed = Mathf.Min(forwardSpeed, config.maxSpeed);

#if UNITY_ANDROID || UNITY_IOS
if (Input.touchCount > 0)
{
    Touch touch = Input.GetTouch(0);

    if (touch.phase == TouchPhase.Began)
    {
        swipeStartPos = touch.position;
    }
    else if (touch.phase == TouchPhase.Ended)
    {
        Vector2 swipeDelta = touch.position - swipeStartPos;

        // Check if swipe is big enough
        if (swipeDelta.magnitude > minSwipeDistance)
        {
            // Horizontal Swipe
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                if (swipeDelta.x > 0)
                {
                    horizontalInput = 1f; // Right
                }
                else
                {
                    horizontalInput = -1f; // Left
                }

                Invoke(nameof(ResetHorizontal), 0.15f);
            }
            // Vertical Swipe
            else
            {
                // if (swipeDelta.y > 0 && isGrounded)
                // {
                //     rb.AddForce(Vector3.up * config.jumpForce, ForceMode.Impulse);
                //     isGrounded = false;
                // }

                // updated

                // if (swipeDelta.y > 0 && isGrounded)
                // {
                //     rb.AddForce(Vector3.up * config.jumpForce, ForceMode.Impulse);
                //     isGrounded = false;

                //     if (animator != null)
                //         animator.SetBool("IsJumping", true);
                // }

                if (swipeDelta.y > 0)
{
    TryJump();
}
            }
        }
    }
}
#endif

        // if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        // {
        //     rb.AddForce(Vector3.up * config.jumpForce, ForceMode.Impulse);
        //     isGrounded = false;
        // }

        // updated

        // if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        // {
        //     rb.AddForce(Vector3.up * config.jumpForce, ForceMode.Impulse);
        //     isGrounded = false;

        //     if (animator != null)
        //         animator.SetBool("IsJumping", true);
        // }

        if (Input.GetKeyDown(KeyCode.Space))
{
    TryJump();
}

    }

    void ResetHorizontal()
    {
        horizontalInput = 0f;
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

    void TryJump()
{
    if (!isGrounded || jumpLocked) return;

    jumpLocked = true;
    isGrounded = false;

    rb.AddForce(Vector3.up * config.jumpForce, ForceMode.Impulse);

    if (animator != null)
        animator.SetBool("IsJumping", true);

    // Invoke(nameof(UnlockJump), jumpInputCooldown);
    Invoke(nameof(UnlockJump), 0.15f);
}

void UnlockJump()
{
    jumpLocked = false;
}

void StopJumpAnimation()
{
    if (animator != null)
        animator.SetBool("IsJumping", false);
}

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

    // void BoostSpeed()
    // {
    // config.sideSpeed = Mathf.Min(
    //     config.sideSpeed + speedBoostPerMission,
    //     maxSideSpeed
    // );

    //     currentSideSpeed = Mathf.Min(
    //         currentSideSpeed + speedBoostPerMission,
    //         maxSideSpeed
    //     );
    // }
    public float GetForwardSpeed()
    {
        return forwardSpeed;
    }
    void BoostSpeed()
    {
        currentSideSpeed = Mathf.Min(
            currentSideSpeed + speedBoostPerMission,
            maxSideSpeed
        );

        forwardSpeed += 4f; // strong spike
        forwardSpeed = Mathf.Min(forwardSpeed, config.maxSpeed);
    }

    void OnCollisionEnter(Collision col)
    {
        if (isDead) return;

        // if (col.gameObject.CompareTag("Ground"))
        // {
        //     isGrounded = true;
        // }
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            // if (animator != null)
            //     animator.SetBool("IsJumping", false);

            if (animator != null)
    Invoke(nameof(StopJumpAnimation), 0.05f);
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
