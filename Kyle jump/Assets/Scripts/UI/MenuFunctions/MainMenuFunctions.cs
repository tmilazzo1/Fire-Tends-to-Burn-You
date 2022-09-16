using UnityEngine;

public class MainMenuFunctions : MonoBehaviour
{
    [SerializeField] string gameSceneName;
    MenuTransitionFunctions menuTransitionFunctions;

    private void Start()
    {
        menuTransitionFunctions = GetComponent<MenuTransitionFunctions>();
    }

    public void newGamePressed()
    {
        menuTransitionFunctions.disableUI();
        SceneTransition.Instance.loadScene(gameSceneName);
    }

    public void newGamePlusPressed()
    {
        Debug.Log("newGame+");
    }

    public void quitPressed()
    {
        Application.Quit();
        Debug.Log("quit game");
    }
}
