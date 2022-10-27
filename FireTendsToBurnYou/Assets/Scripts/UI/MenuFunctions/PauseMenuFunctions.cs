using UnityEngine;
using UnityEngine.UI;

public class PauseMenuFunctions : MonoBehaviour
{
    [SerializeField] Text levelText;
    [SerializeField] Text timeText;
    [SerializeField] int mainMenuIndex;
    [SerializeField] AudioClip menuSong;
    MenuTransitionFunctions menuTransitionFunctions;
    PauseFunction escapeFunctions;

    private void Start()
    {
        escapeFunctions = GameManager.Instance.GetComponent<PauseFunction>();
        menuTransitionFunctions = GetComponent<MenuTransitionFunctions>();
    }

    private void OnEnable()
    {
        levelText.text = GameManager.Instance.currentLevelData.levelNum.ToString("00") + " / 30";
        timeText.text = formatTime(GameManager.Instance.timeElapsed);
    }

    public void resumePressed()
    {
        escapeFunctions.changePauseState();
    }

    public void quitPressed()
    {
        menuTransitionFunctions.disableUI();
        escapeFunctions.changeAllowFunction(false);
        GameManager.Instance.sceneTransition.loadScene(mainMenuIndex, menuSong);
    }

    string formatTime(float time)
    {
        int total = (int)time;
        int hours = total / 3600;
        total %= 3600;
        int minutes = total / 60;
        total %= 60;
        int seconds = total;

        return hours.ToString("#0") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
