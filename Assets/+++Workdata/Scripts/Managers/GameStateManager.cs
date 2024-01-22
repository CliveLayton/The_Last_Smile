using System;
using UnityEngine;

/// <summary>
/// The GameStateManager lies at the heart of our code.
/// Most importantly for this demonstration, it contains the GameData
/// and manages the loading and saving of our save files.
/// Additionally, it manages the loading and unloading of levels, as well as going back to the main menu.
/// </summary>
public class GameStateManager : MonoBehaviour
{
   public static GameStateManager instance;
   
   public const string mainMenuSceneName = "MainMenu";
   public const string level1SceneName = "Level1";

   [SerializeField] private SettingsMenu settingsOptions;

   public enum GameState
   {
      InMainMenu = 0,
      InGame = 1,
      InPuzzle = 2
   }
   
   //this event notifies any objects that need to know about the changing of the game state.
   //for example, the in game ui toggles itself off when we enter the main menu.
   public event Action<GameState> onStateChanged;

   //the current state.
   public GameState currentState { get; private set; } = GameState.InMainMenu;
   
   /// <summary>
   /// This is the current instance of the GameData that saves the entire current state of the game.
   /// When the game gets loaded, all changes get set within this file, and the different objects can
   /// get their current data from this instance of the game data. 
   /// </summary>
   public GameData data = new GameData();

   #region Level2Objects Variables

   /// <summary>
   /// bool for the open door circle collider is active or not 
   /// </summary>
   public bool shopOpenDoorActive = false;

   /// <summary>
   /// bool for the closed door gameobject is active or not
   /// </summary>
   public bool shopClosedDoorActive = true;

   /// <summary>
   /// bool for npc oliver is active in level2 or not
   /// </summary>
   public bool oliverActive = true;

   /// <summary>
   /// bool for truck without wood is active in level2 or not
   /// </summary>
   public bool truckWithoutWood = true;

   /// <summary>
   /// bool for truck with wood is active in level2 or not
   /// </summary>
   public bool truckWithWood = false;

   #endregion

   private void Awake()
   {
      instance = this;
   }
   
   private void Start()
   {
      //when we start the game, we first want to enter the main menu
      GoToMainMenu();
   }

   //called to enter the main menu. Also changes the game state.
   public void GoToMainMenu()
   {
      currentState = GameState.InMainMenu;
      if (onStateChanged != null)
         onStateChanged(currentState);
      LoadSceneManager.instance.SwitchScene(mainMenuSceneName);
   }
   
   //called to start a new game. Also changes the game state.
   public void StartNewGame()
   {
      data = new GameData();
      settingsOptions.LoadSettings();
      currentState = GameState.InGame;
      if (onStateChanged != null)
         onStateChanged(currentState);
      LoadSceneManager.instance.SwitchScene(data.loadedSceneName);
   }
   
   public void LoadNewGameplayScene(string sceneName)
   {
      if (currentState == GameState.InMainMenu)
         return;

      LoadSceneManager.instance.SwitchScene(sceneName);
      if (sceneName == "Puzzle1" || sceneName == "Puzzle2")
         currentState = GameState.InPuzzle;
      else
         currentState = GameState.InGame;

      data.loadedSceneName = sceneName;
   }
   
   /// <summary>
   /// This function loads a save file from disk.
   /// </summary>
   /// <param name="saveName">The name of the save file we want to load</param>
   public void LoadFromSave(string saveName)
   {
      //first, we try to load the game from via the save manager.
      if (!SaveManager.TryLoadData<GameData>(saveName, out var loadedData))
         return; //if we cannot load the save, we cancel the action.
        
      //after we successfuly loaded the save, we set the data correctly. 
      data = loadedData;
      
      AudioManager.instance.CleanUp();
      
      //we set the correct state (as we want to enter the inGame-State) and give out the callback
      if (data.loadedSceneName == "Puzzle1" || data.loadedSceneName == "Puzzle2")
      {
         AudioManager.instance.InitializeMusic(FMODEvents.instance.puzzleMusic);
         currentState = GameState.InPuzzle;
      }
      else
      {
         switch (data.loadedSceneName)
         {
            case "Level1":
               AudioManager.instance.InitializeMusic(FMODEvents.instance.level1Music);
               break;
            case "Level2":
               AudioManager.instance.InitializeMusic(FMODEvents.instance.northburryMusic);
               break;
            case "Shop":
               AudioManager.instance.InitializeMusic(FMODEvents.instance.shopMusic);
               break;
            case "Level3":
               AudioManager.instance.InitializeMusic(FMODEvents.instance.moonbrightForestMusic);
               break;
            case "Level4":
               AudioManager.instance.InitializeMusic(FMODEvents.instance.oldCabinMusic);
               break;
            default:
               AudioManager.instance.InitializeMusic(FMODEvents.instance.mainMenuMusic);
               break;
         }
         currentState = GameState.InGame;
      }

      if (onStateChanged != null)
         onStateChanged(currentState);
      
      //we load the scene we last saved the game in, as it is set within the data.
      LoadSceneManager.instance.SwitchScene(data.loadedSceneName);
   }

   /// <summary>
   /// We call this method to save the current game state.
   /// </summary>
   /// <param name="saveName">The wanted name for the savegame</param>
   public void SaveGame(string saveName)
   {
      if (currentState == GameState.InMainMenu)
         return; //if we are in the main menu, we dont save anything
      
      //we give the current data and the wanted save name to the SaveManger
      SaveManager.TrySaveData(saveName, data);
   }
}
