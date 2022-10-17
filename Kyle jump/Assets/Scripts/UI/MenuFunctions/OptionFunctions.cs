using UnityEngine;
using UnityEngine.UI;

public class OptionFunctions : MonoBehaviour
{
    [Header("fullScreen variables")]

    [SerializeField] Animator fullScreenToggleAnimator;
    bool isFullScreen;

    [Header("volume Variables")]

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    ButtonFunctions selectedButtonFunctions;
    Slider selectedSlider;

    AudioManager audioManager;
    TransitionManager transitionFunctions;
    bool volumeSelected = false;
    

    private void Start()
    {
        transitionFunctions = GetComponent<TransitionManager>();
        audioManager = GameManager.Instance.GetComponent<AudioManager>();
        setVariables();
    }

    public void setVariables()
    {
        isFullScreen = Screen.fullScreen;
        fullScreenToggleAnimator.SetBool("toggle", isFullScreen);

        musicSlider.GetComponent<Slider>().value = audioManager.getMusicVolume();
        musicSlider.GetComponent<SliderFunctions>().updateText();

        sfxSlider.GetComponent<Slider>().value = audioManager.getSFXVolume();
        sfxSlider.GetComponent<SliderFunctions>().updateText();
    }

    public void fullScreenPressed()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
        fullScreenToggleAnimator.SetBool("toggle", isFullScreen);
    }

    public void volumePressed(SecondaryButtonElements secondaryButtonElements)
    {
        //set variables
        volumeSelected = !volumeSelected;
        if (secondaryButtonElements)
        {
            selectedButtonFunctions = secondaryButtonElements.buttonFunctions;
            selectedSlider = secondaryButtonElements.secondaryObject.GetComponent<Slider>();
        }

        //disallow transistions and change escape function
        transitionFunctions.freezeFunctions(volumeSelected);
        GameManager.Instance.GetComponent<EscapeFunctions>().changeAllowFunction(!volumeSelected);

        //select slider and change animator
        selectedSlider.GetComponent<SliderFunctions>().selectSlider(volumeSelected);
        selectedButtonFunctions.GetComponent<Animator>().SetBool("sliderSelected", volumeSelected);
    }

    public void setMusicVolume()
    {
        audioManager.setMusicVolume(musicSlider.GetComponent<Slider>().value);
    }

    public void setSFXVolume()
    {
        audioManager.setSFXVolume(sfxSlider.GetComponent<Slider>().value);
    }

    private void Update()
    {
        if(Input.GetAxisRaw("Cancel") == 1)
        {
            if(volumeSelected)
            {
                selectedButtonFunctions.pressButton();
                volumePressed(null);
            }
        }
    }
}
