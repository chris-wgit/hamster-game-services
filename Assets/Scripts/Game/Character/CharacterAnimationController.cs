using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Character character;
    private Animator animator;

    public Action OnTriggerAction;
    public Action OnActionFinished;

    private int isRun = Animator.StringToHash("isRun");
    private int isDead = Animator.StringToHash("isDead");
    private int isAttack = Animator.StringToHash("isAttack");
    private int isUltimate = Animator.StringToHash("isUltimate");
    private int isDamaged = Animator.StringToHash("isDamaged");
    private int isPosing = Animator.StringToHash("isPosing");

    private void Awake()
    {
        character = GetComponentInParent<Character>();
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        if(character != null)
        {
            character.MovementState.OnStateChange += UpdateAnimation;
            character.ConditionState.OnStateChange += UpdateConditionState;
        }
        else
        {
            SetPosing();
        }

    }


    private void OnDisable()
    {
        if (character != null)
        {
            character.MovementState.OnStateChange -= UpdateAnimation;
            character.ConditionState.OnStateChange -= UpdateConditionState;
        }
    }

    private void UpdateConditionState(CharacterConditions condition)
    {
        switch (condition)
        {
            case CharacterConditions.Invulrnerable:
                break;
            case CharacterConditions.Normal:
                break;
            case CharacterConditions.Disabled:
                break;
            case CharacterConditions.Dead:
                break;
            case CharacterConditions.Damaged:
                //SetDamaged();
                break;
            default:
                break;
        }
    }

    private void UpdateAnimation(MovementStates states)
    {
        switch (states)
        {
            case MovementStates.Idle:
                SetIdle();
                break;
            case MovementStates.Walking:
                SetRun();
                break;
            case MovementStates.Dashing:
                break;
            case MovementStates.Attacking:
                SetAttack();
                break;
            case MovementStates.Ultimate:
                SetUltimate();
                break;
        }
    }

    private void SetPosing()
    {
        animator.SetTrigger(isPosing);
    }
    private void SetDamaged()
    {
        animator.SetTrigger(isDamaged);
    }
    private void SetRun()
    {
        animator.SetBool(isRun, true);
    }

    private void SetIdle()
    {
        animator.SetBool(isRun, false);
        animator.SetBool(isRun, false);
    }

    private void SetAttack()
    {
        animator.SetTrigger(isAttack);
    }

    private void SetDead()
    {
        animator.SetBool(isDead, true);
    }

    private void SetUltimate()
    {
        animator.SetTrigger(isUltimate);
    }

    public void WaitTrigger(Action onTriggered)
    {
        OnTriggerAction = onTriggered;
    }

    public void WaitEndTrigger(Action onEndTriggered)
    {
        OnActionFinished = onEndTriggered;
    }

    public void TriggerAction()
    {
        OnTriggerAction?.Invoke();
    }

    public void FinishAction()
    {
        OnActionFinished?.Invoke();
    }

}

