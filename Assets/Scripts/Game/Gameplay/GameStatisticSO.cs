using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="Game Statistics", menuName ="Game/Game Statistics")]
public class GameStatisticSO : ScriptableObject
{
    [BoxGroup("Match Statistic")]
    [ShowInInspector]
    public GameObject MasterCharacter { get; protected set; }
    [BoxGroup("Match Statistic")]
    public int masterTeam;
    [BoxGroup("Match Statistic")]
    public int masterIndex;
    [BoxGroup("Cached GameObject")]
    public List<GameObject> cachedTargetPlayer;

    public int winnerTeam;
    protected int teamIndex;


    public UnityAction OnMasterCharacterSet;
    public void SetMasterCharacter(GameObject character)
    {
        if (character != null)
        {
            MasterCharacter = character;

            masterTeam = character.GetComponent<Character>().Team;

            cachedTargetPlayer = new List<GameObject>();

            OnMasterCharacterSet?.Invoke();

        }
    }
}
