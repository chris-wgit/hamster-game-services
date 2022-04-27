using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRenderer : CharacterAbility
{
    [ShowInInspector]
    public List<IShaderFX> renderObject = new List<IShaderFX>();

    private CharacterController controller;

    public List<HiddenShader> hiddenPlaces = new List<HiddenShader>();

    private bool isFading;
    private bool isHiding;

    public Color damagedColor;
    private int previousHealth;
    public GameObject healingFX;

    private float outline;
    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<CharacterController>();
        renderObject = new List<IShaderFX>();
    }

    private void OnEnable()
    {
        _character.ConditionState.OnStateChange += UpdateRenderer;
        _health.OnCurrentHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _character.ConditionState.OnStateChange -= UpdateRenderer;
        _health.OnCurrentHealthChanged -= OnHealthChanged;
    }

    private void Start()
    {
        previousHealth = _health.currentHealth;
    }

    public void AddObjectToRenderer(IShaderFX obj)
    {
        renderObject.Add(obj);
    }

    public void RemoveFromRenderer(IShaderFX obj)
    {
        if (renderObject.Contains(obj))
            renderObject.Remove(obj);
    }

    private void UpdateRenderer(CharacterConditions state)
    {
        switch (state)
        {
            case CharacterConditions.Invulrnerable:
                ToggleCharacter(true);
                break;

            case CharacterConditions.Normal:
                break;

            case CharacterConditions.Disabled:
                break;

            case CharacterConditions.Dead:
                ToggleCharacter(false);
                break;

            default:
                break;
        }
    }

    private void ToggleCharacter(bool value)
    {
        SetOpaqueShader();
        hiddenPlaces.Clear();
        ToggleCharacterRender(value);
        controller.enabled = value;


    }

    private void ToggleCharacterRender(bool value)
    {
        for (int i = 0; i < renderObject.Count; i++)
        {
            renderObject[i]._GameObject().SetActive(value);
        }
        isHiding = !value;
    }

    private void SetFadingShader()
    {
        foreach (var item in renderObject)
        {
            item.SetOpacity();
        }
        isFading = true;
    }

    private void SetHiddenShader()
    {
        foreach (var item in renderObject)
        {
            item.SetHidden();
        }
        isHiding = true;
        _character.RemoveObjectFromCachedTargetObject();
    }

    private void SetOpaqueShader()
    {
        foreach (var item in renderObject)
        {
            item.SetOpaque();
        }
        isFading = false;
        isHiding = false;
        if (_character.IsTargetableObject()) _character.AddObjectToCachedTargetObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 15) return;
        HiddenShader hiddenPlace = other.gameObject.transform.GetChild(0).GetComponent<HiddenShader>();
        if (hiddenPlace != null)
        {
            hiddenPlaces.Add(hiddenPlace);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 15) return;
        HiddenShader hiddenPlace = other.transform.GetChild(0).GetComponent<HiddenShader>();
        if (hiddenPlace && hiddenPlaces.Contains(hiddenPlace))
        {
            hiddenPlaces.Remove(hiddenPlace);
        }
    }

    protected override void Update()
    {
        if (hiddenPlaces.Count > 0)
        {
            if (IsFriendlyCharacter() && !isFading)
            {
                SetFadingShader();
            }
            if (!IsFriendlyCharacter() && !isHiding && CanHideState() && IsHiddenPlaceCanHide())
            {
                SetHiddenShader();
            }
            if (isHiding && (!CanHideState() || !IsHiddenPlaceCanHide()))
            {
                SetOpaqueShader();
            }
        }
        else
        {
            if (isFading || isHiding) SetOpaqueShader();
        }
    }

    private void OnHealthChanged(int newHealth)
    {
        if (newHealth < previousHealth)
        {
            if (!isHiding && !isFading)
                SetDamagedEffect();
        }
        else if (newHealth > previousHealth)
        {
            if (!isHiding)
                HealingEffect();
        }

        previousHealth = newHealth;
    }

    private void SetDamagedEffect()
    {
        foreach (var item in renderObject)
        {
            item.SetColor(damagedColor);
        }
    }

    private void HealingEffect()
    {
        healingFX.SetActive(true);
    }

    private bool IsFriendlyCharacter()
    {
        if (_character.Team == GameInstance.Instance._GameStatistic.masterTeam) return true;
        return false;
    }

    private bool CanHideState()
    {
        if (_character.MovementState.CurrentState != MovementStates.Attacking &&
            _character.MovementState.CurrentState != MovementStates.Ultimate &&
            _character.ConditionState.CurrentState != CharacterConditions.Damaged
            ) return true;
        return false;
    }

    private bool IsHiddenPlaceCanHide()
    {
        foreach (var item in hiddenPlaces)
        {
            if (!item.CanHide()) return false;
        }
        return true;
    }
}