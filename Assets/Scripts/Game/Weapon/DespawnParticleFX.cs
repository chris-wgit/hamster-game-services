using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnParticleFX : MonoBehaviour
{
    private float lifeTime;

    private ParticleSystem partical;
    private void Awake()
    {
        partical = GetComponent<ParticleSystem>();
        lifeTime = partical.main.duration;
    }
    void OnSpawn()
    {
        PoolManager.Despawn(gameObject, lifeTime);
    }

    void OnDespawn()
    {

    }
}

