using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    ButtonManager buttonManager;
    ArrowManager arrowManager;

    private void Awake()
    {
        buttonManager = GetComponent<ButtonManager>();
        arrowManager = GetComponent<ArrowManager>();
    }

    public void enableUI(bool enabledVar)
    {
        if(!buttonManager) buttonManager = GetComponent<ButtonManager>();
        if(!arrowManager) arrowManager = GetComponent<ArrowManager>();
        arrowManager.changeState(enabledVar);
        buttonManager.changeState(enabledVar);
    }

    public void freezeFunctions(bool freezeVar)
    {
        arrowManager.changeState(!freezeVar);
        buttonManager.freezeFunction(freezeVar);
    }
}
