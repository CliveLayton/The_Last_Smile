using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
   [field: Header("Ambience")]
   
   //audio reference for ambience music
   [field: SerializeField] public EventReference ambience { get; private set; }
   
   [field: Header("Music")]
   
   //audio reference for main menu music
   [field: SerializeField] public EventReference mainMenuMusic { get; private set; }
   
   //audio reference for level1 music
   [field: SerializeField] public EventReference level1Music { get; private set; }
   
   //audio reference for northburry music
   [field: SerializeField] public EventReference northburryMusic { get; private set; }
   
   //audio reference for shop music
   [field: SerializeField] public EventReference shopMusic { get; private set; }
   
   //audio reference for moonbrightforest music
   [field: SerializeField] public EventReference moonbrightForestMusic { get; private set; }
   
   //audio reference for old cabin music
   [field: SerializeField] public EventReference oldCabinMusic { get; private set; }
   
   //audio reference for puzzle music
   [field: SerializeField] public EventReference puzzleMusic { get; private set; }

   //audio reference for credit music
   [field: SerializeField] public EventReference creditMusic { get; private set; }

   [field: Header("Player SFX")]
   
   //audio reference for player footsteps outside
   [field: SerializeField] public EventReference playerFootstepsOutside { get; private set; }
   
   //audio reference for player footsteps inside
   [field: SerializeField] public EventReference playerFootstepsInside { get; private set; }
   
   [field: Header("NPC SFX")]
   
   [field: Header("Environment")]
   
   [field: Header("UI SFX")]
   
   //audio reference for button hovered sound
   [field: SerializeField] public EventReference buttonHovered { get; private set; }
   
   //audio reference for dialgoue text sound
   [field: SerializeField] public EventReference dialogueText { get; private set; }
   
   //audio reference for toggle on
   [field: SerializeField] public EventReference toggleButtonOn { get; private set; }
   
   //audio reference for toggle off
   [field: SerializeField] public EventReference toggleButtonOff { get; private set; }
   
   //audio reference for puzzle solved 
   [field: SerializeField] public EventReference puzzleSolved { get; private set; }
   
   //audio reference for load game 
   [field: SerializeField] public EventReference loadGameButton { get; private set; }
   
   //audio reference for open menu
   [field: SerializeField] public EventReference opemMenu { get; private set; }
   
   //audio reference for turn pages
   [field: SerializeField] public EventReference turnPages { get; private set; }
   
   public static FMODEvents instance { get; private set; }

   private void Awake()
   {
      if (instance != null)
      {
         Debug.LogError("Found more than one FMOD Events instance in the scene");
      }

      instance = this;
   }
}
