﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeFunctions : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuObject;
    GameManager gameManager;
    bool AllowFunction = true;
    bool isPaused;
    bool keyDown = false;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    private void Update()
    {
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
}
