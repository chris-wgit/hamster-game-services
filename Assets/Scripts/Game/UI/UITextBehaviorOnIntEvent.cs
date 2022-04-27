using UnityEngine;
using TMPro;
using System;

public class UITextBehaviorOnIntEvent : MonoBehaviour
{
    private TextMeshProUGUI text;
    public IntEventChannelSO OnUpdateEvent;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        OnUpdateEvent.OnEventRaised += UpdateText;
    }
    private void OnDisable()
    {
        OnUpdateEvent.OnEventRaised -= UpdateText;
    }

    private void UpdateText(int value)
    {
        text.text = value.ToString();
    }
}

