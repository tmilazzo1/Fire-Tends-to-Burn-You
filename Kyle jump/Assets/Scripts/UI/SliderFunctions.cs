using UnityEngine.UI;
using UnityEngine;

public class SliderFunctions : MonoBehaviour
{
    Text numberText;
    Animator animator;
    [SerializeField] Animator leftArrowAnimator;
    [SerializeField] Animator rightArrowAnimator;
    Slider slider;
    bool selected = false;
    float keyDownCounter;
    [SerializeField] int increment;

    private void Start()
    {
        slider = GetComponent<Slider>();
        numberText = GetComponentInChildren<Text>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!selected) return;
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            if (keyDownCounter == 0f || keyDownCounter > .5f)
            {
                if (Input.GetAxisRaw("Horizontal") == 1)
                {
                    slider.value += increment;
                    rightArrowAnimator.SetTrigger("bounce");
                }
                else if (Input.GetAxisRaw("Horizontal") == -1)
                {
                    slider.value -= increment;
                    leftArrowAnimator.SetTrigger("bounce");
                }
                updateText();
            }
            keyDownCounter += Time.deltaTime;
        }
        else
        {
            keyDownCounter = 0;
        }
    }

    void updateText()
    {
        numberText.text = slider.value.ToString("00");
    }

    public void selectSlider()
    {
        selected = !selected;
        animator.SetBool("selected", selected);
    }
}
