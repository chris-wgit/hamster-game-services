using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public abstract partial class ScriptableSkill : ScriptableObject
{
    [BoxGroup("Basic Infor")]
    public string skillName;
    [BoxGroup("Basic Infor")]
    [Multiline]
    public string description;

    [BoxGroup("Visual")]
    [PreviewField(75)]
    public Sprite icon;

    [BoxGroup("Statistic")]
    [Range(1, 6)]
    public int maxCharge = 1;
    [BoxGroup("Statistic")]
    [Tooltip("Do we add full charge for player at start")]
    public bool initialCharge;
    [BoxGroup("Statistic")]
    public float cooldown = 1f;
    public float totalCooldown { get { return cooldown * maxCharge; } }
    [BoxGroup("Statistic")]
    public float actionDuration = 0.5f;

    [BoxGroup("State Machine")]
    public MovementStates characterState = MovementStates.Attacking;

    [BoxGroup("FX", order: 8)]
    public GameObject OnSpawnVFX;
    [BoxGroup("FX")]
    public AudioClip OnSpawnSFX;
    [BoxGroup("FX")]
    public GameObject OnDespawnVFX;
    [BoxGroup("FX")]
    public AudioClip OnDespawnSFX;

    [BoxGroup("Feedbacks", order:7), Range(0.0f,1.0f)]
    public float shakeFeedbackPower;

    public abstract bool IsCharacterStateCanAction(Character character);

    public virtual Vector3 DestinationFromInput(Vector2 input, Transform normalizedTransform) { return Vector3.zero; }

    public abstract void Action(Character owner, LocalTransform transformData);

    public abstract void Action(Character owner, LocalTransform transformData, int timestamp);

    public abstract void Action(Character owner);

    public abstract void EndAction();

}
