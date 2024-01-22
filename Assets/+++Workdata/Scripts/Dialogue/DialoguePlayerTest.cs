using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SearchService;
using UnityEngine.UI;
/// <summary>
/// this is the manager script for all dialogues in the game
/// </summary>
public class DialoguePlayerTest : MonoBehaviour
{
    public static DialoguePlayerTest instance;
    public TextAsset dialogueAsset;
    //the button that get initialize for choices
    [SerializeField] private DialogueButton buttonPrefab;
    //the parent for the buttons to initialize each button on the right place
    [SerializeField] private RectTransform buttonParent;
    [SerializeField] private TextMeshProUGUI dialogueTextComponent;
    [SerializeField] private float typingSpeed = 0.04f;
    public string inkPath;
    //audio reference for typing text
    public EventReference dialogueTextSFX;
    //animator for the dialogue system
    private Animator layoutAnimator;

    //npcs and their animator
    private Animator ameliaAnimator;
    private Animator jackAnimator;
    private Interactable ameliaWell;
    private Interactable jackForest;

    private PlayerController player;
    //the in game UI script
    private InGameUI inGameUI;
    //the button for open the journal
    private GameObject openMenuButton;
    //the button for using the camera
    private GameObject useCameraButton;
    
    private const string LAYOUT_TAG = "layout";
    private const string SPEAKER_TAG = "speaker";

    private string currentLayout;
    /// <summary>
    /// Animator for ContinueArrow
    /// </summary>
    [SerializeField] private Animator arrowAnimator;
    
    
    //CollectableData for the info text of each character
    [SerializeField] private CollectableData oliverInfo;
    [SerializeField] private CollectableData ameliaInfo;
    [SerializeField] private CollectableData jackInfo;

    private void Awake()
    {
        instance = this;
        //get the layout animator
        layoutAnimator = GetComponent<Animator>();
        inGameUI = GameObject.FindGameObjectWithTag("InGameHUD").GetComponent<InGameUI>();
        openMenuButton = GameObject.Find("OpenMenuButton");
        useCameraButton = GameObject.Find("UseCameraButton");
    }

    public void OnEnable()
    {
        StartCoroutine(StoryContinue());
    }

    private IEnumerator StoryContinue()
    {
        Story story = new Story(dialogueAsset.text);
        story.ChoosePathString(inkPath);
        
        //reset layout and speaker
        layoutAnimator.Play("Liam");
        
        //set player inactive while dialogue is playing
        player = FindObjectOfType<PlayerController>().gameObject.GetComponent<PlayerController>();
        player.inSequence = true;
        player.enabled = false;
        
        //set the gameUI off while dialogue is playing
        inGameUI.menuActive = false;
        openMenuButton.SetActive(false);
        useCameraButton.SetActive(false);

        //set the functions coming from ink
        ObjectInteractionFunctions(story);
        CharacterInfoFunctions(story);
        
        while (story.canContinue || story.currentChoices.Count > 0)
        {
            yield return StartCoroutine(ShowNextTexts(story));
            if(story.currentChoices.Count > 0)
                yield return StartCoroutine(ShowNextDecision(story));
        }

        //enable all objects if dialogue ends
        player.enabled = true;
        player.inSequence = false;
        inGameUI.menuActive = true;
        openMenuButton.SetActive(true);
        useCameraButton.SetActive(true);
        
        //unbind all functions coming from ink
        UnbindAllFunction(story);
        gameObject.SetActive(false);
    }

    private IEnumerator ShowNextTexts(Story story)
    {
        while (story.canContinue)
        {
            // dialogueTextComponent.text = story.Continue();
            yield return StartCoroutine(DisplayLine(story.Continue(), story));
            //sets the continue arrow of the dialogue active
            arrowAnimator.SetTrigger(currentLayout);
            //waits for a frame
            yield return null;
            while (!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }

            while (!Input.GetMouseButtonUp(0))
            {
                yield return null;
            }
            //deactivates the continue arrwo of the dialogue
            arrowAnimator.SetTrigger("exit");
            yield return null;
        }
    }

    private IEnumerator ShowNextDecision(Story story)
    {
        int choice = -1;
        for(int i = 0; i < story.currentChoices.Count; i++)
        {
            var newButton = Instantiate(buttonPrefab, buttonParent);
            newButton.Initialize(i, story.currentChoices[i].text, buttonIndex => choice = buttonIndex);
        }

        while (choice < 0)
            yield return null;
        
        story.ChooseChoiceIndex(choice);
        for (int i = buttonParent.childCount - 1; i >= 0; i--)
            Destroy(buttonParent.GetChild(i).gameObject);
    }

    private IEnumerator DisplayLine(string line, Story currentStory)
    {
        //empty the dialog text
        dialogueTextComponent.text = "";
        
        AudioManager.instance.StartDialogueSFX();

        //handle tags
        yield return StartCoroutine(HandleTags(currentStory.currentTags));

        //display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            if (Input.GetMouseButton(0))
            {
                dialogueTextComponent.text = line;
                AudioManager.instance.StopDialogueSFX();
                arrowAnimator.SetTrigger(currentLayout);
                break;
            }
            dialogueTextComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        AudioManager.instance.StopDialogueSFX();
    }

    private IEnumerator HandleTags(List<string> currentTags)
    {
        //loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {
            //parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            
            //handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    currentLayout = tagValue;
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
        yield return null;
    }

    /// <summary>
    /// all functions to set the character info for each character in the ink story
    /// </summary>
    /// <param name="currentStory">story</param>
    private void CharacterInfoFunctions(Story currentStory)
    {
        currentStory.BindExternalFunction("InfoOliver", (int sympathyCount) =>
        {
            if((sympathyCount == 1) && !GameStateManager.instance.data.HasCollectible(oliverInfo.identifier))
                GameStateManager.instance.data.AddCollectible(oliverInfo.identifier);
        });
        
        currentStory.BindExternalFunction("InfoAmelia", (int sympathyCount) =>
        {
            if((sympathyCount == 1) && !GameStateManager.instance.data.HasCollectible(ameliaInfo.identifier))
                GameStateManager.instance.data.AddCollectible(ameliaInfo.identifier);
        });
        
        currentStory.BindExternalFunction("InfoJack", (int sympathyCount) =>
        {
            if((sympathyCount == 1) && !GameStateManager.instance.data.HasCollectible(jackInfo.identifier))
                GameStateManager.instance.data.AddCollectible(jackInfo.identifier);
        });
        
    }

    /// <summary>
    /// all object interactions we manage within ink story
    /// </summary>
    /// <param name="currentStory">story</param>
    private void ObjectInteractionFunctions(Story currentStory)
    {
        //function for amelia to move after dialogue on the well, activates the open shop door, deactivates the closed shop door
        currentStory.BindExternalFunction("WalkAway", (string animationTrigger, bool shopClosedDoorActive, 
            bool shopOpenDoorActive) =>
        {
            ameliaAnimator = GameObject.FindGameObjectWithTag("Amelia").GetComponent<Animator>();
            ameliaAnimator.Play(animationTrigger);
            GameStateManager.instance.shopClosedDoorActive = shopClosedDoorActive;
            GameStateManager.instance.shopOpenDoorActive = shopOpenDoorActive;
        });
        
        //function to deactivate oliver in the Level2 scene, deactivate amelia on the well
        currentStory.BindExternalFunction("NPCWell", (bool oliverActive, bool ameliaActive, 
            bool transporterWithoutWoodActive, bool transporterWithWoodActive) =>
        {
            ameliaWell = GameObject.FindGameObjectWithTag("Amelia").GetComponentInChildren<Interactable>();
            ameliaWell.SetObjectActive(ameliaActive);
            GameStateManager.instance.oliverActive = oliverActive;
            GameStateManager.instance.truckWithoutWood = transporterWithoutWoodActive;
            GameStateManager.instance.truckWithWood = transporterWithWoodActive;
        });
        
        //function for the animation to move jack after dialogue, deactivates jack in the woods
        currentStory.BindExternalFunction("WalkToCabin", (string animationTrigger, bool jackActive) =>
        {
            jackAnimator = GameObject.FindGameObjectWithTag("Jack").GetComponent<Animator>();
            jackForest = GameObject.FindGameObjectWithTag("Jack").GetComponentInChildren<Interactable>();
            jackAnimator.Play(animationTrigger);
            jackForest.SetObjectActive(jackActive);
        });
    }

    /// <summary>
    /// unbind all function from ink
    /// </summary>
    /// <param name="currentstory">story</param>
    private void UnbindAllFunction(Story currentstory)
    {
        currentstory.UnbindExternalFunction("WalkAway");
        currentstory.UnbindExternalFunction("WalkToCabin");
        currentstory.UnbindExternalFunction("NPCWell");
        currentstory.UnbindExternalFunction("InfoOliver");
        currentstory.UnbindExternalFunction("InfoAmelia");
        currentstory.UnbindExternalFunction("InfoJack");
    }
    
}
