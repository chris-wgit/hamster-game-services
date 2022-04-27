using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using System;
using Sirenix.OdinInspector;

public class FeedbacksManager : MonoBehaviour
{
    public MMFeedbacks shakeFeedbacks;
    public MMFeedbacks flashFeedbacks;

    private void OnEnable()
    {
        EventHandlerFX.OnShakeEvent += ShakeEvent;
        EventHandlerFX.OnFlashEvent += FlashEvent;
    }
    private void OnDisable()
    {
        EventHandlerFX.OnShakeEvent -= ShakeEvent;
        EventHandlerFX.OnFlashEvent -= FlashEvent;

    }

    [Button(ButtonSizes.Large)]
    private void FlashEvent()
    {
        flashFeedbacks.PlayFeedbacks();
    }
    [Button(ButtonSizes.Large)]
    private void ShakeEvent(float power)
    {
        shakeFeedbacks.FeedbacksIntensity = power;
        shakeFeedbacks.PlayFeedbacks();
    }
}
