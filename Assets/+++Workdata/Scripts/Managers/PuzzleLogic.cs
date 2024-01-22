using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleLogic : MonoBehaviour
{
    private int correctPuzzle;

    public GameObject WinningScreen;

    [SerializeField] private CollectableData map;

    //first snippet to select
    [SerializeField] private GameObject firstSnippet;
    //the Ok button to close the tutorial text
    [SerializeField] private GameObject continueButton;
    //continue button on the winning screen
    [SerializeField] private GameObject winScreenButton;

    private void Awake()
    {
        correctPuzzle = 0;
        
        EventSystem.current.SetSelectedGameObject(continueButton);
    }

    /// <summary>
    /// counts correct puzzles, if puzzles 9 do PuzzleSolved Method
    /// </summary>
    /// <param name="i">int</param>
    public void CountCorrectPuzzles(int i)
    {
        correctPuzzle += i;

        if (correctPuzzle == 10)
        {
            PuzzleSolved();
        }
    }

    /// <summary>
    /// sets winning screen active, enables map in pausemenu
    /// </summary>
    private void PuzzleSolved()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.puzzleSolved, this.transform.position);
        WinningScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(winScreenButton);
        GameStateManager.instance.data.AddCollectible(map.identifier);
    }

    public void SelectFirstSnippet()
    {
        EventSystem.current.SetSelectedGameObject(firstSnippet);
    }

    //sound for button selected
    public void ButtonSelectedSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonHovered, this.transform.position);
    }
    
    //sound for button is pressed
    public void ButtonPressedSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonPressed, this.transform.position);
    }
}
