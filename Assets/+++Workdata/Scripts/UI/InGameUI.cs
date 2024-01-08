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
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject openMenuText;
    [SerializeField] private GameObject northburrySignPicture;
    [SerializeField] private GameObject transporterPicture;

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
            openMenuText.SetActive(false);
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

    /// <summary>
    /// sets the map in the menu active or not
    /// </summary>
    /// <param name="state">bool</param>
    public void EnableMap(bool state)
    {
        map.SetActive(state);
    }

    /// <summary>
    /// sets the transporter picture on the menu active or not
    /// </summary>
    /// <param name="state"></param>
    public void EnableTransporterPicture(bool state)
    {
        transporterPicture.SetActive(state);
    }

    /// <summary>
    /// enables the northburry sign picture in the picutre menu on or off
    /// </summary>
    /// <param name="state">boolean</param>
    public void EnableNorthburrySignPicture(bool state)
    {
        northburrySignPicture.SetActive(state);
    }

    /// <summary>
    /// sets the open menu text active or not
    /// </summary>
    /// <param name="state">bool</param>
    public void EnableOpenMenuText(bool state)
    {
        openMenuText.SetActive(state);
    }

    //plays a sound if a button is hovered
    public void ButtonSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonHovered, this.transform.position);
    }
}
