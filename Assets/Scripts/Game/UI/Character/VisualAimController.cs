using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VisualAimController : MonoBehaviour
{
    public InputController controller;
    public Character character;

    public GameObject aimObject;
    public SpriteRenderer aimGraphic;

    public Color skillColor;
    public Color ultimateColor;

    public WeaponSkill weaponSkill;

    private Vector2 currentDirection;

    private bool onAiming;

    private void OnEnable()
    {
        controller.controllerInput.CharacterController.FireValue.performed += OnWeaponSkill;
        controller.controllerInput.CharacterController.Action2Value.performed += OnWeaponUltimate;
        controller.controllerInput.CharacterController.Action2Value.canceled += OnWeaponUltimateCancelled;
    }


    private void OnDisable()
    {
        controller.controllerInput.CharacterController.FireValue.performed -= OnWeaponSkill;
        controller.controllerInput.CharacterController.Action2Value.performed -= OnWeaponUltimate;
        controller.controllerInput.CharacterController.Action2Value.canceled -= OnWeaponUltimateCancelled;
    }

    private void OnWeaponUltimateCancelled(InputAction.CallbackContext obj)
    {
        if (aimObject != null) aimObject.SetActive(false);
        onAiming = false;
    }

    private void Start()
    {
        if (aimObject != null) aimObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (onAiming)
        {
            Vector3 aimDirection = new Vector3(currentDirection.x, 0, currentDirection.y);
            aimDirection = aimDirection.z * character.normalizedTransform.forward + aimDirection.x * character.normalizedTransform.right;
            transform.rotation = Quaternion.LookRotation(aimDirection, Vector3.up);
        }
    }



    private void OnWeaponSkill(InputAction.CallbackContext context)
    {
        //if (!weaponSkill.cooldown.IsSkillAvailable()) return;
        currentDirection = context.ReadValue<Vector2>();
        if(currentDirection != Vector2.zero)
        {
            if (aimObject != null) aimObject.SetActive(true);
            aimGraphic.color = skillColor;
            onAiming = true;
        }
        else
        {
            if (aimObject != null) aimObject.SetActive(false);
            onAiming = false;
        }
       

       
    }
    private void OnWeaponUltimate(InputAction.CallbackContext context)
    {
        //if (!weaponUltimate.cooldown.IsSkillAvailable()) return;
        currentDirection = context.ReadValue<Vector2>();
        if (currentDirection != Vector2.zero)
        {
            if (aimObject != null) aimObject.SetActive(true);
            aimGraphic.color = ultimateColor;
            onAiming = true;
        }

    }
}
