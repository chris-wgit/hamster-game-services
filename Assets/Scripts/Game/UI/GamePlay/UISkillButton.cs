using System;
using UnityEngine;
using UnityEngine.UI;

public class UISkillButton : MonoBehaviour
{

    public GameObject abilityActivateButton;

    public SkillSliderBehaviour slider;

    public Image skillIcon_Deactive;
    public Image skillIcon_Active;

    private Ability abilityData;
    public void Initialization(Ability ability)
    {
        if(ability != null)
        {
            abilityData = ability;
            if (ability.skillData.icon != null) skillIcon_Deactive.sprite = ability.skillData.icon;
            if (ability.skillData.icon != null) skillIcon_Active.sprite = ability.skillData.icon;

            slider.Initializing(ability);

            ToggleAbilityActivateButton(ability.currentCharge > 0 ? true : false);

            abilityData.onCurrentChargeChanged += SetButton;

        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void SetButton(int currentCharge)
    {
        ToggleAbilityActivateButton(currentCharge > 0 ? true : false);
    }

    public void ToggleAbilityActivateButton(bool newState)
    {
        abilityActivateButton.SetActive(newState);
    }

}

