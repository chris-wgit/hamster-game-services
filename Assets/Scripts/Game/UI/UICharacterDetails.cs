using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterDetails : MonoBehaviour
{

    public PlayerDataSO _PlayerData;

    [Header("Broadcast Event")]
    public VoidEventChannelSO _OnSelectWeaponChanged;

    public void OnSelectCharacterClicked()
    {
        //_PlayerData.SetCharacter(GameSetting.PreviewCharacterID);
        // _OnSelectCharacterChanged.RaiseEvent(GameSetting.CharacterID);
    }
}

