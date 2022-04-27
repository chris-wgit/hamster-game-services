using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour, IWeaponOwner
{
    public MeleeSkill damageData;

    protected Character _Owner;
    protected float lifeTime;
    protected int damage;
    protected float speed;

    public ParticleSystem particle; 

    private GameObject OnSpawnFX;
    private GameObject OnDespawnFX;

    private AudioClip OnSpawnSFX;
    private AudioClip OnDespawnSFX;

    public bool followOwner;

    protected virtual void Awake()
    {
        Initialization();
    }
    protected virtual void Initialization()
    {
        lifeTime = damageData.lifeTime;

        if (particle == null) particle = GetComponentInChildren<ParticleSystem>();

        if (lifeTime == 0 && particle != null) lifeTime = particle.main.duration;

        damage = damageData.damage;
        followOwner = damageData.followOwner;
        OnSpawnFX = damageData.OnSpawnVFX;
        OnDespawnFX = damageData.OnDespawnVFX;
        OnSpawnSFX = damageData.OnSpawnSFX;
        OnDespawnSFX = damageData.OnDespawnSFX;
    }

    private void LateUpdate()
    {
        if (followOwner)
        {
            transform.position = _Owner.transform.position;
        }
    }

    public virtual void OnSpawn()
    {
        PoolManager.Despawn(gameObject, lifeTime);
        //VFX
        if (OnSpawnFX != null) PoolManager.Spawn(OnSpawnFX, transform.position, transform.rotation);

        if (OnSpawnSFX != null) EventHandlerFX.PlayAudioFX(OnSpawnSFX, transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamageable target = other.GetComponent<IDamageable>();

        if (target != null)
        {
            if (!target.IsDamageable(_Owner)) return;

            target.ApplyDamage(damage, _Owner);

            if (damageData.despawnOnColliderCharacter)
                PoolManager.Despawn(gameObject);
        }

        if (damageData.despawnOnColliderEnvironment)
            PoolManager.Despawn(gameObject);

    }

    public virtual void OnDespawn()
    {
        if (OnDespawnFX != null) PoolManager.Spawn(OnDespawnFX, transform.position, transform.rotation);

        if (OnDespawnSFX != null) EventHandlerFX.PlayAudioFX(OnDespawnSFX, transform.position);

    }

    public virtual void SetOwner(Character owner)
    {
        _Owner = owner;
    }

    public virtual Character GetOwner()
    {
        return _Owner;
    }

    public virtual int GetDamage()
    {
        return damage;
    }
}

