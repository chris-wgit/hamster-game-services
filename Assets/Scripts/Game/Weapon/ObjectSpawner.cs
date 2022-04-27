using Photon.Pun;
using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviourPunCallbacks
{
    public GameObject spawnObj;

    private CooldownTimer cooldown;

    private GameObject obj;

    public int respawnTime;

    public bool spawnOnStart;

    private void Start()
    {
        cooldown = new CooldownTimer();
        cooldown.Initialization(respawnTime);
        if (spawnOnStart)
        {
            Instantiate();
        }
        else
        {
            cooldown.StartCooldown(()=> SpawnRoutine());
        }
    }

    private void Update()
    {
        if (cooldown.IsActive)
        {
            cooldown.Update();
        }
    }

    [PunRPC]
    public void Instantiate()
    {
        if (obj != null) return;

        obj = PoolManager.Spawn(spawnObj, transform.position, transform.rotation);
        CollectableItem item = obj.GetComponent<CollectableItem>();
        item.spawner = this;
    }

    [PunRPC]
    public void DestroyItem()
    {
        PoolManager.Despawn(obj);
        obj = null;
        cooldown.StartCooldown(()=> SpawnRoutine());
    }

    public void CallDestroyItem()
    {
        this.photonView.RPC("DestroyItem", RpcTarget.All);
    }


    private void SpawnRoutine()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("Instantiate", RpcTarget.All);
        }
    }
}