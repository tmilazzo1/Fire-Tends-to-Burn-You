using UnityEngine;

public class sliderArrowFunctions : MonoBehaviour
{
    [SerializeField] SliderFunctions sliderFunctions;

    public void animFinished()
    {
        sliderFunctions.animFinished();
    }
}
