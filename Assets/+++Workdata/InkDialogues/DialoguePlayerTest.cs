using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePlayerTest : MonoBehaviour
{
    [SerializeField] private TextAsset dialogueAsset;
    [SerializeField] private DialogueButton buttonPrefab;
    [SerializeField] private RectTransform buttonParent;
    [SerializeField] private TextMeshProUGUI dialogueTextComponent;

    public void Start()
    {
        StartCoroutine(StoryContinue());
    }

    private IEnumerator StoryContinue()
    {
        Story story = new Story(dialogueAsset.text);

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
        while (story.canContinue)
        {
            dialogueTextComponent.text = story.Continue();
            while (!Input.GetMouseButtonDown(0))
                yield return null;
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
}
