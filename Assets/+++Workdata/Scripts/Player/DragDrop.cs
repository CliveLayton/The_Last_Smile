using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    
    protected override void Awake()
    {
        base.Awake();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (useExactImagePixels)
            GetComponent<Image>().alphaHitTestMinimumThreshold = .5f;
    }

    private void Update()
    {
        if (!interactable)
            return;
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            Vector3 direction = new Vector3();
            if (Input.GetKey(KeyCode.LeftArrow))
                direction.x = -1;
            if (Input.GetKey(KeyCode.RightArrow))
                direction.x = 1;
            if (Input.GetKey(KeyCode.UpArrow))
                direction.y = 1;
            if (Input.GetKey(KeyCode.DownArrow))
                direction.y = -1;

            transform.position += direction * (200 * Time.deltaTime);
        }
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
