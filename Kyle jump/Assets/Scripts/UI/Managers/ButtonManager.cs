using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    int maxIndex;
    int index;
    bool keyDown;
    bool isEnabled = true;
    bool isFrozen = false;

    [Header("Unity Setup")]

    [SerializeField] Transform buttonsParent;

    [Header("Audio")]

    [SerializeField] int buttonSelectIndex = 0;

    private void Start()
    {
        maxIndex = buttonsParent.childCount - 1;
        for(int i = 0; i < buttonsParent.childCount; i++)
        {
            buttonsParent.GetChild(i).gameObject.GetComponent<ButtonFunctions>().setVariables(i, this);
        }
    }

    private void Update()
    {
        if (!isEnabled) return;
        if (isFrozen) return;
        if (maxIndex == 0) return;

        if(Input.GetAxisRaw("Vertical") != 0)
        {
            if (!keyDown)
            {
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    if (index <= 0)
                    {
                        index = maxIndex;
                    }
                    else
                    {
                        index--;
                    }
                }
                else if (Input.GetAxisRaw("Vertical") < 0)
                {
                    if (index >= maxIndex)
                    {
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }
                GameManager.Instance.GetComponent<AnimatorFunctions>().PlaySound(buttonSelectIndex);
                keyDown = true;
            }
        }
        else
        {
            keyDown = false;
        }
    }

    public int getIndex()
    {
        if(isEnabled)
        {
            return index;
        }else
        {
            return -1;
        }
    }

    public void changeState(bool enabledVar)
    {
        isEnabled = enabledVar;
    }

    public void freezeFunction(bool freezeVar)
    {
        isFrozen = freezeVar;
    }
}
