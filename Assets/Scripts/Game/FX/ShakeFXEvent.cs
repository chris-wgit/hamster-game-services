using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeFXEvent : MonoBehaviour
{
    public float shakePower;
    private void OnEnable()
    {
        ShakeEvent();
    }

    void ShakeEvent()
    {
        EventHandlerFX.ShakeEvent(shakePower);
    }
}
