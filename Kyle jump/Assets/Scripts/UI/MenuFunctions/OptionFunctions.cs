using UnityEngine;
using UnityEngine.UI;

public class OptionFunctions : MonoBehaviour
{
    [SerializeField] GameObject volumeSlider;
    [SerializeField] Animator fullScreenToggleAnimator;
    [SerializeField] Animator volumeButtonAnimator;
    TransitionManager transitionFunctions;
    bool volumeSelected = false;
    bool isFullScreen;

    private void Start()
    {
        transitionFunctions = GetComponent<TransitionManager>();
        isFullScreen = Screen.fullScreen;
        setFullScreen();
    }

    public void fullScreenPressed()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
        fullScreenToggleAnimator.SetBool("toggle", isFullScreen);
    }

    public void setFullScreen()
    {
        fullScreenToggleAnimator.SetBool("toggle", isFullScreen);
    }

    public void volumePressed()
    {
        volumeSelected = !volumeSelected;
        volumeSlider.GetComponent<SliderFunctions>().selectSlider(volumeSelected);
        transitionFunctions.freezeFunctions(volumeSelected);
        volumeButtonAnimator.SetBool("sliderSelected", volumeSelected);
        if (GameManager.Instance) GameManager.Instance.GetComponent<EscapeFunctions>().changeAllowFunction(!volumeSelected);
    }

    public void setVolume()
    {
        Debug.Log(volumeSlider.GetComponent<Slider>().value);
    }

    private void Update()
    {
        if(Input.GetAxisRaw("Cancel") == 1)
        {
            if(volumeSelected)
            {
                volumePressed();
            }
        }
    }
}
