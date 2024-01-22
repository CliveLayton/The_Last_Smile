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
   [SerializeField] private GameObject tutorialCamera;
   //button for open the in game menu
   private GameObject openMenuButton;
   //button for using the camera
   private GameObject useCameraButton;
   //in game ui script
   private InGameUI inGameUI;


   private void Awake()
   {
      openMenuButton = GameObject.Find("OpenMenuButton");
      useCameraButton = GameObject.Find("UseCameraButton");
      inGameUI = GameObject.FindGameObjectWithTag("InGameHUD").GetComponent<InGameUI>();
   }

   /// <summary>
   /// sets the tutorial Object for the camera active
   /// </summary>
   public void ActivateTutorial()
   {
      tutorialCamera.SetActive(true);
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

   /// <summary>
   /// sound for open the camera
   /// </summary>
   public void TakeCameraSound()
   {
      AudioManager.instance.PlayOneShot(FMODEvents.instance.cameraOn, this.transform.position);
   }

   /// <summary>
   /// sound for close the camera
   /// </summary>
   public void TurnCameraOffSound()
   {
      AudioManager.instance.PlayOneShot(FMODEvents.instance.cameraOff, this.transform.position);
   }
}
