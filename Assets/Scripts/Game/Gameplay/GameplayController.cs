using Photon.Pun;
using UnityEngine;
using Sirenix.OdinInspector;
using Photon.Realtime;
using DesertFoxTeam;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using System;
using System.Collections;
using Cinemachine;

public class GameplayController : Singleton<GameplayController>, IOnEventCallback
{
    public GameplaySO gameplay;

    public GameStatisticSO _GameStatistic;

    public PlayerDataSO playerData;
    public CharacterLibrarySO characterLibrary;

    public CooldownTimer respawnCooldown;

    GameObject localPlayer;
    

    public SpectatorCameraController spectatorCameraController;

    public StateMachine<GameState> _GameState;

    public float currentTimeOut;

    private bool isInstatiated;

    protected override void Awake()
    {
        base.Awake();
        respawnCooldown = new CooldownTimer();
        respawnCooldown.Initialization(gameplay.respawnTime);
        
        _GameState = new StateMachine<GameState>(gameObject, true);

        isInstatiated = false;

    }

    private void OnEnable()
    {
        EventHandlerGameplay.OnMasterCharacterDead += StartRespawnCooldown;
        respawnCooldown.OnTimerCompleteEvent += RespawnCharacter;
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        EventHandlerGameplay.OnMasterCharacterDead -= StartRespawnCooldown;
        respawnCooldown.OnTimerCompleteEvent -= RespawnCharacter;
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void Start()
    {
        gameplay = GameInstance.Instance._GameMode;

        _GameStatistic.cachedTargetPlayer.Clear();
        PhotonNetwork.LocalPlayer.SetPlayerReady();

        ChangeGameState(GameState.WaitConnection);

        StartWaitConnection();

    }

    private void StartWaitConnection()
    {
        StartCoroutine(CoWaitConnection());
    }

    private IEnumerator CoWaitConnection()
    {
        currentTimeOut = gameplay.connectionTimeOut;
        currentTimeOut -= Extensions.SecondFromFloat(PhotonNetwork.ServerTimestamp - PhotonNetwork.CurrentRoom.StartedTime());
        while (currentTimeOut > 0) yield return null;
        //TimeOut End, start game
        if (PhotonNetwork.IsMasterClient && _GameState.CurrentState == GameState.WaitConnection)
            StartBattleGameEvent();
    }

    public void StartBattleGameEvent()
    {

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(PhotonEventCodes.StartBattle, null, raiseEventOptions, SendOptions.SendReliable);
    }
    public void BattleReady()
    {
        SpawnCharacter();
        ChangeGameState(GameState.Playing);
    }

    private void SpawnCharacter()
    {
        

        if (PhotonNetwork.LocalPlayer.NickName == "Spectator")
        {
            CinemachineCameraController cameraController = FindObjectOfType<CinemachineCameraController>();
            cameraController.gameObject.SetActive(false);

            spectatorCameraController.gameObject.SetActive(true);

           StartCoroutine( FollowRandomPlayer());

            GameObject controller = GameObject.Find("Controller");

            for (int j = 1; j < controller.transform.childCount; j++)
            {
                controller.transform.GetChild(j).gameObject.SetActive(false);
            }

        }
        else
        {
            if (!isInstatiated)
            {
                Transform spawnPosition = SpawnBaseManager.instance.GetSpawnPosition(_GameStatistic.masterTeam, _GameStatistic.masterIndex);
                string characterName;
                if (gameplay.modeCharacter.Count > 0)
                {
                    int randomCharacter = UnityEngine.Random.Range(0, gameplay.modeCharacter.Count);
                    characterName = gameplay.modeCharacter[randomCharacter].characterPrefab.name;
                }
                else
                {
                    characterName = characterLibrary.CharacterList[playerData.currentCharacter].characterPrefab.name;
                }

                localPlayer = PhotonNetwork.Instantiate(characterName, spawnPosition.position, spawnPosition.rotation);
                localPlayer.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);

                isInstatiated = true;
            }
        }
       
    }

    private IEnumerator FollowRandomPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        Character[] characters = FindObjectsOfType<Character>();
        if (characters.Length != 0)
        {
            int index = UnityEngine.Random.Range(0, characters.Length);
            spectatorCameraController.Target = characters[index].gameObject.transform;
        }

        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 8));
        StartCoroutine(FollowRandomPlayer());
    }

    private void StartRespawnCooldown(Player killer)
    {
        if (_GameState.CurrentState == GameState.GameOver) return;
        ChangeGameState(GameState.Respawn);
        respawnCooldown.StartCooldown();
    }

    private void RespawnCharacter()
    {
        _GameStatistic.MasterCharacter.GetComponent<Character>().ResetCharacterStates();
        ChangeGameState(GameState.Playing);
    }

    private void Update()
    {
    
    
        if (respawnCooldown.IsActive) respawnCooldown.Update();
        if(_GameState.CurrentState == GameState.GameOver)
        {
            respawnCooldown.Pause();
        }
    }

    public void OnEvent(EventData photonEvent)
    {
        if(photonEvent.Code == PhotonEventCodes.StartBattle)
        {
            BattleReady();
        }

        if(photonEvent.Code == PhotonEventCodes.GameOver)
        {
            Debug.Log("Game Over");
            SetGameOver((int)photonEvent.CustomData);
        }

        if(photonEvent.Code == PhotonEventCodes.ExtraTime)
        {
            UIGameManager.instance.AddExtraTime((int)photonEvent.CustomData);
        }
    }

    private void SetGameOver(int winnerTeam)
    {
        _GameStatistic.winnerTeam = winnerTeam;
        ChangeGameState(GameState.GameOver);

        StartCoroutine(CoDisconnect());
    }

    private IEnumerator CoDisconnect()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            while (PhotonNetwork.InRoom) yield return null;
        }
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
            while (PhotonNetwork.IsConnected) yield return null;
        }
    }

    private void ChangeGameState(GameState state)
    {
        _GameState.ChangeState(state);
        EventHandlerGameplay.GameStateChangedEvent(state);
    }

    public void CallTimeOut()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        if (GameInstance.Instance._GameMode.IsGameOverTimeOut(PhotonNetwork.CurrentRoom.GetScore()))
        {
            Debug.Log("GameOver");
            CallGameOver();
        }
        else
        {
            Debug.Log("CallExtraTime");
            CallExtraTime();
        }
    }

    public void CallExtraTime()
    {
        int data = gameplay.bonusTime;
        RaiseEventOptions option = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(PhotonEventCodes.ExtraTime, data, option, SendOptions.SendReliable);
    }
    public void RaiseGameOverEvent(int winnerTeam) 
    {
        int data = winnerTeam;

        RaiseEventOptions option = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(PhotonEventCodes.GameOver, data, option, SendOptions.SendReliable);
    }

    public void CallGameOver()
    {
        RaiseGameOverEvent(GetHighestScoreTeam(PhotonNetwork.CurrentRoom.GetScore()));
    }
    public int GetHighestScoreTeam(int[] scoreData)
    {
        int highestScore = 0;
        int winnerTeam = 0;
        for (int i = 0; i < scoreData.Length; i++)
        {
            if (scoreData[i] > highestScore)
            {
                highestScore = scoreData[i];
                winnerTeam = i;
            }
        }
        return winnerTeam;
    }
}

[System.Serializable]
public class Team
{
    public string teamName;
    public int teamIndex;
    public TeamSpawn[] spawnBase;
}

[System.Serializable]
public class TeamSpawn
{
    public int index;
    public Transform transform;
}

public enum GameState
{
    WaitConnection,
    Playing,
    Respawn,
    GameOver
}

