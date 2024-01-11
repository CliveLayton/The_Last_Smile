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

    [SerializeField] private CollectableData transporterData;
    [SerializeField] private CollectableData northburrySignData;
    [SerializeField] private CollectableData mapData;

    private PlayerController player;
    
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
        if (context.performed && (GameStateManager.instance.currentState != GameStateManager.GameState.InMainMenu)
                              && (inGameMenu.activeSelf == false))
        {
            OpenIngameUI();
            openMenuText.SetActive(false);
        }
        else if (context.performed && (GameStateManager.instance.currentState != GameStateManager.GameState.InMainMenu)
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

    public void SaveGame2()
    {
        GameStateManager.instance.SaveGame("SaveGame2");
    }

    public void SaveGame3()
    {
        GameStateManager.instance.SaveGame("SaveGame3");
    }
    
    //this is called via the button in the upper left corner
    public void OpenIngameUI()
    {
        if(GameStateManager.instance.data.HasCollectible(transporterData.identifier))
            transporterPicture.SetActive(true);
        else
            transporterPicture.SetActive(false);
        
        if(GameStateManager.instance.data.HasCollectible(northburrySignData.identifier))
            northburrySignPicture.SetActive(true);
        else
            northburrySignPicture.SetActive(false);
        
        if(GameStateManager.instance.data.HasCollectible(mapData.identifier))
            map.SetActive(true);
        else
            map.SetActive(false);
        
        if (GameStateManager.instance.currentState == GameStateManager.GameState.InGame)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            player.enabled = false;
        }

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
        
        if(GameStateManager.instance.currentState == GameStateManager.GameState.InGame) 
            player.enabled = true;
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
