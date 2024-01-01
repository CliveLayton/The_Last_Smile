using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject openMenuButton;
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject characterMenu;
    [SerializeField] private GameObject photoMenu;
    [SerializeField] private GameObject mapButton;

    private GameInput inputActions;

    private void Awake()
    {
        inputActions = new GameInput();
    }

    private void Start()
    {
        GameStateManager.instance.onStateChanged += OnStateChange;
        gameObject.SetActive(false);
    }
    
    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.UI.PauseGame.performed += PauseGame;
    }

    private void OnDisable()
    {
        inputActions.Disable();

        inputActions.UI.PauseGame.performed -= PauseGame;
    }

    void PauseGame(InputAction.CallbackContext context)
    {
        if (context.performed && (GameStateManager.instance.currentState == GameStateManager.GameState.InGame)
                              && (inGameMenu.activeSelf == false))
        {
            OpenIngameUI();
        }
        else if (context.performed && (GameStateManager.instance.currentState == GameStateManager.GameState.InGame)
                                   && (inGameMenu.activeSelf == true))
        {
            CloseIngameUI();
        }
    }

    private void OnStateChange(GameStateManager.GameState newState)
    {
        //we toggle the availability of the inGame menu whenever the game state changes
        bool isInGame = newState == GameStateManager.GameState.InGame;
        gameObject.SetActive(isInGame);
    }
    
    //this is called via the "go to main menu" button
    public void GoToMainMenu()
    {
        GameStateManager.instance.GoToMainMenu();
        CloseIngameUI();
    }
    
    //this is called via the "save game" button or relevant events in the game
    public void SaveGame()
    {
        GameStateManager.instance.SaveGame("SaveGame1");
    }
    
    //this is called via the button in the upper left corner
    public void OpenIngameUI()
    {
        inGameMenu.SetActive(true);
        openMenuButton.SetActive(false);
        pauseMenu.SetActive(true);
        optionMenu.SetActive(false);
        characterMenu.SetActive(false);
        photoMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(mapButton);
    }
    
    //this is called via the button in the upper left corner, and by the GameStateManager.
    public void CloseIngameUI()
    {
        inGameMenu.SetActive(false);
        openMenuButton.SetActive(true);
    }
    
    //plays a sound if a button is hovered
    public void ButtonSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonHovered, this.transform.position);
    }
}
