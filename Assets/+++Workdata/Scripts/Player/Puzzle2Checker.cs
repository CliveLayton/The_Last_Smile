using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle2Checker : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject incorrectText;
    [SerializeField] private GameObject puzzleRing;
    
    public bool isCorrect = false;

    private void OnEnable()
    {
        DragDrop.OnDragStartEvent += OnDragStart;
    }

    private void OnDisable()
    {
        DragDrop.OnDragStartEvent -= OnDragStart;
    }

    private void OnDragStart()
    {
        isCorrect = false;
    }

    public void ConfirmAnswer()
    {
        if (isCorrect)
        {
            winScreen.SetActive(true);
        }
        else if (!isCorrect)
        {
            incorrectText.SetActive(true);
            StartCoroutine(DisableIncorrectText());
        }
    }

    private IEnumerator DisableIncorrectText()
    {
        yield return new WaitForSeconds(0.5f);
        incorrectText.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.gameObject == puzzleRing)
        {
            isCorrect = true;
        }
    }

}
