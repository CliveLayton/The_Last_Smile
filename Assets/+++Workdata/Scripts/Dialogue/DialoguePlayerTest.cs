using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.SearchService;
using UnityEngine.UI;

public class DialoguePlayerTest : MonoBehaviour
{
    public static DialoguePlayerTest instance;
    public TextAsset dialogueAsset;
    [SerializeField] private DialogueButton buttonPrefab;
    [SerializeField] private RectTransform buttonParent;
    [SerializeField] private TextMeshProUGUI dialogueTextComponent;
    [SerializeField] private float typingSpeed = 0.04f;
    public string inkPath;
    private Animator layoutAnimator;

    private Animator ameliaAnimator;
    private Animator jackAnimator;

    private PlayerController player;
    
    private const string LAYOUT_TAG = "layout";
    private const string SPEAKER_TAG = "speaker";

    private string currentLayout;
    /// <summary>
    /// Animator for ContinueArrow
    /// </summary>
    [SerializeField] private Animator arrowAnimator;

    private void Awake()
    {
        instance = this;
        //get the layout animator
        layoutAnimator = GetComponent<Animator>();
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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.enabled = false;

        story.BindExternalFunction("WalkAway", (string animationTrigger, bool shopClosedDoorActive, 
            bool shopOpenDoorActive, bool oliverActive) =>
        {
            ameliaAnimator = GameObject.FindGameObjectWithTag("Amelia").GetComponent<Animator>();
            ameliaAnimator.Play(animationTrigger);
            GameStateManager.instance.shopClosedDoorActive = shopClosedDoorActive;
            GameStateManager.instance.shopOpenDoorActive = shopOpenDoorActive;
            GameStateManager.instance.oliverActive = oliverActive;
        });
        story.BindExternalFunction("WalkToCabin", (string animationTrigger) =>
        {
            jackAnimator = GameObject.FindGameObjectWithTag("Jack").GetComponent<Animator>();
            jackAnimator.Play(animationTrigger);
        });
        while (story.canContinue || story.currentChoices.Count > 0)
        {
            yield return StartCoroutine(ShowNextTexts(story));
            if(story.currentChoices.Count > 0)
                yield return StartCoroutine(ShowNextDecision(story));
        }

        player.enabled = true;
        story.UnbindExternalFunction("WalkAway");
        story.UnbindExternalFunction("WalkToCabin");
        gameObject.SetActive(false);
    }

    private IEnumerator ShowNextTexts(Story story)
    {
        while (story.canContinue)
        {
            // dialogueTextComponent.text = story.Continue();
            yield return StartCoroutine(DisplayLine(story.Continue(), story));
            //waits for a frame
            arrowAnimator.SetTrigger(currentLayout);
            yield return null;
            while (!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }

            while (!Input.GetMouseButtonUp(0))
            {
                yield return null;
            }
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
        
        //handle tags
        yield return StartCoroutine(HandleTags(currentStory.currentTags));

        //display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            if (Input.GetMouseButton(0))
            {
                dialogueTextComponent.text = line;
                arrowAnimator.SetTrigger(currentLayout);
                break;
            }
            dialogueTextComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
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
}
