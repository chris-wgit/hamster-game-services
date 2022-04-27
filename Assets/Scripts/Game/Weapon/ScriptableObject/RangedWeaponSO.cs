using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName ="Ranged Weapon", menuName ="GameData/Ranged Weapon")]
public class RangedWeaponSO : WeaponDataSO
{

    public override void WeaponAttack(Character owner, LocalTransform transform)
    {
        //base.WeaponAttack(owner, transform);
        //GameObject bulletObj = PoolManager.Spawn(bulletPrefab, transform.position, transform.rotation);
        //IWeaponOwner projectile = bulletObj.GetComponent<IWeaponOwner>();
        //projectile.SetOwner(owner);

    }
}
