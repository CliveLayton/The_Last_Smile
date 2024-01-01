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

    public void StartNewGame()
    {
        GameStateManager.instance.StartNewGame();
    }

    public void LoadGame()
    {
        GameStateManager.instance.LoadFromSave("SaveGame1");
    }

    public void QuitGame()
    {
        print("Quit");
        Application.Quit();
    }
}
