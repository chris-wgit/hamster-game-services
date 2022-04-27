using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MultipeProjectiles : Projectile, IWeaponOwner
{
    [Required]
    [BoxGroup("Data")]
    public GameObject projectileObject;
    [BoxGroup("Data")]
    public Transform[] childTransform;

    public bool spawnDelay;

    private bool isOriginal;
    private int spawnTime;

    protected override void Awake()
    {
        if(childTransform == null)
        {
            childTransform = GetComponentsInChildren<Transform>();
        }
        Initialization();
    }

    protected override void Initialization()
    {
        lifeTime = projectiles.lifeTime;
        OnSpawnFX = projectiles.OnSpawnVFX;
        OnDespawnFX = projectiles.OnDespawnVFX;
        OnSpawnSFX = projectiles.OnSpawnSFX;
        OnDespawnSFX = projectiles.OnDespawnSFX;
    }

    public override void InitProjectiles(bool isOriginal, int spawnTime = 0)
    {
        this.isOriginal = isOriginal;
        this.spawnTime = spawnTime;

        if (!spawnDelay)
        {
            SpawnChildProjectile();
        }
        else
        {

            StartCoroutine(SpawnChildRoutine());
        }
    }
    public override void OnSpawn()
    {
        PoolManager.Despawn(gameObject, projectiles.lifeTime);
        //VFX
        if (OnSpawnFX != null && spawnFX) PoolManager.Spawn(OnSpawnFX, transform.position, transform.rotation);

        if (OnSpawnSFX != null && spawnSFX) EventHandlerFX.PlayAudioFX(OnSpawnSFX, transform.position);
    }

    private void SpawnChildProjectile()
    {
        for (int i = 0; i < childTransform.Length; i++)
        {
            GameObject bulletObj = PoolManager.Spawn(projectileObject, childTransform[i].position, childTransform[i].rotation);
            Projectile bullet = bulletObj.GetComponent<Projectile>();
            bullet.SetOwner(_Owner);
            bullet.InitProjectiles(isOriginal, spawnTime);

        }
    }

    private IEnumerator SpawnChildRoutine()
    {
        for (int i = 0; i < childTransform.Length; i++)
        {
            GameObject bulletObj = PoolManager.Spawn(projectileObject, childTransform[i].position, childTransform[i].rotation);
            Projectile bullet = bulletObj.GetComponent<Projectile>();
            bullet.SetOwner(_Owner);
            bullet.InitProjectiles(isOriginal, spawnTime);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override void SetOwner(Character attacker)
    {
        base.SetOwner(attacker);
    }

    public override void OnDespawn()
    {

    }
}

