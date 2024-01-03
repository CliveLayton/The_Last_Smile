using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMover : MonoBehaviour
{
     #region Inspector

     [SerializeField] private Animator cameraAnim;

     [SerializeField] private CinemachineVirtualCamera virtualCameraCamera;

     [SerializeField] private GameObject player;

     [Header("Movement")] 
    
    [SerializeField] private float movementSpeed = 5f;

    #endregion

    #region Input Variables

    private GameInput inputActions;

    private InputAction moveAction;

    #endregion

    #region Private Variables

    private Rigidbody2D rb;

    private BoxCollider2D col;

    public bool inSnapPosition = false;

    private InGameUI inGameUI;

    /// <summary>
    /// current movement input of the player
    /// </summary>
    private Vector2 moveInput;

    #endregion

    #region Unity Event Functions

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

        inputActions = new GameInput();
        moveAction = inputActions.Camera.Move;
        inGameUI = GameObject.FindGameObjectWithTag("InGameHUD").GetComponent<InGameUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Movement();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        inputActions.Camera.Enable();

        inputActions.Camera.Snap.performed += Snap;
        inputActions.Camera.Leave.performed += Leave;
    }

    private void OnDisable()
    {
        inputActions.Camera.Disable();

        inputActions.Camera.Snap.performed -= Snap;
        inputActions.Camera.Leave.performed -= Leave;
    }

    #endregion

    #region Own Methods/Functions
    
    #endregion

    #region Collision Functions

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SnapPosition"))
        {
            inSnapPosition = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SnapPosition"))
        {
            inSnapPosition = false;
        }
    }

    #endregion

    #region Input CallbackContext Methods

    private void Snap(InputAction.CallbackContext context)
    {
        if (context.performed && !inSnapPosition)
        {
            cameraAnim.SetTrigger("Snap");
        }
        else if (context.performed && inSnapPosition)
        {
            cameraAnim.SetTrigger("Snap");
            inGameUI.EnableNorthburrySignPicture(true);
        }
    }

    private void Leave(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            virtualCameraCamera.Priority = 9;
            player.gameObject.SetActive(true);
            cameraAnim.SetTrigger("FadeOut");
            this.gameObject.SetActive(false);
        }
    }

    #endregion

    #region Movement

    private void Movement()
    {
        if (moveInput != Vector2.zero)
        {
            rb.AddForce(moveInput * movementSpeed, ForceMode2D.Force);
        }
        else if (moveInput == Vector2.zero)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    #endregion
}
