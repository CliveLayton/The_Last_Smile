using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHUD : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private DialoguePlayerTest dialoguePlayerTest;
    

    public void SetDialogueBox(bool state)
    {
        dialogueBox.SetActive(state);
    }

    public void SetDialogueBoxText(string inkPath)
    {
        dialoguePlayerTest.inkPath = inkPath;
    }
}
