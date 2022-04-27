using Photon.Pun;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Inspector;

public class GameplaySO : ScriptableObject
{
    [BoxGroup("Gameplay Infor")]
    public SceneField gameScene;

    [BoxGroup("Gameplay Infor")]
    public int modeID;

    [BoxGroup("Gameplay Infor")]
    public int maxPlayer;

    [BoxGroup("Gameplay Infor")]
    public int teamSize;

    [BoxGroup("Gameplay Infor")]
    public float respawnTime;

    [BoxGroup("Gameplay Infor")]
    public float connectionTimeOut = 15f;

    [BoxGroup("Gameplay Infor")]
    public float gameTime = 300f;

    [BoxGroup("Gameplay Infor")]
    public int bonusTime = 15;

    public List<CharacterDataSO> modeCharacter = new List<CharacterDataSO>();

    protected bool isGameOver = false;

    protected int teamIndex;

    [BoxGroup("Match Result")]
    public RewardDataSO winRewardData;

    [BoxGroup("Match Result")]
    public RewardDataSO lostRewardData;

    protected bool isReady;

    [HideInInspector]
    public int currentPlayerIndex;

    public virtual void SetupRoomProperties()
    {
    }

    public virtual bool IsReady(int playerCount)
    {
        return isReady;
    }

    public virtual bool IsGameOver(int[] score)
    {
        return isGameOver;
    }

    public virtual bool IsGameOverTimeOut(int[] score) 
    {
        return false;
    }

    public virtual int GetTeamFill()
    {
        int[] size = PhotonNetwork.CurrentRoom.GetSize();
        teamIndex = 0;

        currentPlayerIndex = size[0];
        for (int i = 0; i < size.Length; i++)
        {
            if (size[i] < currentPlayerIndex)
            {
                currentPlayerIndex = size[i];
                teamIndex = i;
            }
        }
        return teamIndex;
    }
}