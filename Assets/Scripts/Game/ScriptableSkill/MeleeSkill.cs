using Sirenix.OdinInspector;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectiles/MeleeSkill")]
public class MeleeSkill : DamagedSkill
{

    [BoxGroup("Statistic"), ShowInInspector]
    public float lifeTime = 1;

    [BoxGroup("Statistic"), ShowInInspector]
    public bool followOwner = false;

    public override void Action(Character owner, LocalTransform transformData)
    {
        if (damagedObject != null)
        {
            GameObject go = PoolManager.Spawn(damagedObject.gameObject, transformData.position, transformData.rotation);
            DamageObject proj = go.GetComponent<DamageObject>();
            proj.SetOwner(owner);
        }
    }

    public override void Action(Character owner)
    {

    }

    public override void Action(Character owner, LocalTransform transformData, int timestamp)
    {
        if (damagedObject != null)
        {
            GameObject go = PoolManager.Spawn(damagedObject.gameObject, transformData.position, transformData.rotation);
            DamageObject proj = go.GetComponent<DamageObject>();
            proj.SetOwner(owner);
        }
    }

    public override void EndAction()
    {

    }

    public override bool IsCharacterStateCanAction(Character character)
    {
        return (character.ConditionState.CurrentState == CharacterConditions.Normal|| character.ConditionState.CurrentState == CharacterConditions.Invulrnerable)
                && character.MovementState.CurrentState != MovementStates.Attacking;
    }


}