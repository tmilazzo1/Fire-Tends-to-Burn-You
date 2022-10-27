using UnityEngine;

public class MainMenuFunctions : MonoBehaviour
{
    [SerializeField] int gameSceneIndex;
    [SerializeField] AudioClip gameSong;
    MenuTransitionFunctions menuTransitionFunctions;

    private void Start()
    {
        menuTransitionFunctions = GetComponent<MenuTransitionFunctions>();
    }

    public void newGamePressed()
    {
        menuTransitionFunctions.disableUI();
        GameManager.Instance.sceneTransition.loadScene(gameSceneIndex, gameSong);
    }

    public void quitPressed()
    {
        Application.Quit();
        Debug.Log("quit game");
    }
}
