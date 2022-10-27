using System.Collections;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [Header("Unity Setup")]

    [SerializeField] GameObject eye;
    AnimatorFunctions animatorFunctions;
    Animator animator;
    Rigidbody2D rb;
    PlayerPhysics playerPhysics;

    [Header("Audio")]

    [SerializeField] int jumpIndex;
    [SerializeField] int landIndex;
    [SerializeField] int crouchIndex;
    bool crouching;

    [Header("Eye Animation")]

    [SerializeField] float eyeBounds;
    float timeCounter;
    float currentEyePosX = 0;
    float newEyePosX;
    float velocityCapY;
    float offsetY;

    bool canLand = false;


    private void Start()
    {
        animatorFunctions = GetComponent<AnimatorFunctions>();
        playerPhysics = GetComponent<PlayerPhysics>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        velocityCapY = playerPhysics.jumpPower * 0.7f;
        offsetY = eye.transform.localPosition.y;
    }

    private void FixedUpdate()
    {
        //calculating eyeposition
        timeCounter += Time.deltaTime;
        eye.transform.localPosition = new Vector3(getNewEyePosX(), getNewEyePosY(), 0);

        //call land animation
        if (rb.velocity.y < -1) canLand = true;
        if (playerPhysics.isGrounded && canLand)
        {
            canLand = false;
            StartCoroutine(land());
        }

        crouch();
    }

    float getNewEyePosX()
    {
        float percent = timeCounter / (playerPhysics.lerpTime / 2);
        return Mathf.Lerp(currentEyePosX, newEyePosX, percent);
    }

    float getNewEyePosY()
    {
        float eyePos;

        if (rb.velocity.y / velocityCapY < 1 && rb.velocity.y / velocityCapY > -1)
        {
            eyePos = (rb.velocity.y / velocityCapY) * eyeBounds;
        }
        else if (rb.velocity.y < 0)
        {
            eyePos = -eyeBounds;
        }
        else
        {
            eyePos = eyeBounds;
        }

        return eyePos + offsetY;
    }

    //called from PlayerPhysics.resetMovement
    public void setNewEyePosX(int newMotionState)
    {
        currentEyePosX = eye.transform.localPosition.x;
        newEyePosX = eyeBounds * newMotionState;
        timeCounter = 0;
    }

    public IEnumerator jump()
    {
        animator.SetBool("jump", true);
        animatorFunctions.PlaySound(jumpIndex);

        yield return new WaitForSeconds(.01f);

        animator.SetBool("jump", false);
    }

    public IEnumerator land()
    {
        animator.SetBool("landed", true);
        animatorFunctions.PlaySound(landIndex);

        yield return new WaitForSeconds(.01f);

        animator.SetBool("landed", false);
    }
    
    public void crouch()
    {
        if (playerPhysics.canJump() && rb.velocity.x > -.01f && rb.velocity.x < .01f && Input.GetAxisRaw("Vertical") < 0)
        {
            animator.SetBool("crouch", true);
            if (!crouching)
            {
                crouching = true;
                animatorFunctions.PlaySound(crouchIndex);
            }
        }
        else
        {
            animator.SetBool("crouch", false);
            crouching = false;
        }
    }
}
