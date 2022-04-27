using Sirenix.OdinInspector;
using UnityEngine;

public class ProjectilesSO : ScriptableObject
{
    [BoxGroup("Basic Infor")]
    public string projectileName;

    [BoxGroup("Basic Infor")]
    [Multiline]
    public string description;

    [BoxGroup("Visual")]
    [PreviewField(75)]
    public GameObject projectileObject;

    [BoxGroup("Visual")]
    [PreviewField(75)]
    public Sprite icon;

    [BoxGroup("Statistic")]
    public int damage;

    [ShowInInspector]
    [BoxGroup("Statistic")]
    [SerializeField]
    protected int range;

    [BoxGroup("Statistic")]
    public float speed;

    [BoxGroup("Statistic")]
    protected float lifeTime { get { return Range / speed; } }

    [BoxGroup("Statistic")]
    [ShowInInspector]
    public virtual float LifeTime { get { return lifeTime; } }

    public virtual float Range { get { return range / 10; } }

    public bool despawnOnCollider;

    [BoxGroup("VFX")]
    public GameObject ImpactFX;

    [BoxGroup("VFX")]
    public AudioClip OnSpawnAudio;

    [BoxGroup("VFX")]
    public AudioClip OnDespawnAudio;
}