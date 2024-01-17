using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public EventReference musicName;

    private void Awake()
    {
        AudioManager.instance.CleanUp();
        AudioManager.instance.InitializeMusic(FMODEvents.instance.mainMenuMusic);
    }

    /// <summary>
    /// just load the normal game as a new game
    /// </summary>
    public void StartNewGame()
    {
        AudioManager.instance.CleanUp();
        AudioManager.instance.InitializeMusic(musicName);
        GameStateManager.instance.StartNewGame();
    }

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

    public void LoadGame2()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.loadGameButton, this.transform.position);
        GameStateManager.instance.LoadFromSave("SaveGame2");
    }

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

    public void ButtonSelectedSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonHovered, this.transform.position);
    }
}
