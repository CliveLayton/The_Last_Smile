using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    //the linked collectible data, which is a ScriptableObject.
    //each has to be uniquely named, because with that unique name the data is identified within the
    //save game.
    [SerializeField] private CollectableData data;

    private InGameUI inGameUI;

    private void Awake()
    {
        inGameUI = GameObject.FindGameObjectWithTag("InGameHUD").GetComponent<InGameUI>();
        
        //When loading the scene, we destroy the collectible, if it was already saved as collected.
        if (GameStateManager.instance.data.HasCollectible(data.identifier))
            Destroy(gameObject);
    }

    public void CollectPolaroid()
    {
        //We add the unique name of the collectible to the data, once the collectible is collected.
        //This means that it will be saved as well.
        GameStateManager.instance.data.AddCollectible(data.identifier);
        Destroy(gameObject);
    }

    //OnValidate is only called in editor when something about this script changed.
    //Here, we only change the game object name to represent what pickup is linked, 
    //without us having to change the name by hand
    private void OnValidate()
    {
        if (data == null)
            name = "[Collectible] -unasigned-";
        else
            name = "[Collectible] " + data.identifier;
    }
}
