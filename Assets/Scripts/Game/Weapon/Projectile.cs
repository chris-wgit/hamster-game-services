using Cysharp.Threading.Tasks;
using Photon.Pun;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using System;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour, IWeaponOwner
{
    [BoxGroup("Data",order:1)]
    public ProjectileSkill projectiles;

    protected Character _Owner;
    protected float lifeTime;
    protected int damage;
    protected float speed;

    private Rigidbody rb;

    [BoxGroup("VFX", order:2)]
    public bool spawnFX;
    [BoxGroup("VFX")]
    public bool spawnSFX;

    protected GameObject OnSpawnFX;
    protected GameObject OnDespawnFX;

    protected AudioClip OnSpawnSFX;
    protected AudioClip OnDespawnSFX;

    private float syncTime;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Initialization();
        syncTime = 0.05f;
    }
    protected virtual void Initialization()
    {
        lifeTime = projectiles.lifeTime;
        damage = projectiles.damage;
        speed = projectiles.speed;
        OnSpawnFX = projectiles.OnSpawnVFX;
        OnDespawnFX = projectiles.OnDespawnVFX;
        OnSpawnSFX = projectiles.OnSpawnSFX;
        OnDespawnSFX = projectiles.OnDespawnSFX;

    }
    private Vector3 spawnPos;
    public virtual void OnSpawn()
    {
        spawnPos = transform.position;
        //VFX
        if (OnSpawnFX != null && spawnFX) PoolManager.Spawn(OnSpawnFX, transform.position, transform.rotation);
        
        if (OnSpawnSFX != null && spawnSFX) EventHandlerFX.PlayAudioFX(OnSpawnSFX, transform.position);
    }

    public virtual void InitProjectiles(bool isOriginal, int spawnTime = 0)
    {
        if (isOriginal)
        {

            PoolManager.Despawn(gameObject, lifeTime);
            SetMovement(speed);
        }
        else
        {

            float delayedTime = (PhotonNetwork.ServerTimestamp - spawnTime);
            delayedTime /= 1000;
            Debug.Log("Delayed time is " + delayedTime);
            float movedRange = speed * (delayedTime+ syncTime);
            Debug.Log("Moved Range is " + movedRange);
            float fowardSpeed = movedRange / syncTime;
            Debug.Log("FowardSpeed is " + fowardSpeed);
            PoolManager.Despawn(gameObject, (lifeTime-(delayedTime)));

            SyncPosition(movedRange);
            //SyncProjectilesTask(fowardSpeed).Forget();

            //StartCoroutine(SyncSpeedRoutine(fowardSpeed));
            //SyncMovement(movedRange);
            //SetMovement(speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        IDamageable target = other.GetComponent<IDamageable>();

        if (target != null)
        {
            if (!target.IsDamageable(_Owner)) return;

            target.ApplyDamage(damage, _Owner);

            if (projectiles.despawnOnColliderCharacter)
                PoolManager.Despawn(gameObject); 
        }
        else
        {
            if (projectiles.despawnOnColliderEnvironment)
                PoolManager.Despawn(gameObject);
        }
    }

    private void SyncPosition(float range)
    {
        Debug.Log(transform.position);
        transform.DOMove(transform.position + transform.forward.normalized*range, syncTime).OnComplete(()=>SetMovement(speed));
    }
    //private async UniTaskVoid SyncProjectilesTask(float fastSpeed, float distance)
    //{
    //    SetMovement(fastSpeed);
    //    await UniTask.Delay(Mathf.CeilToInt(syncTime * 1000), true);
    //    Debug.DrawLine(spawnPos, transform.position, Color.green, 60);
    //    SetMovement(speed);
    //}

    //private IEnumerator SyncSpeedRoutine(float fastSpeed)
    //{

    //    SetMovement(fastSpeed);
    //    yield return new WaitForSeconds(syncTime);
    //    Debug.DrawLine(spawnPos, transform.position, Color.green, 60);
    //    SetMovement(speed);
    //}

    private  void SetMovement(float speed)
    {
        if (rb != null)
        {
            rb.velocity = speed * transform.forward.normalized;
        }
    }

    protected virtual void SyncMovement(float movedRange)
    {
        transform.position += transform.forward.normalized * movedRange;
    }

    public virtual void OnDespawn()
    {
        Debug.DrawLine(spawnPos, transform.position, Color.red, 60);
        Debug.Log("Despawn Time is" + PhotonNetwork.ServerTimestamp);
        if (OnDespawnFX != null) PoolManager.Spawn(OnDespawnFX, transform.position, transform.rotation);
        
        if(OnDespawnSFX != null) EventHandlerFX.PlayAudioFX(OnDespawnSFX, transform.position);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
#endif
}

