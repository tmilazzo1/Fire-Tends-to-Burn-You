using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[System.Serializable]
public class ScreenProperties
{
    public PostProcessProfile postProcessProfile;
    public TransitionManager transitionManager;
}

public class MenuTransitionFunctions : MonoBehaviour
{
    [Header("Unity Setup")]

    [SerializeField] int startingMenuNumber;
    int currentMenuNumber;
    Animator animator;

    [Header("Post-Processing Variables")]

    [SerializeField] PostProcessVolume ppVolume;
    [SerializeField] ScreenProperties[] screenProperties;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        currentMenuNumber = startingMenuNumber;
        for (int i = 0; i < screenProperties.Length; i++)
        {
            if (i != currentMenuNumber)
            {
                screenProperties[i].transitionManager.enableUI(false);
            }else
            {
                screenProperties[i].transitionManager.enableUI(true);
            }
        }
    }

    public void changeMenu(int dir)
    {
        screenProperties[currentMenuNumber].transitionManager.enableUI(false);

        if (dir > 0)
        {
            dir = 1;
        }else
        {
            dir = -1;
        }

        currentMenuNumber += dir;
        if(ppVolume) ppVolume.profile = screenProperties[currentMenuNumber].postProcessProfile;
        animator.SetInteger("menuNumber", currentMenuNumber);
    }

    //called in animations of scene transitions
    public void enableUI()
    {
        screenProperties[currentMenuNumber].transitionManager.enableUI(true);
    }

    //called when transitioning to a new scene
    public void disableUI()
    {
        for (int i = 0; i < screenProperties.Length; i++)
        {
            screenProperties[i].transitionManager.enableUI(false);
        }
    }
}
