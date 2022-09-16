using UnityEngine;
using UnityEngine.UI;

public class PauseMenuFunctions : MonoBehaviour
{
    [SerializeField] Text levelText;
    [SerializeField] Text timeText;
    [SerializeField] string mainMenuName;
    MenuTransitionFunctions menuTransitionFunctions;
    EscapeFunctions escapeFunctions;

    private void Start()
    {
        escapeFunctions = GameManager.Instance.GetComponent<EscapeFunctions>();
        menuTransitionFunctions = GetComponent<MenuTransitionFunctions>();
    }

    private void OnEnable()
    {
        levelText.text = GameManager.Instance.getCurrentLevel().ToString("00") + " / 30";
        timeText.text = formatTime(GameManager.Instance.getTimeElapsed());
    }

    public void resumePressed()
    {
        escapeFunctions.changePauseState();
    }

    public void quitPressed()
    {
        menuTransitionFunctions.disableUI();
        escapeFunctions.changeAllowFunction(false);
        SceneTransition.Instance.loadScene(mainMenuName);
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
