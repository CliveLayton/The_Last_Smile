using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public EventReference musicName;

    //new game button object on main canvas
    [SerializeField] private GameObject newGameButton;
    //slot1 button object on load menu
    [SerializeField] private GameObject slot1Button;
    //no button object on exit menu
    [SerializeField] private GameObject noButton;
    //master slider object on option menu
    [SerializeField] private GameObject masterSlider;

    private void Awake()
    {
        AudioManager.instance.CleanUp();
        AudioManager.instance.InitializeMusic(FMODEvents.instance.mainMenuMusic);
        EventSystem.current.SetSelectedGameObject(newGameButton);
    }

    /// <summary>
    /// just load the normal game as a new game, set the music for level1
    /// </summary>
    public void StartNewGame()
    {
        AudioManager.instance.CleanUp();
        AudioManager.instance.InitializeMusic(musicName);
        GameStateManager.instance.StartNewGame();
    }

    //loads the credits scene
    public void StartCredits()
    {
        LoadSceneManager.instance.SwitchScene("Credits");
    }

    /// <summary>
    /// load the SaveGame1 Data
    /// </summary>
    public void LoadGame()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.loadGameButton, this.transform.position);
        GameStateManager.instance.LoadFromSave("SaveGame1");
    }

    /// <summary>
    /// laod the SaveGame2 Data
    /// </summary>
    public void LoadGame2()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.loadGameButton, this.transform.position);
        GameStateManager.instance.LoadFromSave("SaveGame2");
    }

    /// <summary>
    /// load the SaveGame3 Data
    /// </summary>
    public void LoadGame3()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.loadGameButton, this.transform.position);
        GameStateManager.instance.LoadFromSave("SaveGame3");
    }

    /// <summary>
    /// quit the application
    /// </summary>
    public void QuitGame()
    {
        print("Quit");
        Application.Quit();
    }

    /// <summary>
    /// set selected button for main menu
    /// </summary>
    public void SetSelectedButtonMain()
    {
        EventSystem.current.SetSelectedGameObject(newGameButton);
    }
    
    /// <summary>
    /// set selected button for options menu
    /// </summary>
    public void SetSelectedButtonOptions()
    {
        EventSystem.current.SetSelectedGameObject(masterSlider);
    }

    /// <summary>
    /// set selected button for load game screen
    /// </summary>
    public void SetSelectedButtonLoad()
    {
        EventSystem.current.SetSelectedGameObject(slot1Button);
    }

    /// <summary>
    /// set selected button for quit menu
    /// </summary>
    public void SetSelectedButtonQuit()
    {
        EventSystem.current.SetSelectedGameObject(noButton);
    }

    /// <summary>
    /// plays sound for button selected
    /// </summary>
    public void ButtonSelectedSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonHovered, this.transform.position);
    }
    
    /// <summary>
    /// plays sound for button is pressed
    /// </summary>
    public void ButtonPressedSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonPressed, this.transform.position);
    }

    /// <summary>
    /// plays sound for switching scenes at start
    /// </summary>
    public void SwitchMenuBeginSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.switchesMenuBegin, this.transform.position);
    }

    /// <summary>
    /// plays sound for switching scenes at end
    /// </summary>
    public void SwitchMenuEndSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.switchMenuEnd, this.transform.position);
    }

    /// <summary>
    /// plays sound for new game button is pressed
    /// </summary>
    public void NewGameButtonSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.newGameButtonPressed, this.transform.position);
    }

    /// <summary>
    /// plays sound for new game animation
    /// </summary>
    public void NewGameAnimationSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.newGameAnimationSound, this.transform.position);
    }
}
