using UnityEngine;
using Photon.Pun;
using Cysharp.Threading.Tasks;
using System;

public class CharacterAbility : MonoBehaviour
{
    protected Character _character;
    protected InputController _inputController;
    protected CharacterMovement _characterMovement;
    protected Health _health;
    protected StateMachine<MovementStates> _movementState;
    protected StateMachine<CharacterConditions> _conditionState;
    protected CharacterAnimationController _animation;
    protected PhotonView photonView;

    protected GameObject closestEnemy;

    protected bool isInputInitialized = false;

    protected Transform normalizedTransform;

    protected virtual void Awake()
    {
        PreInitialization();
    }

    //Get Character Components
    protected virtual void PreInitialization()
    {
        _character = GetComponent<Character>();
        photonView = GetComponent<PhotonView>();
        _inputController = GetComponent<InputController>();
        _characterMovement = GetComponent<CharacterMovement>();
        _health = GetComponent<Health>();
        _animation = GetComponentInChildren<CharacterAnimationController>();
    }

    //Init Ability Data after character initialized
    public virtual void Initialization()
    {
        _movementState = _character.MovementState;
        _conditionState = _character.ConditionState;

        normalizedTransform = _character.normalizedTransform;
        isInputInitialized = true;
    }

    
    public virtual void HandleInput()
    {

    }

    public virtual void HandleInput(Vector2 input)
    {

    }


    protected GameObject GetClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject currentEnemy in _character.gameStatistics.cachedTargetPlayer)
        {
            if (currentEnemy != null) 
            {
                float distanceToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
                if (distanceToEnemy < distanceToClosestEnemy)
                {
                    distanceToClosestEnemy = distanceToEnemy;
                    closestEnemy = currentEnemy;
                }
            }
        }
        return closestEnemy;

    }

    protected virtual bool IsSkillActionable() { return false; }
    protected Vector3 GetTargetDirection()
    {
        Vector3 targetDirection = transform.forward;

        //Get nearest target
        closestEnemy = GetClosestEnemy();

        if (closestEnemy != null)
        {
            Vector3 direction = closestEnemy.transform.position - transform.position;
            if(direction.sqrMagnitude <= 144) //check if target too far
            {
                targetDirection = direction;
            }
        }
        return targetDirection;
    }

    public CharacterMovement GetMovement()
    {
        return _characterMovement;
    }

    protected virtual void Update()
    {

    }
}
