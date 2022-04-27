using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSlotUI : MonoBehaviour
{
    public CharacterDataSO _CharacterData;

    public TextMeshProUGUI characterName;

    public Image characterIcon;

    public void SetCharacterUI()
    {
        characterName.text = _CharacterData.characterName;
        if (_CharacterData.Icon != null)
            characterIcon.sprite = _CharacterData.Icon;
        else
            characterIcon.enabled = false;
    }

    public void ViewCharacter()
    {
        EventHandlerUI.SwapCharacter(_CharacterData);
    }

}

