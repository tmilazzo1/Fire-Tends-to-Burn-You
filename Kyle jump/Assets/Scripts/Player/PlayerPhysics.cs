using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerAnimations playerAnimations;

    [Header("Jumping Attributes")]

    public float jumpPower;
    [SerializeField] float jumpTime;
    [SerializeField] float delayedJumpInputDuration;
    [SerializeField] float coyoteTimeDuration;
    [HideInInspector] public bool isGrounded;
    bool isJumping = false;
    bool justJumped = true;
    bool jumpIsInputed = true;
    float delayedJumpInputCounter;
    float airTimeCounter;
    float coyoteTimeCounter;
    

    [Header("Horizontal Movement")]

    [SerializeField] float moveSpeed;
    public float lerpTime;
    float currentVelocityX;
    float newVelocityX;
    float moveTimeCounter;
    int motionState;


    [Header("raycast")]

    [SerializeField] LayerMask groundMask;
    [SerializeField] float boxHeight = .05f;
    [SerializeField] float headBoxWidthMultiplier;
    bool headCollision = false;
    Vector2 playerSize;
    Vector2 botBoxSize;
    Vector2 headBoxSize;
    Vector2 topCenter;
    Vector2 botCenter;
    RaycastHit2D leftRay;
    RaycastHit2D rightRay;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimations = GetComponent<PlayerAnimations>();
        playerSize = GetComponent<BoxCollider2D>().size * transform.localScale;
        botBoxSize = new Vector2(playerSize.x * .97f, boxHeight);
        headBoxSize = new Vector2(botBoxSize.x * headBoxWidthMultiplier, boxHeight);
    }

    void Update()
    {
        //set anchor points for raycast boxes and lines
        topCenter = (Vector2)transform.position + new Vector2(0, 0.5f * (playerSize.y + boxHeight));
        botCenter = (Vector2)transform.position - new Vector2(0, 0.5f * (playerSize.y + boxHeight));

        
        //code for corner correction using raycast
        if (rb.velocity.y < 0f || headCollision) return;

        float currentVelocity = rb.velocity.y;

        //Debug.Log("raycast stuff");
        float rayLength = (1 - headBoxWidthMultiplier) * botBoxSize.x * 0.5f;
        Vector2 leftRayStart = topCenter - new Vector2(headBoxSize.x * 0.5f, 0f);
        //Debug.DrawRay(leftRayStart, Vector2.left * rayLength);
        Vector2 rightRayStart = topCenter + new Vector2(headBoxSize.x * 0.5f, 0f);
        //Debug.DrawRay(rightRayStart, Vector2.right * rayLength);
        leftRay = Physics2D.Raycast(leftRayStart, Vector2.left, rayLength, groundMask);
        rightRay = Physics2D.Raycast(rightRayStart, Vector2.right, rayLength, groundMask);

        if (leftRay.collider)
        {
            float playerLeftSide = transform.position.x - playerSize.x * 0.5f;
            float correctionValue = Mathf.Abs(playerLeftSide - (leftRay.point.x + 0.01f));
            transform.position = new Vector2(transform.position.x + correctionValue, transform.position.y);
            rb.velocity = new Vector2(rb.velocity.x, currentVelocity);
        }

        if (rightRay.collider)
        {
            float playerRightSide = transform.position.x + playerSize.x * 0.5f;
            float correctionValue = Mathf.Abs(playerRightSide - (rightRay.point.x - 0.01f));
            transform.position = new Vector2(transform.position.x - correctionValue, transform.position.y);
            rb.velocity = new Vector2(rb.velocity.x, currentVelocity);
        }
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(topCenter, headBoxSize);
        Gizmos.DrawWireCube(botCenter, botBoxSize);
    }
    */
    private void FixedUpdate()
    {
        jump();
        setIsGrounded();
        setVelocityX();
    }

    void jump()
    {
        delayedJumpInputCounter += Time.deltaTime;
        if (Input.GetAxisRaw("Jump") > 0 || delayedJumpInputCounter <= delayedJumpInputDuration)
        {
            if(!jumpIsInputed) delayedJumpInputCounter = 0;
            jumpIsInputed = true;
            if (canJump())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                justJumped = true;
                isJumping = true;
                delayedJumpInputCounter = delayedJumpInputDuration + 1f;
                airTimeCounter = 0;
                playerAnimations.StartCoroutine(playerAnimations.jump());
            }

            if (Input.GetAxisRaw("Jump") > 0)
            {
                if (isJumping && jumpTime > airTimeCounter)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                    airTimeCounter += Time.deltaTime;
                    if (Physics2D.OverlapBox(topCenter, headBoxSize, 0f, groundMask) != null)
                    {
                        headCollision = true;
                        isJumping = false;
                    }else
                    {
                        headCollision = false;
                    }
                }
                else
                {
                    isJumping = false;
                }
            }
        }
        else
        {
            jumpIsInputed = false;
            justJumped = false;
            isJumping = false;
        }
    }

    void setIsGrounded()
    {
        if (Physics2D.OverlapBox(botCenter, botBoxSize, 0f, groundMask))
        {
            isGrounded = true;
        }else
        {
            isGrounded = false;
        }
        if (isJumping) isGrounded = false;

        //coyote time
        if (!isGrounded)
        {
            coyoteTimeCounter += Time.deltaTime;
        }
        else
        {
            coyoteTimeCounter = 0;
        }
    }

    public bool canJump()
    {
        bool canJump = true;

        if (!isGrounded && coyoteTimeCounter > coyoteTimeDuration) canJump = false;
        if (rb.velocity.y > .01 && rb.velocity.y < -.01) canJump = false;
        if (justJumped) canJump = false;

        return canJump;
    }

    void setVelocityX()
    {
        //sets the horizontal movement
        float percent = moveTimeCounter / lerpTime;
        rb.velocity = new Vector2(Mathf.Lerp(currentVelocityX, newVelocityX, percent), rb.velocity.y);


        //calculates the horizontal speed and lerping
        moveTimeCounter += Time.deltaTime;

        if (Input.GetAxisRaw("Horizontal") > 0 && motionState != 1)
        {
            resetMovement(1);
            newVelocityX = moveSpeed;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && motionState != -1)
        {
            resetMovement(-1);
            newVelocityX = -moveSpeed;
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 && motionState != 0)
        {
            resetMovement(0);
            newVelocityX = 0;
        }
    }

    //called in update function
    void resetMovement(int newMotionState)
    {
        motionState = newMotionState;
        playerAnimations.setNewEyePosX(newMotionState);
        moveTimeCounter = 0;
        currentVelocityX = rb.velocity.x;
    }
}
