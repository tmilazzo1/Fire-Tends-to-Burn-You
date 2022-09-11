using UnityEngine;

public class TransitionFunctions : MonoBehaviour
{
    ButtonManager buttonManager;
    ArrowManager arrowManager;

    private void Start()
    {
        buttonManager = GetComponent<ButtonManager>();
        arrowManager = GetComponent<ArrowManager>();
    }

    public void enableUI(bool enabledVar)
    {
        arrowManager.changeState(enabledVar);
        buttonManager.changeState(enabledVar);
    }
}
