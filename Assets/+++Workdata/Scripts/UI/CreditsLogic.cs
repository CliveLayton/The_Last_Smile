using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreditsLogic : MonoBehaviour
{
    private GameInput inputActions;

    private GameObject inGameUI;

    private void Awake()
    {
        inputActions = new GameInput();
        if (GameStateManager.instance.currentState != GameStateManager.GameState.InMainMenu)
        {
            inGameUI = GameObject.FindGameObjectWithTag("InGameHUD").gameObject;
            inGameUI.SetActive(false);
        }

        AudioManager.instance.CleanUp();
        AudioManager.instance.InitializeMusic(FMODEvents.instance.creditMusic);
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Interact.performed += SkipCredits;
    }

    private void OnDisable()
    {
        inputActions.Disable();

        inputActions.Player.Interact.performed -= SkipCredits;
    }

    void SkipCredits(InputAction.CallbackContext context)
    {
        if(context.performed)
            GameStateManager.instance.GoToMainMenu();
    }

    public void EndCredits()
    {
        GameStateManager.instance.GoToMainMenu();
    }
}
