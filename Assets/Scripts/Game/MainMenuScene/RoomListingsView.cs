using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun;

public class RoomListingsView : MonoBehaviour
{
    [SerializeField]
    private Text text;

    public RoomInfo RoomInfo { get; private set; }

    public int gameModeID;

    private void Start()
    {
        
    }

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        gameModeID = (int)roomInfo.CustomProperties["mode"];
        
        text.text = roomInfo.Name + " , " + roomInfo.MaxPlayers;
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(RoomInfo.Name);
        PlayerPrefs.SetString("JoinedAs", "Player");

        FindMatchController findMatchController = FindObjectOfType<FindMatchController>();
        GameInstance.Instance._GameMode = UIMainMenu.instance.gameModes[gameModeID];
        findMatchController.RoomNameText.text = RoomInfo.Name;
        findMatchController.CreateModePanel.SetActive(true);
        findMatchController.CreatePanel.SetActive(false);
        findMatchController.CreatedPanel.SetActive(true);
    }

    public void Spectate()
    {
        PhotonNetwork.LocalPlayer.NickName = "Spectator";
        PhotonNetwork.JoinRoom(RoomInfo.Name);
        GameInstance.Instance._GameMode = UIMainMenu.instance.gameModes[gameModeID];
        PlayerPrefs.SetString("JoinedAs", "Spectator");
        FindMatchController findMatchController = FindObjectOfType<FindMatchController>();
        
        findMatchController.RoomNameText.text = RoomInfo.Name;
        findMatchController.CreateModePanel.SetActive(true);
        findMatchController.CreatePanel.SetActive(false);
        findMatchController.CreatedPanel.SetActive(true);
    }


}
