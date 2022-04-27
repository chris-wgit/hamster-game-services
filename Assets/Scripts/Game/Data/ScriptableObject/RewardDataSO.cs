using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Reward Data", menuName = "Data/ Reward Data")]
public class RewardDataSO : ScriptableObject
{
    public int winGames;

    public int experience;
    public int trophy;

    public int gold;
    public int gem;
}
