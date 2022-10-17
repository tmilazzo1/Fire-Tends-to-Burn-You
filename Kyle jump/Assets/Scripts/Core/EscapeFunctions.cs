using UnityEngine;

public class EscapeFunctions : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuObject;
    GameManager gameManager;
    bool AllowFunction = true;
    bool isPaused;
    bool keyDown = false;
    bool allowPause;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    private void Update()
    {
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
        }
        else
        {
            gameManager.changeTimeScale(1);
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
