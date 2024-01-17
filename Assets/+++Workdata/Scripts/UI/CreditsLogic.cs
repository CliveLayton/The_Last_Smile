using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreditsLogic : MonoBehaviour
{
    private GameInput inputActions;

    private void Awake()
    {
        inputActions = new GameInput();
        
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
            GameStateManager.instance.LoadNewGameplayScene("MainMenu");
    }

    public void EndCredits()
    {
        GameStateManager.instance.LoadNewGameplayScene("MainMenu");
    }
}
