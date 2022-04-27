using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience
{
    static int _experience = 0;
    static int level;
    static int _levelMax = 1000;

    public static PlayerExpData GetPlayerExpData(int currentLevel, int currentExp)
    {
        PlayerExpData newData;
        level = currentLevel;
        experience = currentExp;
        newData.level = level;
        newData.experience = experience;

        return newData;
    } 
    public static int experience
    {
        get { return _experience; }
        set
        {
            if (value <= _experience)
            {
                // decrease
                _experience = Math.Max(value, 0);
            }
            else
            {
                // increase with level ups
                // set the new value (which might be more than expMax)
                _experience = value;

                // now see if we leveled up (possibly more than once too)
                // (can't level up if already max level)
                while (_experience >= maxExp && level < _levelMax)
                {
                    // subtract current level's required exp, then level up
                    _experience -= maxExp;
                    ++level;

                }

                // set to expMax if there is still too much exp remaining
                if (_experience > maxExp) _experience = maxExp;
            }
        }
    }

    // required experience grows by 10% each level (like Runescape)
    [SerializeField] public static ExponentialInt _maxExp = new ExponentialInt { multiplier = 100, baseValue = 1.1f };
    public static int maxExp { get { return _maxExp.Get(level); } }


}

[Serializable]
public struct ExponentialInt
{
    public int multiplier;
    public float baseValue;
    public int Get(int level) => Convert.ToInt32(multiplier * Mathf.Pow(baseValue, (level - 1)));
}

public struct PlayerExpData
{
    public int level;
    public int experience;
}
