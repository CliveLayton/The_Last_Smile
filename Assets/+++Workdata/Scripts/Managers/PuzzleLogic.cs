using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLogic : MonoBehaviour
{
    private int correctPuzzle;

    public GameObject WinningScreen;

    private void Awake()
    {
        correctPuzzle = 0;
    }

    public void CountCorrectPuzzles(int i)
    {
        correctPuzzle += i;

        if (correctPuzzle == 9)
        {
            PuzzleSolved();
        }
    }

    private void PuzzleSolved()
    {
        WinningScreen.SetActive(true);
    }
}
