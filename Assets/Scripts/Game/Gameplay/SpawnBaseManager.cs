using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SpawnBaseManager : Singleton<SpawnBaseManager>
{
    public Team[] teams;

    private Transform spawnPosition;

    public Transform GetSpawnPosition(int teamIndex, int playerIndex)
    {
        //Debug.Log($"Getting position for player team {teamIndex} index {playerIndex}");
        return teams[teamIndex].spawnBase[playerIndex].transform;
    }
}
