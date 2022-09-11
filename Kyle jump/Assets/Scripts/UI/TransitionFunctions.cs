using UnityEngine;

public class TransitionFunctions : MonoBehaviour
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
        arrowManager.changeState(enabledVar);
        buttonManager.changeState(enabledVar);
    }

    public void freezeFunctions(bool freezeVar)
    {
        arrowManager.changeState(!freezeVar);
        buttonManager.freezeFunction(freezeVar);
    }
}
