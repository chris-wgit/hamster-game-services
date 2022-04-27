using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeapon : MonoBehaviour
{
    public WeaponDataSO weapon;

    public Image icon;
    public UITextBehaviour weaponName;

    public void InitData(WeaponDataSO data)
    {
        weapon = data;

        if (weapon.icon != null) icon.sprite = weapon.icon;
        else icon.enabled = false;

        weaponName.SetText(weapon.weaponName);
    }

    public void OnWeaponSelected()
    {
        EventHandlerUI.EquipWeapon(weapon);
    }
}
