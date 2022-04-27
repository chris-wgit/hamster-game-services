using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class CharacterEquipWeapon : MonoBehaviour
{
    [BoxGroup("Handler")]
    public Transform handleR;
    [BoxGroup("Handler")]
    public Transform handleL;

    [BoxGroup("Setting")]
    public PlayerDataSO _PlayerData;
    [BoxGroup("Setting")]
    public bool isAutoInit;

    [BoxGroup("Weapon Data")]
    public WeaponLibrarySO weaponLibrary;
    [BoxGroup("Weapon Data")]
    public WeaponDataSO currentWeapon;

    public Action<WeaponDataSO> OnWeaponEquipped;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (isAutoInit)
        {
            //SetWeapon(_PlayerData.weaponID);
            SetWeapon(currentWeapon.id);
        }
    }

    public void SetWeapon(string weaponID)
    {
        currentWeapon = weaponLibrary.GetWeapon(weaponID);
        EquipWeapon();
    }

    public void SetWeapon(WeaponDataSO weaponData)
    {
        currentWeapon = weaponData;
        EquipWeapon();
    }

    [Button(ButtonSizes.Large)]
    private void EquipWeapon()
    {
        RemoveWeapon();
        currentWeapon.EquipWeapon(handleL, handleR, animator);
        OnWeaponEquipped?.Invoke(currentWeapon);
    }

    private void RemoveWeapon()
    {
        //RemoveChild(handleL);
        //RemoveChild(handleR);
    }

    private void RemoveChild(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }
}