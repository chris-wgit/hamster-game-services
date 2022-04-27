using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System;

[CreateAssetMenu(fileName ="Dash", menuName = "Character Skill/Dash")]
public class CharacterDash : BuffSkill
{
    private CharacterMovement _movement;
    public float dashTime;
    public float dashSpeed;

    private float previousSpeed;
    private void SpeedUp()
    {
        previousSpeed = _movement.movementSpeed;
        _movement.SetCharacterSpeed(dashSpeed);
    }

    public override void Action(Character owner, LocalTransform transformData)
    {

    }

    public override void Action(Character owner)
    {
        _movement = owner.gameObject.GetComponent<CharacterMovement>();
        SpeedUp();
    }

    public override bool IsCharacterStateCanAction(Character character)
    {

        return character.MovementState.CurrentState != MovementStates.Attacking
                && (character.ConditionState.CurrentState == CharacterConditions.Normal || character.ConditionState.CurrentState == CharacterConditions.Invulrnerable)
                && character.MovementState.CurrentState == MovementStates.Walking;
    }

    public override void EndAction()
    {
        _movement.SetCharacterSpeed(previousSpeed);
    }

    public override void Action(Character owner, LocalTransform transformData, int timestamp)
    {
        throw new NotImplementedException();
    }
}