using Sirenix.OdinInspector;
using System;
using UnityEngine;

public abstract class ScriptableItem : ScriptableObject
{
    [BoxGroup("Basic Infor")]
    public string itemName;
    [BoxGroup("Basic Infor")]
    [Multiline]
    public string description;

    [BoxGroup("Visual"), PreviewField(80)]
    public GameObject itemObject;

    [BoxGroup("FX", order: 8)]
    public GameObject OnSpawnVFX;
    [BoxGroup("FX")]
    public AudioClip OnSpawnSFX;
    [BoxGroup("FX")]
    public GameObject OnDespawnVFX;
    [BoxGroup("FX")]
    public AudioClip OnDespawnSFX;

    public abstract bool IsUseable(Character character);

    public abstract void Action(Character character, Action onComplete);
}
