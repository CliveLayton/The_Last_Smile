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
   
   //audio reference for main music in the background
   [field: SerializeField] public EventReference music { get; private set; }
   
   [field: Header("Player SFX")]
   
   [field: Header("NPC SFX")]
   
   [field: Header("Environment")]
   
   [field: Header("UI SFX")]
   
   [field: SerializeField] public EventReference buttonHovered { get; private set; }
   
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
