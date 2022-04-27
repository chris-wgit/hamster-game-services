using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandlerUI
{

    public static Action<string, string> OnShowPopUpEvent;

    public static Action<bool, string> OnShowLoadingEvent;
    public static Action<string> OnSetLoadingInforEvent;

    public static Action<bool> OnShowLoadingWithLoadProcessEvent;
    public static Action<string, float> OnSetLoadingProcessEvent;

    public static Action<CharacterDataSO> OnSwapCharacterEvent;
    public static Action OnChangeCharacterEvent;

    public static Action<WeaponDataSO> OnSwapWeaponEvent;
    public static Action OnEquipWeaponEvent;

    public static void EquipWeapon()
    {
        OnEquipWeaponEvent?.Invoke();
    }
    public static void ChangeCharacter()
    {
        OnChangeCharacterEvent?.Invoke();
    }
    public static void SwapCharacter(CharacterDataSO character)
    {
        OnSwapCharacterEvent?.Invoke(character);
    }

    public static void EquipWeapon(WeaponDataSO weapon)
    {
        OnSwapWeaponEvent?.Invoke(weapon);
    }
    public static void ActiveLoading(bool value, string loadingInfor =null)
    {
        OnShowLoadingEvent?.Invoke(value, loadingInfor);
    }
    public static void SetLoadingInfor(string loadingInfor)
    {
        OnSetLoadingInforEvent?.Invoke(loadingInfor);
    }

    public static void ActiveLoadingWithProcess(bool value)
    {
        OnShowLoadingWithLoadProcessEvent?.Invoke(value);
    }

    public static void ActivePopUp(string title, string infor)
    {
        OnShowPopUpEvent?.Invoke(title, infor);
    }

    public static void SetLoadingProcessEvent(string infor, float process)
    {
        OnSetLoadingProcessEvent?.Invoke(infor, process);
    }
}
