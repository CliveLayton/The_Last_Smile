using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using TMPro;
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

    private SpriteRenderer sr;

    private Animator playerAnimator;

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

    //audio event instance for player footsteps outside
    private EventInstance playerFootstepsOutside;

    //audio event instance for player footsteps inside
    private EventInstance playerFootstepsInside;

    #endregion

    #region Unity Event Functions

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        
        //initialize sound for the dialogue text for the current scene
        AudioManager.instance.InitializeDialogueSFX(FMODEvents.instance.dialogueText);

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

    private void Start()
    {
        playerFootstepsInside = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootstepsInside);
        playerFootstepsOutside = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootstepsOutside);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(transform.position + groundCheckPos, groundCheckSize, 0, groundLayer);

        if (!inSequence)
        {
            if(LoadSceneManager.instance.sceneLoaded) 
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
       
        AnimateWalk();
        UpdateSound();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Sprint.performed += Run;
        inputActions.Player.Sprint.canceled += Run;

        inputActions.Player.Interact.performed += Interact;
    }

    private void OnDisable()
    {
        rb.velocity = new Vector2(0, 0);
        
        playerAnimator.SetFloat("movementSpeed", 0);
        
        playerFootstepsInside.stop(STOP_MODE.IMMEDIATE);
        playerFootstepsOutside.stop(STOP_MODE.IMMEDIATE);
        
        inputActions.Player.Disable();
        
        inputActions.Player.Sprint.performed -= Run;
        inputActions.Player.Sprint.canceled -= Run;

        inputActions.Player.Interact.performed -= Interact;
    }

    #endregion

    #region Own Methods/Functions
    

    #endregion

    #region Collision Functions

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("NotAvailable") && moveInput.y >= 0.5f)
        {
            var notPartOfDemo = GameObject.Find("NotPartOfDemo").GetComponent<SpriteRenderer>();
            notPartOfDemo.enabled = true;
            StartCoroutine(SetNotPartOfDemoOff(notPartOfDemo));
        }

        if (other.CompareTag("GoToShop") && moveInput.x >= 0.5f)
        {
            var goToShopText = GameObject.Find(("GoToShopText")).GetComponent<SpriteRenderer>();
            goToShopText.enabled = true;
            StartCoroutine(SetNotPartOfDemoOff(goToShopText));
        }

        if (other.CompareTag("GoToCity") && moveInput.x <= 0.5f)
        {
            var goToCityText = GameObject.Find("GoToCityText").GetComponent<SpriteRenderer>();
            goToCityText.enabled = true;
            StartCoroutine(SetNotPartOfDemoOff(goToCityText));
        }
    }

    private IEnumerator SetNotPartOfDemoOff(SpriteRenderer currentObject)
    {
        yield return new WaitForSeconds(1f);
        currentObject.enabled = false;
    }

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

    #endregion

    #region Movement

    private void Movement()
    {
        float currentSpeed = lastMovement.magnitude;

        if (moveInput.x > 0)
        {
            leftMovement = false;
            sr.flipX = false;
        }
        else if (moveInput.x < 0)
        {
            leftMovement = true;
            sr.flipX = true;
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

    #region Animations

    private void AnimateWalk()
    {
        Vector2 velocity = lastMovement;
        float speed = velocity.magnitude;
        
        playerAnimator.SetFloat("movementSpeed", speed);
    }

    #endregion

    private void UpdateSound()
    {
        if (Mathf.Abs(rb.velocity.x) > 1f && GameStateManager.instance.data.loadedSceneName != "Shop")
        {
            //get the playback state
            PLAYBACK_STATE playbackStateOutside;
            playerFootstepsOutside.getPlaybackState(out playbackStateOutside);
            if (playbackStateOutside.Equals(PLAYBACK_STATE.STOPPED))
                playerFootstepsOutside.start();
        }
        else if (Mathf.Abs(rb.velocity.x) > 1f && GameStateManager.instance.data.loadedSceneName == "Shop")
        {
            PLAYBACK_STATE playbackStateInside;
            playerFootstepsInside.getPlaybackState(out playbackStateInside);
            if (playbackStateInside.Equals(PLAYBACK_STATE.STOPPED))
                playerFootstepsInside.start();
        }
        else
        {
            playerFootstepsInside.stop(STOP_MODE.ALLOWFADEOUT);
            playerFootstepsOutside.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
    
    /// <summary>
    /// draws a wirecube in unity to visualize the groundcheck
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + groundCheckPos, groundCheckSize);
    }
}
