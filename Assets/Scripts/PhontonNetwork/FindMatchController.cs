using Cysharp.Threading.Tasks;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;
using System;
using UnityEngine.UI;

public class FindMatchController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public PlayerDataSO playerData;

    [Header("----------Game Data-----------")]
    public GameplaySO gameMode;

    public GameStatisticSO _GameStatistic;

    public UIRoomController roomUI;

    public TMP_Dropdown regionDropdown;

    [Header("----------Create Room---------")]
    public InputField RoomName;

    public Toggle IsVisible;

    public Toggle IsOpen;

    public InputField MaxPlayers;

    public GameObject CreatePanel;

    public GameObject CreatedPanel;

    public TMP_Text RoomNameText;

    public GameObject CreateModePanel; 


    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        //string region = PlayerPrefs.GetString(PrefsKeys.Region);
        string region = "eu";
        SetPhotonRegion(region);
    }
    
    public void SwapPhotonRegion()
    {
        string selectedRegion = "";
        switch (regionDropdown.value)
        {
            case 0:
                selectedRegion = "";
                break;
            case 1:
                selectedRegion = "asia";
                break;
            case 2:
                selectedRegion = "au";
                break;
            case 3:
                selectedRegion = "cae";
                break;
            case 4:
                selectedRegion = "eu";

                break;
            case 5:
                selectedRegion = "in";

                break;
            case 6:
                selectedRegion = "jp";

                break;
            case 7:
                selectedRegion = "ru";

                break;
            case 8:
                selectedRegion = "rue";

                break;
            case 9:
                selectedRegion = "za";

                break;
            case 10:
                selectedRegion = "sa";

                break;
            case 11:
                selectedRegion = "kr";

                break;
            case 12:
                selectedRegion = "tr";

                break;
            case 13:
                selectedRegion = "us";

                break;
            case 14:
                selectedRegion = "usw";

                break;
        }

        SetPhotonRegion(selectedRegion);
    }

    public void SetPhotonRegion(string region)
    {
        PlayerPrefs.SetString(PrefsKeys.Region, region);
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region;
    }
    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
    }


    public void FindGameEvent()
    {
        gameMode = GameInstance.Instance._GameMode;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.JoinRandomRoom();
            
        }

        roomUI.ActiveRoomUI(true);
        roomUI.SetRoomInfor("1/1");
    }

    public override void OnConnectedToMaster()
    {
        //PhotonNetwork.GameVersion = Application.version;
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.NickName = playerData.displayName;
        PhotonNetwork.JoinLobby();
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("Still in Room");
        }

        //PhotonNetwork.JoinRandomRoom();
    }

    private void Update()
    {
        //Debug.Log( PhotonNetwork.CountOfRooms);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {

        CreateGameModeRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        EventHandlerUI.OnShowPopUpEvent("Error", "Please check your internet connection");
    }

    public override void OnCreatedRoom()
    {
        //Host init room properties
        CreatePanel.SetActive(false);
        CreatedPanel.SetActive(true);
        RoomNameText.text = PhotonNetwork.CurrentRoom.Name;
        Debug.Log("RoomCreated");
        
        gameMode.SetupRoomProperties();
    }

 

    public override void OnLeftRoom()
    {
        CreatePanel.SetActive(true);
        CreatedPanel.SetActive(false);
    }

    public override void OnJoinedRoom()
    {

        UpdateRoomUI();


        PoolManager.CleanUpPool();

        PhotonNetwork.LocalPlayer.SetWeapon(playerData.weaponID);

        if (PhotonNetwork.IsMasterClient)
        {
            SetNewPlayerTeam(PhotonNetwork.LocalPlayer);
            if (gameMode.IsReady(PhotonNetwork.CurrentRoom.PlayerCount))
            {
                Debug.Log("Game Is Ready");
                CloseRoom();
                StartCoroutine(WaitForGameReady());
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdateRoomUI();

        //Only let the master client handle this connection
        if (PhotonNetwork.IsMasterClient)
        {
            SetNewPlayerTeam(newPlayer);

            if (gameMode.IsReady(PhotonNetwork.CurrentRoom.PlayerCount))
            {
                Debug.Log("Game Is Ready");
                CloseRoom();
                StartCoroutine(WaitForGameReady());
            }
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateRoomUI();

        if (PhotonNetwork.IsMasterClient)
        {
            RemovePlayer(otherPlayer);
        }
    }

    public override void OnPlayerPropertiesUpdate(Player player, Hashtable changedProps)
    {
        if (player.IsLocal)
        {
            if (changedProps[PlayerExtensions.team] != null)
                _GameStatistic.masterTeam = Convert.ToInt32(changedProps[PlayerExtensions.team]);

            if (changedProps[PlayerExtensions.playerIndex] != null)
                _GameStatistic.masterIndex = Convert.ToInt32(changedProps[PlayerExtensions.playerIndex]);
        }

        if (changedProps[PlayerExtensions.team] != null)
            PhotonRoomStaticData.SavePlayerTeam(player.ActorNumber,player.NickName, Convert.ToInt32(changedProps[PlayerExtensions.team]));
        if (changedProps[PlayerExtensions.weapon] != null)
            PhotonRoomStaticData.SavePlayerWeapon(player.ActorNumber, changedProps[PlayerExtensions.weapon].ToString());
           
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
       
    }

    

    public void CreateGameModeRoom()
    {

        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Connecting photon....");
            PhotonNetwork.ConnectUsingSettings();
            return;
        }


        if (!PhotonNetwork.NetworkingClient.IsConnected)
        {
            Debug.Log("Connecting photon client....");
            PhotonNetwork.NetworkingClient.ConnectToMasterServer();
            return;
        }

        if (PhotonNetwork.InRoom)
        {
            Debug.Log("Already Inside a Room");
            return;
        }

        //if (!PhotonNetwork.InLobby)
        //{
        //    Debug.Log("Not in lobby");
        //    PhotonNetwork.JoinLobby(TypedLobby.Default);
        //    return;
        //}


        try
        {
            gameMode = UIMainMenu.instance.gameModes[0];
            GameInstance.Instance.SetGameMode(gameMode);
            //joining failed so try to create our own room
            RoomOptions roomOptions = new RoomOptions
            {
                IsVisible = IsVisible.isOn,
                IsOpen = IsOpen.isOn,
                MaxPlayers = 0,
                CustomRoomPropertiesForLobby = new string[] { "mode" },
                CustomRoomProperties = new Hashtable() { { "mode", gameMode.modeID } },
                CleanupCacheOnLeave = false,
                BroadcastPropsChangeToAll = false

            };

            //same as in OnCennectedToMaster() method above, here we are setting room properties for matchmaking
            //comment out for fully random matchmaking
            //RoomOptions roomOptions = new RoomOptions();
            //roomOptions.CustomRoomPropertiesForLobby = new string[] { "mode" };
            //roomOptions.CustomRoomProperties = new Hashtable() { { "mode", gameMode.modeID } };

            //roomOptions.MaxPlayers = (byte)gameMode.maxPlayer;
            //roomOptions.CleanupCacheOnLeave = false;
            //roomOptions.BroadcastPropsChangeToAll = false;
            PhotonNetwork.CreateRoom(RoomName.text, roomOptions);
            Debug.Log("Rached the end");
        }
        catch (Exception ex)
        {
            Debug.Log(ex +"Try Again");
        }

      
    }

    private void SetNewPlayerTeam(Player player)
    {
        int teamIndex = gameMode.GetTeamFill();
        PhotonNetwork.CurrentRoom.AddSize(teamIndex, +1);
        player.SetPlayerTeamIndex(teamIndex, gameMode.currentPlayerIndex);
    }

    private void RemovePlayer(Player player)
    {
        int teamIndex = player.GetTeam();
        PhotonNetwork.CurrentRoom.AddSize(teamIndex, -1);
    }

    private IEnumerator WaitForGameReady()
    {
        yield return new WaitForSeconds(2f);

        StartGameEvent();
    }
    private void UpdateRoomUI()
    {
        roomUI.SetRoomInfor($"{PhotonNetwork.CurrentRoom.PlayerCount}/{gameMode.maxPlayer}");
        //if (PhotonNetwork.IsMasterClient) roomUI.ActiveStartGameButton();
    }

    private void CloseRoom()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
    }

    [BoxGroup("Debug Test")]
    [Button(ButtonSizes.Large)]
    public void StartGameEvent()
    {

        PhotonNetwork.CurrentRoom.SetStartedTime(PhotonNetwork.ServerTimestamp);
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(PhotonEventCodes.StartLoadGameScene, null, raiseEventOptions, SendOptions.SendReliable);     
    }

    public void StartLoadGameScene()
    {
        StartCoroutine(LoadGameScene());
        EventHandlerUI.ActiveLoadingWithProcess(true);
    }


    private IEnumerator LoadGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(gameMode.gameScene.SceneName);

        while (!asyncLoad.isDone)
        {
            EventHandlerUI.SetLoadingProcessEvent($"Loading Scene... {asyncLoad.progress * 100} %", asyncLoad.progress);
            yield return null;
        }
    }

    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        //if (PhotonNetwork.IsConnected)
        //{
        //    PhotonNetwork.Disconnect();
        //}

        roomUI.ActiveRoomUI(false);
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;

        if (eventCode == PhotonEventCodes.StartLoadGameScene)
        {
            StartLoadGameScene();
        }
    }
    
}

