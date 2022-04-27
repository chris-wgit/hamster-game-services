using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnOnTime : MonoBehaviour
{
    public float lifeTime;
    public void OnSpawn()
    {
        PoolManager.Despawn(gameObject, lifeTime);
    }
}
