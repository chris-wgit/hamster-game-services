using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class NetworkSandbox : MonoBehaviourPunCallbacks
{
    public GameObject characterObject;
    public PlayerDataSO _PlayerData;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnected()
    {

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LocalPlayer.SetWeapon(_PlayerData.weaponID);
        PhotonNetwork.LocalPlayer.SetTeam(Random.Range(0, 2));
        PhotonNetwork.LocalPlayer.SetPlayerIndex(Random.Range(0, 2));

        CoSpawnCharacter();
    }

    private async void CoSpawnCharacter()
    {
        await UniTask.Delay(3000);
        InstantiateCharacter();

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        
    }

    public void InstantiateCharacter()
    {
        Transform spawnPos = SpawnBaseManager.instance.GetSpawnPosition(PhotonNetwork.LocalPlayer.GetTeam(), PhotonNetwork.LocalPlayer.GetIndex());
        PhotonNetwork.Instantiate(characterObject.name, spawnPos.position, spawnPos.rotation);
    }
}
