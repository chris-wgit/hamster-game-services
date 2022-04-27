using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugLog : MonoBehaviour
{
    public TextMeshProUGUI debugText;

    private void OnEnable()
    {
        EventHandlerDebug.onShowLog += ShowLog;
    }
    private void OnDisable()
    {
        EventHandlerDebug.onShowLog -= ShowLog;
    }

    private void ShowLog(string obj)
    {
        debugText.text = obj;
    }
}
