using UnityEngine;

public class SecondaryButtonElements : MonoBehaviour
{
    public GameObject secondaryObject;
    [HideInInspector] public ButtonFunctions buttonFunctions;
    Animator secondaryAnimator;
    ButtonManager buttonManager;
    int thisIndex;

    public void setVar(int newIndex, ButtonManager newButtonManager)
    {
        thisIndex = newIndex;
        buttonManager = newButtonManager;
        secondaryAnimator = secondaryObject.GetComponent<Animator>();
        buttonFunctions = GetComponent<ButtonFunctions>();
    }

    private void Update()
    {
        if (thisIndex == buttonManager.getIndex())
        {
            secondaryAnimator.SetBool("hovered", true);
        }else
        {
            secondaryAnimator.SetBool("hovered", false);
        }
    }
}
