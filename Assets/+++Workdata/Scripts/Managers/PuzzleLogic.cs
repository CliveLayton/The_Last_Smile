using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLogic : MonoBehaviour
{
    private int correctPuzzle;

    public GameObject WinningScreen;

    [SerializeField] private CollectableData map;

    private void Awake()
    {
        correctPuzzle = 0;
    }

    /// <summary>
    /// counts correct puzzles, if puzzles 9 do PuzzleSolved Method
    /// </summary>
    /// <param name="i">int</param>
    public void CountCorrectPuzzles(int i)
    {
        correctPuzzle += i;

        if (correctPuzzle == 9)
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
        GameStateManager.instance.data.AddCollectible(map.identifier);
    }
}
