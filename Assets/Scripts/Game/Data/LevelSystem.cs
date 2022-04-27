using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem :ScriptableObject
{
    private int level;
    private int expCurrent;
    private int expRequirement;

    //public int 

    public void AddExperience(int amount)
    {
        expCurrent += amount;
        if(expCurrent >= expRequirement)
        {
            level++;
            expCurrent -= expRequirement;
        }
    }
}


