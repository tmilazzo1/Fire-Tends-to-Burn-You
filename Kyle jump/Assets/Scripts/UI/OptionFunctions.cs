using UnityEngine;
using UnityEngine.UI;

public class OptionFunctions : MonoBehaviour
{
    [SerializeField] GameObject volumeSlider;
    TransitionFunctions transitionFunctions;
    bool functionsFrozen = false;

    private void Start()
    {
        transitionFunctions = GetComponent<TransitionFunctions>();
    }

    public void fullScreenPressed()
    {
        Debug.Log("fullscreen");
    }

    public void volumePressed()
    {
        volumeSlider.GetComponent<SliderFunctions>().selectSlider();
        functionsFrozen = !functionsFrozen;
        transitionFunctions.freezeFunctions(functionsFrozen);
    }

    public void setVolume()
    {
        Debug.Log(volumeSlider.GetComponent<Slider>().value);
    }
}
