using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CameraInteractable : MonoBehaviour
{
    //this is the guid with which we identify the individual objects.
    //make sure they are all unique, or two objects data will be saved and loaded as the same object!
    //We set this value by code in the OnValidate-Function
    [SerializeField] private string uniqueGuid;

    //In this class, we save all variables that we want to persist over play sessions.
    [Serializable]
    public class Data
    {
        //boolean for gameobject
        public bool isActive;
    }
    
    private bool isInRange;

    public UnityEvent interactAction;

    private GameInput inputActions;

    //bool for gameobject
    private bool setActive = true;

    //the collider for the interaction range
    private CircleCollider2D col;

    private InGameUI inGameUI;

    //the main object of the interactable
    [SerializeField] private GameObject interactable;

    [SerializeField] private Data data;

    private void Awake()
    {
        //We try to get the object data from the GameStateManager.
        Data loadedData = GameStateManager.instance.data.GetCameraTriggerData(uniqueGuid);
        
        if (loadedData != null)
        {
            //if the GameStateManager returned a data object, we set our values from that
            data = loadedData;
            SetupFromData();
        }
        else
        {
            //if nothing was given back, there was no data saved for this enemy, so we add the current values.
            GameStateManager.instance.data.AddCameraInteractable(uniqueGuid, data);
            data.isActive = true;
        }

        col = GetComponent<CircleCollider2D>();
        inputActions = new GameInput();
        interactable.SetActive(setActive);
        inGameUI = GameObject.FindGameObjectWithTag("InGameHUD").GetComponent<InGameUI>();
    }
    
    private void SetupFromData()
    {
        setActive = data.isActive;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inGameUI.CameraButtonInteractable(true);
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inGameUI.CameraButtonInteractable(false);
            isInRange = false;
        }
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Interact.performed += Interact;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Player.Interact.performed -= Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (context.performed && isInRange)
        {
            interactAction.Invoke();
        }
    }

    /// <summary>
    /// sets gameobject active or not
    /// </summary>
    /// <param name="state">boolean</param>
    public void SetObjectActive(bool state)
    {
        setActive = state;
        this.gameObject.SetActive(state);
        data.isActive = state;
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
