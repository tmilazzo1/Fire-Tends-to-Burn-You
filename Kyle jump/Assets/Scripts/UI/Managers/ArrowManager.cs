using System;
using UnityEngine;
using UnityEngine.Events;

public class ArrowManager : MonoBehaviour
{
    bool keyDown;
    bool isEnabled = true;

    [Serializable]
    public class rightArrowEvents : UnityEvent { }
    [Serializable]
    public class leftArrowEvents : UnityEvent { }

    [Header("left arrow")]

    [SerializeField] Animator leftArrowAnimator;
    [SerializeField] private leftArrowEvents onLeftPressed = new leftArrowEvents();
    public leftArrowEvents leftPressedEvents { get { return onLeftPressed; } set { onLeftPressed = value; } }


    [Header("right arrow")]

    [SerializeField] Animator rightArrowAnimator;
    [SerializeField] private rightArrowEvents onRightPressed = new rightArrowEvents();
    public rightArrowEvents rightPressedEvents { get { return onRightPressed; } set { onRightPressed = value; } }
    
    
    void Update()
    {
        if (!isEnabled) return;

        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            if(!keyDown)
            {
                if(Input.GetAxisRaw("Horizontal") == 1)
                {
                    if (rightArrowAnimator)
                    {
                        rightArrowAnimator.SetTrigger("pressed");
                        onRightPressed.Invoke();
                    }
                }else if(Input.GetAxisRaw("Horizontal") == -1)
                {
                    if (leftArrowAnimator)
                    {
                        leftArrowAnimator.SetTrigger("pressed");
                        onLeftPressed.Invoke();
                    }
                }
                keyDown = true;
            }
        }else
        {
            keyDown = false;
        }
    }

    public void changeState(bool enableVar)
    {
        isEnabled = enableVar;
        if (rightArrowAnimator) rightArrowAnimator.SetBool("enabled", enableVar);
        if (leftArrowAnimator) leftArrowAnimator.SetBool("enabled", enableVar);
    }
}
