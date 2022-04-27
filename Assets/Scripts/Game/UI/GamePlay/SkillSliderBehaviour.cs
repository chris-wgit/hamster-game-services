using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class SkillSliderBehaviour : MonoBehaviour
{
    public UISliderBehaviour chargeSlider;
    public Image chargeBar;

    public UISliderBehaviour regenSlider;
    public Image regenBar;

    private Ability abilityData;

    public UISliderSeparator sliderChargeSeparator;
    public Image sliderChargeSeparatorBar;

    private bool isInitalized;
    public void Initializing(Ability ability)
    {
        SetupSlider(ability);
    }

    public void SetupSlider(Ability ability)
    {
        abilityData = ability;
        if(chargeSlider != null)
        {
            chargeSlider.SetupDisplay(ability.maxCharge);
            chargeSlider.SetCurrentValue(ability.currentCharge);
            ability.onCurrentChargeChanged += UpdateCharge;
        }
        if(regenSlider != null)
        {
            regenSlider.SetupDisplay(ability.skillData.totalCooldown);
            regenSlider.SetCurrentValue(ability.skillData.totalCooldown - ability.totalCD);
        }
        if(sliderChargeSeparator != null)
        {
            sliderChargeSeparator.SetSeperatorByNumber(ability.maxCharge);
        }

        isInitalized = true;
    }

    private void Update()
    {
        if (!isInitalized) return;

        UpdateRegenSlider(abilityData.skillData.totalCooldown - abilityData.totalCD);
    }

    private void UpdateRegenSlider(float value)
    {
        regenSlider.SetCurrentValue(value);
        regenBar.fillAmount = regenSlider.slider.minValue / regenSlider.slider.maxValue;
    }

    private void UpdateCharge(int value)
    {
        if (chargeSlider != null)
        {
            chargeSlider.SetCurrentValue(value);
            chargeBar.fillAmount = chargeSlider.slider.minValue / chargeSlider.slider.maxValue; ;
        }
    }
}
