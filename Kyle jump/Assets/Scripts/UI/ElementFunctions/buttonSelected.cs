using UnityEngine;

public class buttonSelected : MonoBehaviour
{
    [SerializeField] Animator toggleAnimator;
    ButtonManager buttonManager;
    int thisIndex;

    public void setVar(int newIndex, ButtonManager newButtonManager)
    {
        thisIndex = newIndex;
        buttonManager = newButtonManager;
    }

    private void Update()
    {
        if (thisIndex == buttonManager.getIndex())
        {
            toggleAnimator.SetBool("hovered", true);
        }else
        {
            toggleAnimator.SetBool("hovered", false);
        }
    }
}
