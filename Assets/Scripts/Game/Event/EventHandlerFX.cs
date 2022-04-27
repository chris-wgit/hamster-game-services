using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandlerFX
{
    public static Action<float> OnShakeEvent;
    public static Action OnFlashEvent;

    public static Action<AudioClip, Vector3> On3DAudioEvent;
    public static Action<AudioClip> OnSoundFX;
    public static Action<AudioClip> OnMusicFX;
    public static void ShakeEvent(float power)
    {
        OnShakeEvent?.Invoke(power);
    }

    public static void FlashEvent()
    {
        OnFlashEvent?.Invoke();
    }

    public static void PlayAudioFX(AudioClip clip, Vector3 position)
    {
        On3DAudioEvent?.Invoke(clip, position);
    }

    public static void PlayMusic(AudioClip clip)
    {
        OnMusicFX?.Invoke(clip);
    }
}
