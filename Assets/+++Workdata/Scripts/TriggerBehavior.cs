using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBehavior : MonoBehaviour
{
    //this is the guid with which we identify the individual objects.
    //make sure they are all unique, or two objects data will be saved and loaded as the same object!
    //We set this value by code in the OnValidate-Function
    [SerializeField] private string uniqueGuid;

    //In this class, we save all variables that we want to persist over play sessions.
    [Serializable]
    public class Data
    {
        public bool isActive;
    }

    [SerializeField] private Data data;

    private bool setActive = true;
    public UnityEvent triggerEnterEvent;
    public UnityEvent triggerExitEvent;
    private DialogueHUD dialogueHUD;
    private BoxCollider2D col;
    //in game UI script
    private InGameUI openMenuText;

    private void Awake()
    {
        //We try to get the object data from the GameStateManager.
        Data loadedData = GameStateManager.instance.data.GetTrigggerBehaviorData(uniqueGuid);
        
        if (loadedData != null)
        {
            //if the GameStateManager returned a data object, we set our values from that
            data = loadedData;
            SetupFromData();
        }
        else
        {
            //if nothing was given back, there was no data saved for this enemy, so we add the current values.
            GameStateManager.instance.data.AddTriggerBehavior(uniqueGuid, data);
            data.isActive = true;
        }

        col = GetComponent<BoxCollider2D>();
        col.enabled = setActive;
        dialogueHUD = GameObject.FindGameObjectWithTag("DialogHUD").GetComponent<DialogueHUD>();
        openMenuText = GameObject.FindGameObjectWithTag("InGameHUD").GetComponent<InGameUI>();
    }
    
    private void SetupFromData()
    {
        setActive = data.isActive;
    }

    /// <summary>
    /// Sets the main trigger object on or off
    /// </summary>
    /// <param name="state">bool</param>
    public void SetTriggerObjectActive(bool state)
    {
        data.isActive = state;
        col.enabled = state;
    }

    /// <summary>
    /// sets the dialog box active 
    /// </summary>
    /// <param name="dialogueBoxActive">bool</param>
    public void SetDialogueBoxActive(bool dialogueBoxActive)
    {
        dialogueHUD.SetDialogueBox(dialogueBoxActive);
    }

    /// <summary>
    /// sets the dialog text, this need to be done first before activating the dialog box
    /// </summary>
    /// <param name="inkPath">string</param>
    public void SetDialogueText(string inkPath)
    {
        dialogueHUD.SetDialogueBoxText(inkPath);
    }

    /// <summary>
    /// the text object for open menu after puzzle1
    /// </summary>
    /// <param name="state">bool</param>
    public void SetOpenMenuText(bool state)
    {
        openMenuText.EnableOpenMenuText(state);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            triggerEnterEvent?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            triggerExitEvent?.Invoke();
        }
    }
    
    //OnValidate is called by unity in the editor only, whenever something within the scene changes
    //we use this to set a new unique guid when we detect that it is empty.
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(gameObject.scene.name))
        {
            //if there scene name saved, it means that the game object is in "prefab mode".
            //it is not yet instantiated (meaning an instance within our scene), so it should not have a
            //unique identifier yet.
            uniqueGuid = "";
        }
        else if (string.IsNullOrEmpty(uniqueGuid))
        {
            //if there is not yet an guid set, lets set a new one.
            uniqueGuid = System.Guid.NewGuid().ToString();
        }
    }
}
