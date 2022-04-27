using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PhotonRoomStatisticManager : MonoBehaviourPunCallbacks
{
    private GameplaySO _Gameplay;

    private GameStatisticSO _GameStatistic;

    private GameplayController _GameController;

    [Header("Broadcasting Event")]
    public IntEventChannelSO _OnMasterTeamScoreChanged;

    public IntEventChannelSO _OnEnemyTeamScoreChanged;

    private void Awake()
    {
        _GameController = GameplayController.instance;
        _Gameplay = GameInstance.Instance._GameMode;
        _GameStatistic = GameInstance.Instance._GameStatistic;
        EventHandlerGameplay.OnMasterCharacterDead += UpdateGameStatistic;
    }

    private void OnDestroy()
    {
        EventHandlerGameplay.OnMasterCharacterDead -= UpdateGameStatistic;
    }

    private void Start()
    {
        _GameStatistic.cachedTargetPlayer = new List<GameObject>();
    }


    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        base.OnRoomPropertiesUpdate(propertiesThatChanged);

        if (propertiesThatChanged.ContainsKey(RoomExtensions.score))
        {
            UpdateScore((int[])propertiesThatChanged[RoomExtensions.score]);
        }

        if (!PhotonNetwork.IsMasterClient) return;
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        if (changedProps.ContainsKey(PlayerExtensions.isReady))
        {
            if (AllPlayerReady())
            {
                _GameController.StartBattleGameEvent();
            }
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.AddSize(otherPlayer.GetTeam(), -1);

            if (IsOtherTeamOut()) _GameController.RaiseGameOverEvent(PhotonNetwork.MasterClient.GetTeam());
        }
    }

    private bool IsOtherTeamOut()
    {
        int[] teamSize = PhotonNetwork.CurrentRoom.GetSize();
        int currentOnlineTeam = 0;
        for (int i = 0; i < teamSize.Length; i++)
        {
            if (teamSize[i] > 0) currentOnlineTeam++;
        }
        if (currentOnlineTeam > 1) return false;
        return true;
    }
    private void UpdateGameStatistic(Player killer)
    {
        //Update Score
        PhotonNetwork.CurrentRoom.AddScore(killer.GetTeam(), 1);
        //Update player stats
        killer.SetKillScore();
    }

    private bool AllPlayerReady()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.IsReady() == false)
                return false;
        }
        return true;
    }

    private void UpdateScore(int[] scoreData)
    {
        for (int i = 0; i < scoreData.Length; i++)
        {
            if (i == _GameStatistic.masterTeam)
            {
                _OnMasterTeamScoreChanged.RaiseEvent(scoreData[i]);
            }
            else
            {
                _OnEnemyTeamScoreChanged.RaiseEvent(scoreData[i]);
            }
        }

        if (PhotonNetwork.IsMasterClient)
        {
            if (_Gameplay.IsGameOver(scoreData))
            {
                _GameController.CallGameOver();
            }
        }
    }


}