using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="VFX/Music Event")]
public class MusicEventChannelSO : ScriptableObject
{
    public UnityAction<AudioClip> OnEventRaised;

    public void RaiseEvent(AudioClip clip)
    {
        OnEventRaised?.Invoke(clip);
    }
}
