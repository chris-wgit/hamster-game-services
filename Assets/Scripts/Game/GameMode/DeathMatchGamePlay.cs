using UnityEngine;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Game/DeathMatchGameData")]
public class DeathMatchGamePlay : GameplaySO
{
    [BoxGroup("Gameplay Infor")]
    public int maxScore;

    public override bool IsReady(int playerCount)
    {
        return playerCount >= maxPlayer;
    }

    public override bool IsGameOver(int[] currentScore)
    {
        isGameOver = false;
        for (int i = 0; i < currentScore.Length; i++)
        {
            if (currentScore[i] >= maxScore) 
            {
                isGameOver = true;
            }
        }
        return isGameOver;
    }

    public override bool IsGameOverTimeOut(int[] currentScore)
    {
        Debug.Log("Current score " + currentScore);
        int highestScore = 0;
        int sameScoreTeam=0;
        for (int i = 0; i < currentScore.Length; i++)
        {
            if (currentScore[i] >= maxScore)
            {
                return true;
            }
            if(currentScore[i] > highestScore)
            {
                highestScore = currentScore[i];
                sameScoreTeam = 1;
            }
            else if (currentScore[i] == highestScore)
            {
                sameScoreTeam++;
            }
            Debug.Log(sameScoreTeam);
        }
        if (sameScoreTeam <= 1 ) return true;
        return false;
    }

    public override int GetTeamFill()
    {
        return base.GetTeamFill();
    }

    public override void SetupRoomProperties()
    {
        base.SetupRoomProperties();
        Hashtable roomProps = new Hashtable();
        roomProps.Add(RoomExtensions.teamMemberSize, new int[teamSize]);
        roomProps.Add(RoomExtensions.score, new int[teamSize]);

        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProps);
    }

}




