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

    public void ButtonSelectedSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonHovered, this.transform.position);
    }

    public void ButtonPressedSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonPressed, this.transform.position);
    }
}
