using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : CharacterAbility
{
    private CharacterController _characterController;

    //Movement Data
    public float movementSpeed { get; private set; }
    public float rotationSpeed = 300;

    [HideInInspector]
    public Vector2 movementInput;

    public bool inAction;

    public override void Initialization()
    {
        base.Initialization();
        _characterController = GetComponent<CharacterController>();
        movementSpeed = _character.data.MovementSpeed;
    }

    public void SetCharacterSpeed(float value)
    {
        movementSpeed = value;
    }

    private bool IsMoveable()
    {
        return _conditionState.CurrentState == CharacterConditions.Normal || _conditionState.CurrentState == CharacterConditions.Invulrnerable|| _conditionState.CurrentState == CharacterConditions.Damaged;
    }

    private bool IsRotateable()
    {
        return (_movementState.CurrentState != MovementStates.Attacking || _movementState.CurrentState != MovementStates.Ultimate)
            &&(_conditionState.CurrentState != CharacterConditions.Disabled || _conditionState.CurrentState != CharacterConditions.Dead);
    }
    protected override void Update()
    {
        base.Update();
        movementInput = _inputController.leftJoystickInput;
    }

    private void FixedUpdate()
    {

        if (!isInputInitialized) return;
        if (!IsMoveable()) return;

        if (movementInput != Vector2.zero)
        {
            ProcessMovement(movementInput);
        }
        else
        {
            if (_movementState.CurrentState == MovementStates.Walking)
            {
                _movementState.ChangeState(MovementStates.Idle);
            }
        }

    }

    public void ProcessMovement(Vector2 movementInput)
    {
        HandleMovement(movementInput);
    }

    protected virtual void HandleMovement(Vector2 movementInput)
    {

        SetMovement(movementInput);

        //If character is attacking, don't change character state
        if (_movementState.CurrentState == MovementStates.Attacking) return;

        //Set character state
        _movementState.ChangeState(MovementStates.Walking);

    }
    //Character Movement
    private void SetMovement(Vector2 movementInput)
    {
        Vector3 movementDirection = new Vector3(movementInput.x, 0, movementInput.y);
        movementDirection.Normalize();
        movementDirection = movementDirection.z * normalizedTransform.forward + movementDirection.x * normalizedTransform.right;
        movementDirection.y = 0f;
        _characterController.Move(movementDirection * movementSpeed * Time.deltaTime);
        if (IsRotateable())
        {
            SetRotation(movementDirection);
        }
    }

    //Rotate character
    public void SetRotation(Vector3 direction)
    {
        Quaternion towardRotation = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, towardRotation, rotationSpeed * Time.deltaTime);
    }

    public void SetRotation(Vector3 direction, float speed)
    {
        Quaternion towardRotation = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, towardRotation, speed * Time.deltaTime);
    }


}