using System;
using System.Collections;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public string sceneName;

    public EventReference musicName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //clean up music and initialize the music for the next scene
            AudioManager.instance.CleanUp();
            AudioManager.instance.InitializeMusic(musicName);
            GameStateManager.instance.LoadNewGameplayScene(sceneName);
        }
    }

    /// <summary>
    /// switch scene to the new scene
    /// </summary>
    /// <param name="sceneToLoad">string</param>
    public void SwitchScene(string sceneToLoad)
    {
        AudioManager.instance.CleanUp();
        AudioManager.instance.InitializeMusic(musicName);
        GameStateManager.instance.LoadNewGameplayScene(sceneToLoad);
    }
}
