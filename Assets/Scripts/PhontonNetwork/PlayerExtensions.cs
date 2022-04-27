using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public static class PlayerExtensions
{

    public const string team = "team";
    public const string playerIndex = "index";
    public const string health = "health";
    public const string maxHealth = "maxHealth";

    public const string weapon = "weapon";

    public const string kill = "kill";
    public const string death = "death";

    public const string isReady = "isReady";
    public static string GetName(this PhotonView player)
    {
        return player.Owner.NickName!=null?player.Owner.NickName:"User123";
    }


    public static int GetTeam(this PhotonView player)
    {
        return player.Owner.GetTeam();
    }

    public static string GetWeapon(this PhotonView player)
    {
        return player.Owner.GetWeapon();
    }

    public static string GetWeapon(this Player player)
    {
        if (player.CustomProperties.ContainsKey(weapon))
            return System.Convert.ToString(player.CustomProperties[weapon]);
        else return "unarmed";
    }

    public static void SetWeapon(this PhotonView player, string weaponID)
    {
        player.Owner.SetWeapon(weaponID);
    }

    public static void SetWeapon(this Player player, string weaponID)
    {
        player.SetCustomProperties(new Hashtable() { { weapon, weaponID } });
    }

    public static int GetTeam(this Player player)
    {
        return System.Convert.ToInt32(player.CustomProperties[team]);
    }


    public static void SetTeam(this PhotonView player, int teamIndex)
    {
        player.Owner.SetTeam(teamIndex);
    }


    public static void SetTeam(this Player player, int teamIndex)
    {
        player.SetCustomProperties(new Hashtable() { { team, (byte)teamIndex } });
    }

    public static void SetPlayerTeamIndex(this Player player, int teamIndex, int index)
    {
        player.SetCustomProperties(new Hashtable() { { team, (byte)teamIndex }, { playerIndex, (byte)index } });
    }

    public static void SetPlayerReady(this Player player)
    {

        player.SetCustomProperties(new Hashtable() { { isReady, true } });
    }

    public static bool IsReady(this Player player)
    {
        return player.CustomProperties[isReady] != null;
    }

    public static int GetIndex(this PhotonView player)
    {
        return player.Owner.GetIndex();
    }

    public static int GetIndex(this Player player)
    {
        return System.Convert.ToInt32(player.CustomProperties[playerIndex]);
    }

    public static void SetPlayerIndex(this PhotonView player, int playerIndex)
    {
        player.SetPlayerIndex(playerIndex);
    }
    public static void SetPlayerIndex(this Player player, int index)
    {
        player.SetCustomProperties(new Hashtable() { { playerIndex, (byte)index } });
    }

    public static void SetupDefaultProperties(this Player player)
    {
        Hashtable properties = new Hashtable();
        properties.Add(kill, 0);
        properties.Add(death, 0);
        player.SetCustomProperties(properties);
    }

    public static void SetKillScore(this Player player)
    {
        int current = GetKill(player);
        current++;
        player.SetCustomProperties(new Hashtable() { { kill, (byte)current } });

    }
    public static int GetKill(this Player player)
    {
        return System.Convert.ToInt32(player.CustomProperties[kill]);
    }

    public static void SetDeathScore(this Player player)
    {
        int current = GetDeath(player);
        current++;
        player.SetCustomProperties(new Hashtable() { { death, (byte)current } });

    }

    public static int GetDeath(this Player player)
    {
        return System.Convert.ToInt32(player.CustomProperties[death]);
    }
}

