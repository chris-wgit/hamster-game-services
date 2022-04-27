using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item/BuffHealthItem")]
public class BuffHealthItem : ScriptableItem
{
    [BoxGroup("Statistic")]
    public int healthAmount;
    public override void Action(Character character,Action onComplete)
    {
        Health health = character.gameObject.GetComponent<Health>();
        if(health != null)
        {
            health.AddHealth((short)healthAmount);
            onComplete?.Invoke();
        }
    }

    public override bool IsUseable(Character character)
    {
        return true;
    }
}
