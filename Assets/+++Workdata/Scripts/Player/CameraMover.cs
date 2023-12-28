using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMover : MonoBehaviour
{
     #region Inspector

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
    }

    private void OnDisable()
    {
        inputActions.Camera.Disable();
    }

    #endregion

    #region Own Methods/Functions
    
    #endregion

    #region Collision Functions
    
    

    #endregion

    #region Input CallbackContext Methods
    

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
