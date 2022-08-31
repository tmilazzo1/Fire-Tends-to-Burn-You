using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [Header("Unity Setup")]

    [SerializeField] Transform feetPos;
    [SerializeField] Transform headPos;
    Rigidbody2D rb;
    PlayerAnimations playerAnimations;


    [Header("Jumping Attributes")]

    [SerializeField] float jumpPower;
    [SerializeField] float jumpTime;
    [SerializeField] float delayedJumpInputDuration;
    [SerializeField] float coyoteTimeDuration;
    bool isJumping = false;
    bool justJumped = false;
    bool canLand = false;
    bool delayedJumpInput = false;
    float jumpButtonDownCounter;
    float airTimeCounter;
    float coyoteTimeCounter;
    

    [Header("Horizontal Movement")]

    [SerializeField] float moveSpeed;
    [SerializeField] float lerpTime;
    [SerializeField] float airLerpTimeMultiplier;
    float currentVelocityX;
    float newVelocityX;
    float moveTimeCounter;
    int motionState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimations = GetComponent<PlayerAnimations>();
    }

    private void FixedUpdate()
    {
        //calculates vertical movement
        if(Input.GetAxisRaw("Jump") > 0 || delayedJumpInput)
        {
            if(canJump())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                justJumped = true;
                isJumping = true;
                airTimeCounter = 0;
                playerAnimations.StartCoroutine(playerAnimations.jump());
            }

            if(Input.GetAxisRaw("Jump") > 0)
            {
                if (isJumping && jumpTime > airTimeCounter)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                    airTimeCounter += Time.deltaTime;
                    if (headPos.gameObject.GetComponent<DetectCollision>().detectCollision()) isJumping = false;
                }
                else
                {
                    isJumping = false;
                }
            }
            jumpButtonIsPressed();
        }
        else
        {
            justJumped = false;
            isJumping = false;
            delayedJumpInput = false;
            jumpButtonDownCounter = 0;
        }
        
        //coyote time
        if(!isGrounded())
        {
            coyoteTimeCounter += Time.deltaTime;
        }else
        {
            coyoteTimeCounter = 0;
        }

        //call land animation
        if (rb.velocity.y < -.1) canLand = true;
        if (isGrounded() && canLand)
        {
            canLand = false;
            playerAnimations.StartCoroutine(playerAnimations.land());
        }

        //call crouch animation
        if(canJump() && rb.velocity.x > -.01f && rb.velocity.x < .01f && Input.GetAxisRaw("Vertical") < 0)
        {
            playerAnimations.crouch();
        }else
        {
            playerAnimations.uncrouch();
        }


        //sets the horizontal movement
        rb.velocity = new Vector2(getNewVelocityX(), rb.velocity.y);


        //calculates the horizontal speed and lerping
        moveTimeCounter += Time.deltaTime;

        if (Input.GetAxisRaw("Horizontal") > 0 && motionState != 1)
        {
            resetMovement(1);
            newVelocityX = moveSpeed;
        }else if(Input.GetAxisRaw("Horizontal") < 0 && motionState != -1)
        {
            resetMovement(-1);
            newVelocityX = -moveSpeed;
        }
        else if(Input.GetAxisRaw("Horizontal") == 0 && motionState != 0)
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

    float getNewVelocityX()
    {
        float percent = moveTimeCounter / getLerpTime();
        return Mathf.Lerp(currentVelocityX, newVelocityX, percent);
    }

    //called in getNewVelocityX
    public float getLerpTime()
    {
        if (isGrounded())
        {
            return lerpTime;
        }
        else
        {
            return lerpTime * airLerpTimeMultiplier;
        }
    }

    public bool isGrounded()
    {
        bool isGrounded = true;

        if (!feetPos.gameObject.GetComponent<DetectCollision>().detectCollision()) isGrounded = false;
        if (isJumping) isGrounded = false;

        return isGrounded;
    }

    //called in update function
    bool canJump()
    {
        bool canJump = true;

        if (!isGrounded() && coyoteTimeCounter > coyoteTimeDuration) canJump = false;
        if (rb.velocity.y > .01 && rb.velocity.y < -.01) canJump = false;
        if (justJumped) canJump = false;

        return canJump;
    }

    //called in update function
    void jumpButtonIsPressed()
    {
        jumpButtonDownCounter += Time.deltaTime;

        if(jumpButtonDownCounter > delayedJumpInputDuration)
        {
            delayedJumpInput = false;
        }else
        {
            delayedJumpInput = true;
        }
    }

    //called in PlayerAnimations.Start
    public float getJumpPower()
    {
        return jumpPower;
    }
}
