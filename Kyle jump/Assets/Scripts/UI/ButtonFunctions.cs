using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonFunctions : MonoBehaviour
{
    //someone crazier than me found out how to serialize an event... https://answers.unity.com/questions/1335277/how-to-make-a-custom-onclick-event.html
    [Serializable]
    public class onPressedEvent : UnityEvent { }

    [SerializeField]
    private onPressedEvent onPressed = new onPressedEvent();
    public onPressedEvent pressedEvent { get { return onPressed; } set { onPressed = value; } }


    [SerializeField] ButtonManager buttonManager;
    [SerializeField] int thisIndex;
    Animator animator;
    bool canPress = true;
    bool keyDown = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(buttonManager.getIndex() == thisIndex)
        {
            animator.SetBool("selected", true);
            animator.SetBool("selected", true);
            if (Input.GetAxis("Submit") == 1)
            {
                if(!keyDown)
                {
                    if (canPress)
                    {
                        onPressed.Invoke();
                        animator.SetBool("pressed", true);
                        canPress = false;
                    }
                    keyDown = true;
                }else
                {
                    animator.SetBool("pressed", false);
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
