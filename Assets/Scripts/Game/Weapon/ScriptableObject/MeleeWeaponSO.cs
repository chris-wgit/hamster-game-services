using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName ="Melee Weapon", menuName ="GameData/Melee Weapon")]
public class MeleeWeaponSO : WeaponDataSO
{

    public override void WeaponAttack(Character owner, LocalTransform transform)
    {
        //base.WeaponAttack(owner, transform);
        //GameObject bulletObj = PoolManager.Spawn(weaponSkill.obje, transform.position, transform.rotation);
        //IWeaponOwner projectile = bulletObj.GetComponent<IWeaponOwner>();
        //projectile.SetOwner(owner);

    }
}
