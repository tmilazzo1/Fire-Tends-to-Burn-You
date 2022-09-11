using UnityEngine;

public class StartMenuFunctions : MonoBehaviour
{
    [SerializeField] MainMenuFunctions mainMenuFunctions;

    public void newGamePressed()
    {
        Debug.Log("new game");
    }

    public void optionsPressed()
    {
        Debug.Log("Options");
    }

    public void quitPressed()
    {
        Debug.Log("quit");
    }
}
