using UnityEngine;

public class PauseFunction : MonoBehaviour
{
    bool AllowFunction = true;
    bool isPaused;
    bool keyDown = false;
    bool allowPause;

    [Header("Setup")]

    [SerializeField] GameObject pauseMenuObject;
    GameManager gameManager;
    AnimatorFunctions animatorFunctions;

    [Header("Audio")]

    [SerializeField] int pauseInIndex = 6;
    [SerializeField] int pauseOutIndex = 7;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        animatorFunctions = GetComponent<AnimatorFunctions>();
    }

    private void Update()
    {
        if (gameManager.gameIsWon) return;
        if (!allowPause) return;

        if (Input.GetAxisRaw("Pause") == 1)
        {
            if (!keyDown)
            {
                keyDown = true;
                if (AllowFunction)
                {
                    changePauseState();
                }
            }
        }
        else
        {
            keyDown = false;
        }
    }

    public void changePauseState()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            gameManager.changeTimeScale(0);
            animatorFunctions.PlaySound(pauseInIndex);
        }
        else
        {
            gameManager.changeTimeScale(1);
            animatorFunctions.PlaySound(pauseOutIndex);
        }

        pauseMenuObject.SetActive(isPaused);
    }

    public void changeAllowFunction(bool selectedValue)
    {
        AllowFunction = selectedValue;
    }

    public void resetPauseMenu(bool inGame)
    {
        pauseMenuObject.SetActive(false);
        isPaused = false;
        allowPause = inGame;
        changeAllowFunction(inGame);
    }
}
