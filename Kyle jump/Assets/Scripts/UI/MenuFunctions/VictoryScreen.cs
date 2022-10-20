using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    int submitCounts;
    bool keyDown = true;
    Animator animator;
    AnimatorFunctions animatorFunctions;
    [SerializeField] int mainMenuIndex;

    [Header("UI")]

    [SerializeField] Text timeText;
    [SerializeField] Text deathText;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animatorFunctions = GetComponent<AnimatorFunctions>();
        deathText.text = GameManager.Instance.playerDeaths.ToString();
        timeText.text = formatTime(GameManager.Instance.timeElapsed);
    }

    private void Update()
    {
        if (submitCounts == 3) return;
        if(Input.GetAxisRaw("Submit") == 1)
        {
            if(!keyDown)
            {
                animatorFunctions.PlaySound(0);
                submitCounts++;
                animator.SetInteger("index", submitCounts);
            }
            keyDown = true;
        }else
        {
            keyDown = false;
        }
    }

    public void quitPressed()
    {
        GameManager.Instance.sceneTransition.loadScene(mainMenuIndex, null);
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