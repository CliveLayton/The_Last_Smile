using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueButton : MonoBehaviour
{
    private int choiceIndex = 0;
    private System.Action<int> onClicked;
    [SerializeField] TextMeshProUGUI textComponent;
    
    /// <summary>
    /// initialize a button with the choice from the ink story
    /// </summary>
    /// <param name="choiceIndex">int</param>
    /// <param name="text">string</param>
    /// <param name="onClicked">action</param>
    public void Initialize(int choiceIndex, string text, System.Action<int> onClicked)
    {
        this.choiceIndex = choiceIndex;
        textComponent.text = text;
        this.onClicked = onClicked;
    }
    
    /// <summary>
    /// if button is pressed give the action the choice index of the button to confirm which answer was clicked
    /// </summary>
    public void OnClick()
    {
        if (onClicked != null)
            onClicked(choiceIndex);
        onClicked = null;
    }
}
