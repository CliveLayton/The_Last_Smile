using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private MusicArea area;

    public GameObject loadingScreen;

    private void Start()
    {
        AudioManager.instance.SetMusicArea(area);
    }

    /// <summary>
    /// just load the normal game as a new game
    /// </summary>
    public void StartNewGame()
    {
        GameStateManager.instance.StartNewGame();
    }

    /// <summary>
    /// load the SaveGame1 Data
    /// </summary>
    public void LoadGame()
    {
        GameStateManager.instance.LoadFromSave("SaveGame1");
    }

    public void LoadGame2()
    {
        GameStateManager.instance.LoadFromSave("SaveGame2");
    }

    public void LoadGame3()
    {
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
}
