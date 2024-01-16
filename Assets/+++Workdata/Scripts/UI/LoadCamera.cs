using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class LoadCamera : MonoBehaviour
{
   //Cinemachine Virtual Camera for the camera mode
   [SerializeField] private CinemachineVirtualCamera cameraCam;
   [SerializeField] private PlayerController player;
   [SerializeField] private CameraMover camMover;
   private GameObject openMenuButton;
   private GameObject useCameraButton;
   private InGameUI inGameUI;


   private void Awake()
   {
      openMenuButton = GameObject.Find("OpenMenuButton");
      useCameraButton = GameObject.Find("UseCameraButton");
      inGameUI = GameObject.FindGameObjectWithTag("InGameHUD").GetComponent<InGameUI>();
   }

   /// <summary>
   /// sets the button to open the menu active and change the bool to open the pause menu to true
   /// </summary>
   public void SetOpenMenuButtonActive()
   {
      openMenuButton.SetActive(true);
      useCameraButton.SetActive(true);
      inGameUI.menuActive = true;
   }

   /// <summary>
   /// sets the button to open the menu inactive and change the bool to open the pause menu to false
   /// </summary>
   public void SetOpenMenuButtonInActive()
   {
      openMenuButton.SetActive(false);
      useCameraButton.SetActive(false);
      inGameUI.menuActive = false;
   }
   
   /// <summary>
   /// changes the priority of the virtual camera of the camera mode
   /// </summary>
   /// <param name="priority">int</param>
   public void SetCameraPriority(int priority)
   {
      cameraCam.Priority = priority;
   }

   public void SetPlayerActive()
   {
      player.enabled = true;
   }

   public void SetPlayerInActive()
   {
      player.enabled = false;
   }

   public void SetCameraMoverActive()
   {
      camMover.enabled = true;
   }

   public void SetCameraMoverInActive()
   {
      camMover.enabled = false;
   }
}
