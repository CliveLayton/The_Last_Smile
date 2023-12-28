using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : Selectable, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    
    protected override void Awake()
    {
        base.Awake();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
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

            transform.position += direction * (80 * Time.deltaTime);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        print("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("OnBeginDrag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
