using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "VFX/Audio Event")]
public class AudioEventChannelSO : ScriptableObject
{
    public UnityAction<AudioClip, Vector3, float> OnEventRaised;

    public void RaiseEvent(AudioClip clip, Vector3 position, float pitch = 0f)
    {
        OnEventRaised?.Invoke(clip, position, pitch);
    }
}



