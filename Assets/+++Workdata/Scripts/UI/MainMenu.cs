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

    public void PlayGame(int sceneIndex)
    {
        StartCoroutine(GoToGame(sceneIndex));
    }

    public void QuitGame()
    {
        print("Quit");
        Application.Quit();
    }

    private IEnumerator GoToGame(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
