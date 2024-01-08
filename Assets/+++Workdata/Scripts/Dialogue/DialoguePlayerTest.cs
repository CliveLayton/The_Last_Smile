using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
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

    private void Awake()
    {
        instance = this;
    }

    public void OnEnable()
    {
        StartCoroutine(StoryContinue());
    }

    private IEnumerator StoryContinue()
    {
        Story story = new Story(dialogueAsset.text);
        story.ChoosePathString(inkPath);

        while (story.canContinue || story.currentChoices.Count > 0)
        {
            yield return StartCoroutine(ShowNextTexts(story));
            if(story.currentChoices.Count > 0)
                yield return StartCoroutine(ShowNextDecision(story));
        }
        
        gameObject.SetActive(false);
    }

    private IEnumerator ShowNextTexts(Story story)
    {
        print("NextText");
        while (story.canContinue)
        {
            print("SameText");
            // dialogueTextComponent.text = story.Continue();
            yield return StartCoroutine(DisplayLine(story.Continue()));
            //waits for a frame
            yield return null;
            while (!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }

            yield return null;
        }
    }

    private IEnumerator ShowNextDecision(Story story)
    {
        print("Decision");
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

    private IEnumerator DisplayLine(string line)
    {
        //empty the dialog text
        dialogueTextComponent.text = "";
        
        //display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            if (Input.GetMouseButton(0))
            {
                print("displayLine");
                dialogueTextComponent.text = line;
                break;
            }
            print("displayChars");
            dialogueTextComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
