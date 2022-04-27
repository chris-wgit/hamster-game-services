using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandlerDebug 
{
    public static Action<string> onShowLog;

    public static void ShowLog(string message)
    {
        onShowLog?.Invoke(message);
    }
}
