using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class DragDrop : Selectable, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    /// <summary>
    /// bool for using the exact image pixels to click on or not 
    /// </summary>
    [SerializeField] private bool useExactImagePixels = false;

    public static event Action OnDragStartEvent;
    public static event Action OnDragEndEvent;
    
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    
    //input variables
    private GameInput inputActions;
    private InputAction moveAction;
    private Vector3 moveInput;

    [SerializeField] private float puzzleMoveSpeed = 200f;
    
    protected override void Awake()
    {
        inputActions = new GameInput();
        moveAction = inputActions.Player.MovePuzzle;
        
        base.Awake();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (useExactImagePixels)
            GetComponent<Image>().alphaHitTestMinimumThreshold = .5f;
    }

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector3>();

        if (!interactable)
            return;
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            transform.position += moveInput.normalized * (puzzleMoveSpeed * Time.deltaTime);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        inputActions.Enable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        
        inputActions.Disable();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        if (OnDragStartEvent != null)
            OnDragStartEvent();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (OnDragEndEvent != null)
            OnDragEndEvent();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        
        //get the exact mouse position
        Vector2 mouseAlpha = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
        Vector3[] worldCorners = new Vector3[4];
        ((RectTransform)rectTransform.parent).GetWorldCorners(worldCorners);
        
        Vector3 dragPosition = new Vector3();
        dragPosition.x = Mathf.Lerp(worldCorners[0].x, worldCorners[2].x, mouseAlpha.x);
        dragPosition.y = Mathf.Lerp(worldCorners[0].y, worldCorners[2].y, mouseAlpha.y);
        
        //sets the position of the gamobject to the exact position of the mouse
        rectTransform.position = dragPosition;
    }
}
