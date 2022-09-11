using UnityEngine;

public class StartMenuFunctions : MonoBehaviour
{
    [SerializeField] MainMenuFunctions mainMenuFunctions;

    public void newGamePressed()
    {
        mainMenuFunctions.changeMenu(-1);
    }

    public void optionsPressed()
    {
        mainMenuFunctions.changeMenu(1);
    }

    public void quitPressed()
    {
        Application.Quit();
        Debug.Log("quit game");
    }
}
