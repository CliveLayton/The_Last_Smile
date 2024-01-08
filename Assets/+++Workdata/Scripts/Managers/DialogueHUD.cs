using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHUD : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private DialoguePlayerTest dialoguePlayerTest;
    

    /// <summary>
    /// sets the dialog UI active or not
    /// </summary>
    /// <param name="state">boolean</param>
    public void SetDialogueBox(bool state)
    {
        dialogueBox.SetActive(state);
    }

    /// <summary>
    /// sets the story for dialogue
    /// </summary>
    /// <param name="inkPath">string</param>
    public void SetDialogueBoxText(string inkPath)
    {
        dialoguePlayerTest.inkPath = inkPath;
    }
}
