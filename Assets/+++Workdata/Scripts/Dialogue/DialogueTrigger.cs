using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//not used in this build/game
public class DialogueTrigger : MonoBehaviour
{
   #region Variables

   [Header("Visual Cue")] 
   
   [SerializeField] private GameObject visualCue;

   [Header("Ink JSON")] 
   //link to the inkJSON file
   [SerializeField] private string inkPath;

   private DialogueManager dialogueManager;

   public GameObject player;

   #endregion

   #region Functions

   private void Awake()
   {
      visualCue.SetActive(true);

      dialogueManager = FindObjectOfType<DialogueManager>();
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         //if player is in range and dialogue is not playing enable visual cue
         if (!dialogueManager.dialogueIsPlaying)
         {
            visualCue.SetActive(true);

            dialogueManager.startDialogue = true;
            dialogueManager.dialoguePath = inkPath;
         }
      }
   }

   private void OnTriggerExit2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         dialogueManager.startDialogue = false;
         visualCue.SetActive(false);
      }
   }

   #endregion
}
