using UnityEngine;
using Sirenix.OdinInspector;
using Photon.Pun;
using System;

[CreateAssetMenu(menuName = "Projectiles/Projectile")]
public class ProjectileSkill : DamagedSkill
{

    [BoxGroup("Statistic", order:4),SerializeField]
    private int range;
    [BoxGroup("Statistic")]
    public float speed;
    [BoxGroup("Statistic"), ShowInInspector]
    public float lifeTime { get { return Range / speed; } }

    public float Range { get { return range * 0.1f; } }

    public override void Action(Character owner)
    {
        
    }

    public override void Action(Character caster, LocalTransform transformData)
    {
        if (damagedObject != null)
        {
            GameObject go = PoolManager.Spawn(damagedObject, transformData.position, transformData.rotation);
            Projectile proj = go.GetComponent<Projectile>();
            proj.SetOwner(caster);
            proj.InitProjectiles(true);
        }
    }
    public override bool IsCharacterStateCanAction(Character character)
    {
        return (character.ConditionState.CurrentState == CharacterConditions.Normal || character.ConditionState.CurrentState == CharacterConditions.Invulrnerable)
            && character.MovementState.CurrentState != MovementStates.Attacking;             
    }
    public override Vector3 DestinationFromInput(Vector2 input, Transform normalizedTransform)
    {
        Vector3 destination = new Vector3(input.x, 0, input.y);
        destination = destination.z * normalizedTransform.forward + destination.x * normalizedTransform.right;
        destination.y = 0f;
        return destination;
    }

    public override void EndAction()
    {

    }

    public override void Action(Character owner, LocalTransform transformData, int timestamp)
    {
        if (damagedObject != null)
        {
            GameObject go = PoolManager.Spawn(damagedObject, transformData.position, transformData.rotation);
            Projectile proj = go.GetComponent<Projectile>();
            proj.SetOwner(owner);
            proj.InitProjectiles(false, timestamp);
        }
    }
}
