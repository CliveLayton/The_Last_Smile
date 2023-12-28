using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject item;
    private CanvasGroup canvasGroup;
    private PuzzleLogic puzzleLogic;

    private void Awake()
    {
        puzzleLogic = GameObject.FindGameObjectWithTag("PuzzleCheck").GetComponent<PuzzleLogic>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (EventSystem.current.currentSelectedGameObject == null)
                return;
            var droppedIem = EventSystem.current.currentSelectedGameObject.GetComponent<DragDrop>();
            if (droppedIem == null || !droppedIem.interactable)
                return;
            
            if (Vector2.Distance(droppedIem.transform.position, transform.position) < 40)
            {
                droppedIem.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                if (droppedIem.gameObject == item)
                {
                    puzzleLogic.CountCorrectPuzzles(1);
                    droppedIem.interactable = false;
                    droppedIem.FindSelectableOnDown().Select();
                }
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        print("OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                GetComponent<RectTransform>().anchoredPosition;
        }

        if (eventData.pointerDrag.gameObject == item)
        {
            canvasGroup = item.GetComponent<CanvasGroup>();
            StartCoroutine(EndDragDrop());
            puzzleLogic.CountCorrectPuzzles(1);
        }
    }

    private IEnumerator EndDragDrop()
    {
        yield return new WaitForSeconds(0.1f);
        canvasGroup.blocksRaycasts = false;
    }
}
