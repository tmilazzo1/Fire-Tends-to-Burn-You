using System;
using UnityEngine;
using UnityEngine.Events;

public class ButtonFunctions : MonoBehaviour
{
    //someone crazier than me found out how to serialize an event... https://answers.unity.com/questions/1335277/how-to-make-a-custom-onclick-event.html
    [Serializable]
    public class onPressedEvent : UnityEvent { }

    [SerializeField]
    private onPressedEvent onPressed = new onPressedEvent();
    public onPressedEvent pressedEvent { get { return onPressed; } set { onPressed = value; } }

    ButtonManager buttonManager;
    int thisIndex;
    Animator animator;
    bool canPress = true;
    bool keyDown = false;

    public void setVariables(int newIndex, ButtonManager newButtonManager)
    {
        buttonManager = newButtonManager;
        thisIndex = newIndex;
        animator = GetComponent<Animator>();
        if (GetComponent<buttonSelected>()) GetComponent<buttonSelected>().setVar(thisIndex, buttonManager);
    }

    private void Update()
    {
        if (!buttonManager) return;
        if(buttonManager.getIndex() == thisIndex)
        {
            animator.SetBool("selected", true);
            animator.SetBool("selected", true);
            if (Input.GetAxisRaw("Submit") == 1)
            {
                if(!keyDown)
                {
                    if (canPress)
                    {
                        onPressed.Invoke();
                        animator.SetTrigger("pressed");
                        canPress = false;
                    }
                    keyDown = true;
                }
            }else
            {
                keyDown = false;
            }
        }
        else
        {
            animator.SetBool("selected", false);
        }
    }

    public void enablePress()
    {
        canPress = true;
    }
}
