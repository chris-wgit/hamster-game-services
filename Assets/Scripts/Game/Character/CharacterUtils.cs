using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CharacterUtils
{
    private static GameStatisticSO gamestatistics;
    public static GameObject GetClosestEnemy(Character caster)
    {
        if(gamestatistics == null)
        {
            gamestatistics = Resources.Load<GameStatisticSO>("GameData");
        }

        float distanceToClosestEnemy = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject currentEnemy in gamestatistics.cachedTargetPlayer)
        {
            if (currentEnemy != null)
            {
                float distanceToEnemy = (currentEnemy.transform.position - caster.gameObject.transform.position).sqrMagnitude;
                if (distanceToEnemy < distanceToClosestEnemy)
                {
                    distanceToClosestEnemy = distanceToEnemy;
                    closestEnemy = currentEnemy;
                }
            }
        }
        return closestEnemy;

    }

    public static Vector2 GetTargetDirection(Character caster)
    {
        Vector2 targetDirection = new Vector2(caster.gameObject.transform.forward.x, caster.gameObject.transform.forward.z);
        //Get nearest target
        GameObject closestEnemy = GetClosestEnemy(caster);

        if (closestEnemy != null)
        {
            Vector3 direction = closestEnemy.transform.position - caster.gameObject.transform.position;
            targetDirection = new Vector2(direction.x, direction.z);
        }
        return targetDirection;
    }
}
