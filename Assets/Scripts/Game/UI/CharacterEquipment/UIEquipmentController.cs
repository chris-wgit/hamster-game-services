using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquipmentController : MonoBehaviour
{
    public PlayerDataSO playerData;
    public CharacterShowcase showcase;
    private CharacterDataSO currentCharacter;
    private WeaponDataSO currentWeapon;

    public UnLoadSceneEventSO UnloadEquipmentScene;

    private void OnEnable()
    {
        EventHandlerUI.OnSwapCharacterEvent += SwapCharacter;
        EventHandlerUI.OnSwapWeaponEvent += EquipWeapon;
    }
    private void OnDisable()
    {
        EventHandlerUI.OnSwapCharacterEvent -= SwapCharacter;
        EventHandlerUI.OnSwapWeaponEvent -= EquipWeapon;
    }


    private void SwapCharacter(CharacterDataSO character)
    {
        currentCharacter = character;
    }

    private void EquipWeapon(WeaponDataSO weapon)
    {
        currentWeapon = weapon;
        CharacterEquipWeapon equip = showcase.currentCharacterObject.GetComponent<CharacterEquipWeapon>();
        equip.SetWeapon(weapon);
    }

    public void SaveCharacterWeaponRequest()
    {
        if(currentCharacter || currentWeapon != null)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            if (currentWeapon != null) data.Add(PrefsKeys.CurrentWeapon, currentWeapon.id);
            if (currentCharacter != null) data.Add(PrefsKeys.CurrentCharacter, currentCharacter.id.ToString());

            PlayfabData.UpdateUserData(data, OnSaveCharacterDataSuccess);
        }
        else
        {
            UnloadEquipmentScene.RaiseEvent();
        }

    }

    private void OnSaveCharacterDataSuccess()
    {
        EventHandlerUI.ChangeCharacter();
        EventHandlerUI.EquipWeapon();
        UnloadEquipmentScene.RaiseEvent();
    }

}
