using System.Collections;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [Header("Unity Setup")]

    [SerializeField] GameObject eye;
    Animator animator;
    Rigidbody2D rb;
    PlayerPhysics playerPhysics;


    [Header("Eye Animation")]

    [SerializeField] float eyeBounds;
    float timeCounter;
    float currentEyePosX = 0;
    float newEyePosX;
    float velocityCapY;
    float offsetY;


    private void Start()
    {
        playerPhysics = GetComponent<PlayerPhysics>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        velocityCapY = playerPhysics.getJumpPower() * 0.7f;
        offsetY = eye.transform.localPosition.y;
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;
        eye.transform.localPosition = new Vector3(getNewEyePosX(), getNewEyePosY(), 0);
    }

    float getNewEyePosX()
    {
        float percent = timeCounter / (playerPhysics.getLerpTime() / 2);
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
        yield return new WaitForSeconds(.01f);
        animator.SetBool("jump", false);
    }

    public IEnumerator land()
    {
        animator.SetBool("landed", true);
        yield return new WaitForSeconds(.01f);
        animator.SetBool("landed", false);
    }

    public void crouch()
    {
        animator.SetBool("crouch", true);
    }

    public void uncrouch()
    {
        animator.SetBool("crouch", false);
    }
}
