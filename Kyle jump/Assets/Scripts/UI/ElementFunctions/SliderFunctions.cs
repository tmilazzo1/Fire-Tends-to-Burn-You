using UnityEngine.UI;
using UnityEngine;

public class SliderFunctions : MonoBehaviour
{
    [Header("Unity Setup")]

    [SerializeField] Animator leftArrowAnimator;
    [SerializeField] Animator rightArrowAnimator;
    Text numberText;
    [SerializeField] Animator animator;
    Slider slider;

    [Header("Attributes")]

    [SerializeField] int increment;
    bool animPlaying = false;
    bool selected = false;
    float keyDownCounter;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        numberText = GetComponentInChildren<Text>();
        //animator = GetComponent<Animator>();
        //Debug.Log(animator);
    }

    private void OnEnable()
    {
        updateText();
    }

    private void Update()
    {
        if (!selected) return;
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            if (keyDownCounter == 0f || keyDownCounter > .5f)
            {
                if (Input.GetAxisRaw("Horizontal") == 1)
                {
                    if(slider.value < slider.maxValue)
                    {
                        if(!animPlaying)
                        {
                            animPlaying = true;
                            slider.value += increment;
                            rightArrowAnimator.SetTrigger("bounce");
                            GameManager.Instance.GetComponent<AnimatorFunctions>().PlaySound(4);
                        } 
                    }
                }
                else if (Input.GetAxisRaw("Horizontal") == -1)
                {
                    if (slider.value > slider.minValue)
                    {
                        if(!animPlaying)
                        {
                            animPlaying = true;
                            slider.value -= increment;
                            leftArrowAnimator.SetTrigger("bounce");
                            GameManager.Instance.GetComponent<AnimatorFunctions>().PlaySound(5);
                        }
                    }
                }
                updateText();
            }
            keyDownCounter += Time.unscaledDeltaTime;
        }
        else
        {
            keyDownCounter = 0;
        }
    }

    public void updateText()
    {
        numberText.text = slider.value.ToString("00");
    }

    public void selectSlider(bool selectedValue)
    {
        selected = selectedValue;
        animator.SetBool("selected", selectedValue);
    }

    public void animFinished()
    {
        animPlaying = false;
    }
}
