using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] MainMenuFunctions mainMenuFunctions;
    [SerializeField] Animator leftArrowAnimator;
    [SerializeField] Animator rightArrowAnimator;
    bool keyDown;
    bool isEnabled = true;

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
                        mainMenuFunctions.changeMenu(1);
                    }
                }else if(Input.GetAxisRaw("Horizontal") == -1)
                {
                    if (leftArrowAnimator)
                    {
                        leftArrowAnimator.SetTrigger("pressed");
                        mainMenuFunctions.changeMenu(-1);
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
