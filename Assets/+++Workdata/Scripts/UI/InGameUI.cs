using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    //all objects in the InGameHUD
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject characterMenu;
    [SerializeField] private GameObject photoMenu;
    [SerializeField] private GameObject mapButton;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject openMenuText;
    [SerializeField] private GameObject northburrySignPicture;
    [SerializeField] private GameObject bankPicture;
    [SerializeField] private GameObject transporterPicture;
    [SerializeField] private GameObject flowerPicture;

    //all collectible object data in the InGameHUD
    [SerializeField] private CollectableData bankData;
    [SerializeField] private CollectableData transporterData;
    [SerializeField] private CollectableData northburrySignData;
    [SerializeField] private CollectableData mapData;
    [SerializeField] private CollectableData flowerData;

    //objects for the current position animation on the map
    [SerializeField] private List<GameObject> onMapPositions;
    
    //buttons to open and close the journal
    [SerializeField] private Button openMenuButton;
    [SerializeField] private Button useCameraButton;

    private PlayerController player;
    
    private GameInput inputActions;

    public bool menuActive = true;

    private void Awake()
    {
        inputActions = new GameInput();
    }

    private void Start()
    {
        GameStateManager.instance.onStateChanged += OnStateChange;
        if(GameStateManager.instance.currentState == GameStateManager.GameState.InMainMenu) 
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
        OpenMenuSound();
        
        if (context.performed && menuActive && (GameStateManager.instance.currentState != GameStateManager.GameState.InMainMenu)
                              && (inGameMenu.activeSelf == false))
        {
            OpenIngameUI();
            openMenuButton.interactable = false;
            useCameraButton.interactable = false;
            openMenuText.SetActive(false);
        }
        else if (context.performed && (GameStateManager.instance.currentState != GameStateManager.GameState.InMainMenu)
                                   && (inGameMenu.activeSelf == true))
        {
            openMenuButton.interactable = true;
            useCameraButton.interactable = true;
            CloseIngameUI();
        }
    }

    private void OnStateChange(GameStateManager.GameState newState)
    {
        //we toggle the availability of the inGame menu whenever the game state changes
        bool isInGame = newState != GameStateManager.GameState.InMainMenu;
        gameObject.SetActive(isInGame);
    }
    
    //this is called via the "go to main menu" button
    public void GoToMainMenu()
    {
        GameStateManager.instance.GoToMainMenu();
        CloseIngameUI();
        AudioManager.instance.CleanUp();
        AudioManager.instance.InitializeMusic(FMODEvents.instance.mainMenuMusic);
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

        if (GameStateManager.instance.data.HasCollectible(flowerData.identifier))
            flowerPicture.SetActive(true);
        else
            flowerPicture.SetActive(false);
        
        if (GameStateManager.instance.data.HasCollectible(bankData.identifier))
            bankPicture.SetActive(true);
        else
            bankPicture.SetActive(false);

        if (GameStateManager.instance.currentState == GameStateManager.GameState.InGame)
        {
            player = FindObjectOfType<PlayerController>().gameObject.GetComponent<PlayerController>();
            player.enabled = false;
        }

        inGameMenu.SetActive(true);
        openMenuButton.interactable = false;
        useCameraButton.interactable = false;
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
        openMenuButton.interactable = true;

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

    public void CameraButtonInteractable(bool state)
    {
        useCameraButton.interactable = state;
    }

    /// <summary>
    /// get the sceneName of the current scene and set the positions
    /// </summary>
    /// <param name="sceneName">string</param>
    public void GetMapPosition(string sceneName)
    {
        switch (sceneName)
        {
            case "Puzzle1":
            case "Puzzle2":
            case "MainMenu":
            case "Level1":
                for (int i = 0; i < onMapPositions.Count; i++)
                {
                    SetMapPosition(0, i);
                }
                break;
            case "Level2":
                for (int i = 0; i < onMapPositions.Count; i++)
                {
                    SetMapPosition(1, i);
                }
                break;
            case "Shop":
                for (int i = 0; i < onMapPositions.Count; i++)
                {
                    SetMapPosition(2, i);
                }
                break;
            case "Level3":
                for (int i = 0; i < onMapPositions.Count; i++)
                {
                    SetMapPosition(3, i);
                }
                break;
            case "Level4":
                for (int i = 0; i < onMapPositions.Count; i++)
                {
                    SetMapPosition(4, i);
                }
                break;
        }
    }

    /// <summary>
    /// sets the current position on the map active and all other position false
    /// </summary>
    /// <param name="map">int</param>
    /// <param name="index">int</param>
    private void SetMapPosition(int map, int index)
    {
        onMapPositions[index].SetActive(false);
        onMapPositions[map].SetActive(true);
    }

    //plays a sound if a button is hovered
    public void ButtonSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonHovered, this.transform.position);
    }

    //plays a sound if another menu open up
    public void TurnPagesSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.turnPages, this.transform.position);
    }

    //plays a sound if the menu opens or close
    public void OpenMenuSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.opemMenu, this.transform.position);
    }
}
