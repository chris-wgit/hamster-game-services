using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventHandlerPlayfab
{
    public static Action<Action> OnSetUserNameEvent;

    public static void UpdateUserNameAction(Action onSuccess)
    {
        OnSetUserNameEvent?.Invoke(onSuccess);
    }
}
