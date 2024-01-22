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
    [SerializeField] private GameObject winScreenButton;
    
    public bool isCorrect = false;

    private void Awake()
    {
        EventSystem.current.SetSelectedGameObject(puzzleRing);
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            return;
        //get current item if one is selected
        var droppedItem = EventSystem.current.currentSelectedGameObject.GetComponent<DragDrop>();
        if (droppedItem == null || !droppedItem.interactable)
            return;

        if (Vector2.Distance(droppedItem.transform.position, transform.position) < 40)
        {
            isCorrect = true;
        }
        else
        {
            isCorrect = false;
        }
    }

    private void OnEnable()
    {
        DragDrop.OnDragStartEvent += OnDragStart;
    }

    private void OnDisable()
    {
        DragDrop.OnDragStartEvent -= OnDragStart;
    }

    /// <summary>
    /// if the ring gets drag turn isCorrect back to false
    /// </summary>
    private void OnDragStart()
    {
        isCorrect = false;
    }

    public void ConfirmAnswer()
    {
        if (isCorrect)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.puzzleSolved, this.transform.position);
            winScreen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(winScreenButton);
        }
        else if (!isCorrect)
        {
            incorrectText.SetActive(true);
            StartCoroutine(DisableIncorrectText());
        }
    }

    /// <summary>
    /// wait for 0.5 seconds then srts incorrect text off
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// sound for button selected 
    /// </summary>
    public void ButtonSelectedSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonHovered, this.transform.position);
    }

    /// <summary>
    /// sound for button is pressed
    /// </summary>
    public void ButtonPressedSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonPressed, this.transform.position);
    }
}
