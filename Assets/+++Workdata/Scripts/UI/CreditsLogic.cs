using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreditsLogic : MonoBehaviour
{
    private GameInput inputActions;

    private GameObject inGameUI;

    private bool allowSkip;

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

        StartCoroutine(WaitforSkip());
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
        if (context.performed && allowSkip)
        {
            allowSkip = false;
            GameStateManager.instance.GoToMainMenu();
        }
            
    }

    public void EndCredits()
    {
        GameStateManager.instance.GoToMainMenu();
    }

    private IEnumerator WaitforSkip()
    {
        yield return new WaitForSeconds(2f);
        allowSkip = true;
    }
}
