using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public static class RoomExtensions
{

    /// The key for accessing team fill per team out of the room properties.
    public const string teamMemberSize = "size";


    /// The key for accessing player scores per team out of the room properties.
    public const string score = "score";

    // Key for accessing game started time for timeout
    public const string startedTime = "startedTime";

    /// Returns the networked team fill for all teams out of properties.
    public static int[] GetSize(this Room room)
    {
        return (int[])room.CustomProperties[teamMemberSize];
    }


    /// Increases the team fill for a team by one when a new player joined the game.
    /// This is also being used on player disconnect by using a negative value.
    public static int[] AddSize(this Room room, int teamIndex, int value)
    {
        int[] sizes = room.GetSize();
        sizes[teamIndex] += value;

        room.SetCustomProperties(new Hashtable() { { teamMemberSize, sizes } });
        return sizes;
    }

    public static void SetStartedTime(this Room room, float time)
    {
        room.SetCustomProperties(new Hashtable() { { startedTime, time } });
    }

    public static float StartedTime(this Room room)
    {
        return (float)room.CustomProperties[startedTime];
    }
    /// Returns the networked team scores for all teams out of properties.
    public static int[] GetScore(this Room room)
    {
        return (int[])room.CustomProperties[score];
    }


    /// Increase the score for a team by one when a new player scored a point for his team.
    public static int[] AddScore(this Room room, int teamIndex, int value)
    {
        int[] scores = room.GetScore();
        scores[teamIndex] += value;

        room.SetCustomProperties(new Hashtable() { { score, scores } });
        return scores;
    }

}
