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
    
    public void Initialize(int choiceIndex, string text, System.Action<int> onClicked)
    {
        this.choiceIndex = choiceIndex;
        textComponent.text = text;
        this.onClicked = onClicked;
    }
    
    public void OnClick()
    {
        if (onClicked != null)
            onClicked(choiceIndex);
        onClicked = null;
    }
}
