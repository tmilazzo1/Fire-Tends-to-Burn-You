using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[System.Serializable]
public class ScreenProperties
{
    public PostProcessProfile postProcessProfile;
    public TransitionFunctions transitionFunctions;
}

public class MainMenuFunctions : MonoBehaviour
{
    [SerializeField] ScreenProperties[] screenProperties;
    [SerializeField] PostProcessVolume ppVolume;
    int currentMenuNumber;
    Animator animator;

    private void Start()
    {
        currentMenuNumber = 1;
        animator = GetComponent<Animator>();
        for (int i = 0; i < screenProperties.Length; i++)
        {
            if (i != currentMenuNumber) screenProperties[i].transitionFunctions.enableUI(false);
        }
    }

    public void changeMenu(int dir)
    {
        screenProperties[currentMenuNumber].transitionFunctions.enableUI(false);

        if (dir > 0)
        {
            dir = 1;
        }else
        {
            dir = -1;
        }

        currentMenuNumber += dir;
        ppVolume.profile = screenProperties[currentMenuNumber].postProcessProfile;
        animator.SetInteger("menuNumber", currentMenuNumber);
    }

    public void enableUI()
    {
        screenProperties[currentMenuNumber].transitionFunctions.enableUI(true);
    }
}
