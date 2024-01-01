using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameStateManager.instance.LoadNewGameplayScene(sceneName);
        }
    }
    
    public void SwitchScene(string sceneToLoad)
    {
        GameStateManager.instance.LoadNewGameplayScene(sceneToLoad);
    }
}
