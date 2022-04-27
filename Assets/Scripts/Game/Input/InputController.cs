using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

public class InputController : MonoBehaviour
{

    public CharacterControllerInput controllerInput;

    [HideInInspector]
    public Vector2 leftJoystickInput;

    private Vector2 rightJoysticInput = Vector2.zero;

    public Action<Vector2> OnLeftJoystickPerformed;
    public Action<Vector2> OnRightStickPerformed;
    public Action<Vector2> OnAction2;
    private Vector2 action2Input = Vector2.zero;
    public Action OnAction3;


    #region Initializing
    private void Awake()
    {
        if (controllerInput == null)
        {
            controllerInput = new CharacterControllerInput();
        }

    }


    private void OnEnable()
    {
        controllerInput.Enable();
        controllerInput.CharacterController.FireAction.canceled += FireAction;
        controllerInput.CharacterController.Action2.canceled += Action2;
        controllerInput.CharacterController.Action3.performed += Action3;
    }



    private void OnDisable()
    {
        controllerInput.Disable();
        controllerInput.CharacterController.FireAction.canceled -= FireAction;
    }

    #endregion

    private void FireAction(InputAction.CallbackContext obj)
    {
        OnRightStickPerformed?.Invoke(rightJoysticInput);
    }

    private void Action2(InputAction.CallbackContext obj)
    {
        OnAction2?.Invoke(action2Input);
    }
    private void Action3(InputAction.CallbackContext obj)
    {
        OnAction3?.Invoke();
    }

    private void Update()
    {
        //Read movement input from left joystick
        leftJoystickInput = controllerInput.CharacterController.Movement.ReadValue<Vector2>();
        rightJoysticInput = controllerInput.CharacterController.FireValue.ReadValue<Vector2>();
        action2Input = controllerInput.CharacterController.Action2Value.ReadValue<Vector2>();
    }
}

