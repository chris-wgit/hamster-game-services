using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextBehaviour : MonoBehaviour
{
    [Header("Component Reference")]
    public TextMeshProUGUI textDisplay;

    public void SetText(string newText)
    {
        textDisplay.SetText(newText);
    }
}
