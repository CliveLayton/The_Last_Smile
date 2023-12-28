using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChangeTrigger : MonoBehaviour
{
   [Header("Area")]
   //link to a item in the music area enum
   [SerializeField] private MusicArea area;

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         AudioManager.instance.SetMusicArea(area);
      }
   }
}
