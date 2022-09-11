using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] int maxIndex;
    int index;
    bool keyDown;
    bool isEnabled = true;

    private void Update()
    {
        if (!isEnabled) return;

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
}
