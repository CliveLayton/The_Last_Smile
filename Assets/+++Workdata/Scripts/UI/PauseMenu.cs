using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
   private GameInput inputActions;

   public GameObject pauseMenu;

   public GameObject optionMenu;

   public GameObject characterMenu;

   public GameObject photoMenu;

   public GameObject menuButtons;

   public GameObject player;

   public GameObject mapButton;

   private void Awake()
   {
      inputActions = new GameInput();
      pauseMenu.SetActive(false);
      menuButtons.SetActive(false);
   }

   public void ResumeGame()
   {
      player.SetActive(true);
   }

   public void QuitGame()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
   }

   private void OnEnable()
   {
      inputActions.Enable();

      inputActions.UI.PauseGame.performed += PauseGame;
   }

   private void OnDisable()
   {
      inputActions.Disable();

      inputActions.UI.PauseGame.performed -= PauseGame;
   }

   void PauseGame(InputAction.CallbackContext context)
   {
      if (context.performed && (pauseMenu.activeSelf == false))
      {
         pauseMenu.SetActive(true);
         menuButtons.SetActive(true);
         optionMenu.SetActive(false);
         characterMenu.SetActive(false);
         photoMenu.SetActive(false);
         player.SetActive(false);
         EventSystem.current.SetSelectedGameObject(mapButton);
      }
      else if (context.performed && (pauseMenu.activeSelf == true))
      {
         pauseMenu.SetActive(false);
         menuButtons.SetActive(false);
         player.SetActive(true);
      }
   }

   public void ButtonSound()
   {
      AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonHovered, this.transform.position);
   }
}
