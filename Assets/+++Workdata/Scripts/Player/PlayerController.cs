using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Serializable]
    public class Data
    {
        public string lastScene;
        public Dictionary<string, SaveableVector3> positionsBySceneName = new Dictionary<string, SaveableVector3>();
    }
    
    #region Inspector

    [SerializeField] private Data data;
    
    [Header("Movement")] 
    
    [SerializeField] private float movementSpeed = 4f;
    
    [SerializeField] private float speedChangeRate = 10f;
    
    [SerializeField] private float sprintSpeed = 9f;
    
    [SerializeField] private float jumpPower = 12f;

    [Header("GroundCheck")] 
    [SerializeField] private Vector3 groundCheckPos;

    [SerializeField] private Vector2 groundCheckSize;

    [SerializeField] private LayerMask groundLayer;

    #endregion

    #region Input Variables

    private GameInput inputActions;

    private InputAction moveAction;

    #endregion

    #region Private Variables

    private Rigidbody2D rb;

    private BoxCollider2D col;

    public bool isRunning;

    public bool isInteracting;

    public bool isJumping;

    public bool inSequence = false;

    /// <summary>
    /// bool for player is moving left
    /// </summary>
    public bool leftMovement;

    public bool isGrounded;

    /// <summary>
    /// 1 or -1 to calculate left or right movement
    /// </summary>
    private int directionMultiply;

    /// <summary>
    /// last movement input of the player
    /// </summary>
    private Vector2 lastMovement;

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
        moveAction = inputActions.Player.Move;
        
        var currentStats = GameStateManager.instance.data.data;
        if (currentStats != null)
        {
            //if a set of exists, that means we loaded a save and can take over those values.
            data = currentStats;
            SetupFromData();
        }
        
        GameStateManager.instance.data.data = data;
    }
    
    private void SetupFromData()
    {
        //because the player can move from scene to scene, we want to load the position for the scene we are currently in.
        //if the player was never in this scene, we keep the default position the prefab is at.
        if (data.positionsBySceneName.TryGetValue(gameObject.scene.name, out var position))
            transform.position = position;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(transform.position + groundCheckPos, groundCheckSize, 0, groundLayer);

        if (!inSequence)
        {
            Movement();   
        }
        else if (inSequence)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
    }
    
    private void LateUpdate()
    {
        //we have to save the current position dependant on the scene the player is in.
        //this way, the position can be retained across multiple scenes, and we can switch back and forth.
        var sceneName = gameObject.scene.name;
        if (!data.positionsBySceneName.ContainsKey(sceneName))
            data.positionsBySceneName.Add(sceneName, transform.position);
        else
            data.positionsBySceneName[sceneName] = transform.position;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Sprint.performed += Run;
        inputActions.Player.Sprint.canceled += Run;

        inputActions.Player.Interact.performed += Interact;

        inputActions.Player.Jump.performed += Jump;
        inputActions.Player.Jump.canceled += Jump;

        inputActions.Player.SesamOpenDoor.performed += SesamOpenDoor;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        
        inputActions.Player.Sprint.performed -= Run;
        inputActions.Player.Sprint.canceled -= Run;

        inputActions.Player.Interact.performed -= Interact;

        inputActions.Player.Jump.performed -= Jump;
        inputActions.Player.Jump.canceled -= Jump;

        inputActions.Player.SesamOpenDoor.performed -= SesamOpenDoor;
    }

    #endregion

    #region Own Methods/Functions
    

    #endregion

    #region Collision Functions
    
    

    #endregion

    #region Input CallbackContext Methods

    void Run(InputAction.CallbackContext context)
    {
        isRunning = context.performed;
    }

    void Interact(InputAction.CallbackContext context)
    {
        isInteracting = context.performed;
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (inSequence)
            return;
        
        if (context.performed && isGrounded)
        {
            isJumping = true;
            rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }

        if (context.canceled)
        {
            isJumping = false;
            if (rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }
    }

    //test build for presentation
    void SesamOpenDoor(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameObject closedDoor = GameObject.FindGameObjectWithTag("ClosedDoor");
            CircleCollider2D openDoor = GameObject.FindGameObjectWithTag("OpenDoor").GetComponentInChildren<CircleCollider2D>();

            closedDoor.gameObject.SetActive(false);
            openDoor.enabled = true;
        }
    }

    #endregion

    #region Movement

    private void Movement()
    {
        float currentSpeed = lastMovement.magnitude;

        if (moveInput.x > 0)
        {
            leftMovement = false;
        }
        else if (moveInput.x < 0)
        {
            leftMovement = true;
        }

        directionMultiply = leftMovement ? -1 : 1;

        float targetSpeed = (moveInput.x == 0 ? 0 : (isRunning ? sprintSpeed : movementSpeed) * moveInput.magnitude);

        if (Mathf.Abs(currentSpeed - targetSpeed) > 0.01f)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, speedChangeRate * Time.deltaTime);
        }
        else
        {
            currentSpeed = targetSpeed;
        }

        rb.velocity = new Vector2(currentSpeed * directionMultiply, rb.velocity.y);

        lastMovement.x = currentSpeed;
    }

    #endregion

    /// <summary>
    /// draws a wirecube in unity to visualize the groundcheck
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + groundCheckPos, groundCheckSize);
    }
}
