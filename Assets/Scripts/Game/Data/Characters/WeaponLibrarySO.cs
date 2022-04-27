using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Weapon Library", menuName = "GameData/Weapon Library")]
public class WeaponLibrarySO : ScriptableObject
{
    [ShowInInspector]
    public Dictionary<string, WeaponDataSO> WeaponList;

    private void OnEnable()
    {
        Initialization();
    }
    private void Initialization()
    {
        WeaponList = new Dictionary<string, WeaponDataSO>();
        var weapons = Resources.LoadAll("WeaponSO", typeof(WeaponDataSO));

        foreach (WeaponDataSO item in weapons)
        {
            WeaponList.Add(item.id, item);
        }
    }


    public WeaponDataSO GetWeapon(string weaponID)
    {
        WeaponDataSO weapon;
        WeaponList.TryGetValue(weaponID, out weapon);
        if (weapon != null) return weapon;
        return WeaponList["deagle"];
    }
}