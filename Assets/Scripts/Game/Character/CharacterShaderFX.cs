using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;

public class CharacterShaderFX : CharacterAbility
{
    [Required]
    public SkinnedMeshRenderer meshRenderer;
    private Material[] materials;
    public Color damagedColor;



    protected override void Awake()
    {
        base.Awake();
        materials = meshRenderer.materials;
    }
    private void OnEnable()
    {
        _character.ConditionState.OnStateChange += OnCharacterStateChanged;
    }

    private void OnDisable()
    {
        _character.ConditionState.OnStateChange -= OnCharacterStateChanged;
    }


    private void OnCharacterStateChanged(CharacterConditions condition)
    {
        switch (condition)
        {
            case CharacterConditions.Invulrnerable:
                SetInvictableMaterial();
                break;
            case CharacterConditions.Normal:
                if(_character.ConditionState.PreviousState == CharacterConditions.Invulrnerable)
                {
                    SetNormalMaterial();
                }
                break;
            case CharacterConditions.Disabled:
                break;
            case CharacterConditions.Dead:
                break;
            default:
                break;
        }
    }

    private void SetInvictableMaterial()
    {
        materials[0].SetFloat("_RimMin", 0f);

        materials[0].SetFloat("_RimMax", 0.5f);
    }

    private void SetNormalMaterial()
    {
        materials[0].DOFloat(2,"_RimMax", 2f).OnComplete(() => materials[0].SetFloat("_RimMin", 2f));
    }




    
}
