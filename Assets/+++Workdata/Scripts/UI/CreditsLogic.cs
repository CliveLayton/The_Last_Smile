using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreditsLogic : MonoBehaviour
{
    private GameInput inputActions;

    //in game menu script
    private GameObject inGameUI;

    //bool to check if player can skip the credits
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

    //if the allowSkip is true, loads main menu and set allowSkip back to false
    void SkipCredits(InputAction.CallbackContext context)
    {
        if (context.performed && allowSkip)
        {
            allowSkip = false;
            GameStateManager.instance.GoToMainMenu();
        }
            
    }

    //loads the main menu
    public void EndCredits()
    {
        GameStateManager.instance.GoToMainMenu();
    }

    /// <summary>
    /// waits 2 seconds and turn allowSkip to true
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitforSkip()
    {
        yield return new WaitForSeconds(2f);
        allowSkip = true;
    }
}
