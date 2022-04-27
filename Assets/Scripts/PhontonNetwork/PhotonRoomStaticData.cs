using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhotonRoomStaticData
{
    public static Dictionary<int, PlayerStaticData> playerList;


    public static void SavePlayerTeam(int actorID,string nickname, int team)
    {
        if (playerList == null) playerList = new Dictionary<int, PlayerStaticData>();

        if (playerList.ContainsKey(actorID))
        {
            playerList[actorID].nickName = nickname;
            playerList[actorID].team = team;
        }
        else
        {
            PlayerStaticData newPlayerData = new PlayerStaticData();
            newPlayerData.nickName = nickname;
            newPlayerData.team = team;
            playerList.Add(actorID, newPlayerData);

        }
    }

    public static void SavePlayerWeapon(int actorID, string weaponID)
    {
        if (playerList == null) playerList = new Dictionary<int, PlayerStaticData>();

        if (playerList.ContainsKey(actorID))
        {
            playerList[actorID].weaponID = weaponID;
        }
        else
        {
            PlayerStaticData newPlayerData = new PlayerStaticData();
            newPlayerData.weaponID = weaponID;
            playerList.Add(actorID, newPlayerData);

        }
    }


}

public class PlayerStaticData
{
    public string nickName;
    public int team;
    public string weaponID;

}