using UnityEngine;

public class MainMenuFunctions : MonoBehaviour
{
    [SerializeField] int gameSceneIndex;
    MenuTransitionFunctions menuTransitionFunctions;

    private void Start()
    {
        menuTransitionFunctions = GetComponent<MenuTransitionFunctions>();
    }

    public void newGamePressed()
    {
        menuTransitionFunctions.disableUI();
        GameManager.Instance.sceneTransition.loadScene(gameSceneIndex);
    }

    public void quitPressed()
    {
        Application.Quit();
        Debug.Log("quit game");
    }
}
