using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject item;
    private CanvasGroup canvasGroup;
    private PuzzleLogic puzzleLogic;
    
    //input variables
    private GameInput inputActions;
    private bool insertPuzzle;

    private void Awake()
    {
        inputActions = new GameInput();

        puzzleLogic = GameObject.FindGameObjectWithTag("PuzzleCheck").GetComponent<PuzzleLogic>();
        //sets raycast false on the slot image
        GetComponent<Image>().raycastTarget = false;
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.EnterPuzzle.performed += EnterPuzzle;
        inputActions.Player.EnterPuzzle.canceled += EnterPuzzle;
        
        DragDrop.OnDragStartEvent += OnAnyDragStarted;
        DragDrop.OnDragEndEvent += OnAnyDragStopped;
    }

    private void OnDisable()
    {
        inputActions.Disable();

        inputActions.Player.EnterPuzzle.performed -= EnterPuzzle;
        inputActions.Player.EnterPuzzle.canceled -= EnterPuzzle;
        
        DragDrop.OnDragStartEvent -= OnAnyDragStarted;
        DragDrop.OnDragEndEvent -= OnAnyDragStopped;
    }

    void EnterPuzzle(InputAction.CallbackContext context)
    {
        if (context.performed)
            insertPuzzle = true;
        else if (context.canceled)
            insertPuzzle = false;
    }

    private void OnAnyDragStarted()
    {
        //sets raycast true so items can find the slot
        GetComponent<Image>().raycastTarget = true;
    }

    private void OnAnyDragStopped()
    {
        //sets raycast false so you can drag the items and the slot is not in the way blocking the items
        GetComponent<Image>().raycastTarget = false;
    }

    private void Update()
    {
        if (insertPuzzle)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
                return;
            //get current item if one is selected
            var droppedItem = EventSystem.current.currentSelectedGameObject.GetComponent<DragDrop>();
            if (droppedItem == null || !droppedItem.interactable)
                return;
            
            if (Vector2.Distance(droppedItem.transform.position, transform.position) < 40)
            {
                //drop the current item to the position of the slot
                droppedItem.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                //if the item is correct, count corrct puzzle 1 up
                //set interactable off and select next item
                if (droppedItem.gameObject == item)
                {
                    puzzleLogic.CountCorrectPuzzles(1);
                    droppedItem.interactable = false;
                    //droppedItem.FindSelectableOnDown().Select();
                    if (droppedItem.FindSelectableOnDown())
                    {
                        droppedItem.FindSelectableOnDown().Select();
                    }
                    else if (!droppedItem.FindSelectableOnDown())
                    {
                        droppedItem.FindSelectableOnUp().Select();
                    }
                }
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        //if the item is not the correct item just place it in the slot
        if (eventData.pointerDrag != null)
        {
            //sets the item to the position of the slot 
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                GetComponent<RectTransform>().anchoredPosition;
        }

        //if the item is correct, place it in the slot, count 1 up for correct puzzle and make it no longer interactable
        if (eventData.pointerDrag.gameObject == item)
        {
            canvasGroup = item.GetComponent<CanvasGroup>();
            StartCoroutine(EndDragDrop());
            //counts 1 up in correct puzzle
            puzzleLogic.CountCorrectPuzzles(1);
        }
    }

    private IEnumerator EndDragDrop()
    {
        yield return new WaitForSeconds(0.1f);
        //sets the raycast off, so item is no longer interactable
        canvasGroup.blocksRaycasts = false;
    }
}
