using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HealthTextFX : MonoBehaviour
{
    [SerializeField]
    private Health health;
    [SerializeField]
    private GameObject healthTextObject;

    private bool isOnDamaged;

    private int currentDamaged;

    private void Awake()
    {
        if (health == null) health = GetComponentInParent<Health>();
    }

    private void OnEnable()
    {
        health.OnDamaged += OnCharacterDamaged;
    }
    private void OnDisable()
    {
        health.OnDamaged -= OnCharacterDamaged;
    }

    private void OnCharacterDamaged(int damage)
    {
        if (isOnDamaged)
        {
            currentDamaged += damage;
        }
        else
        {
            currentDamaged = damage;
            StartCoroutine(CoWaitTotalDamaged());
        }
    }

    private IEnumerator CoWaitTotalDamaged()
    {
        isOnDamaged = true;
        yield return new WaitForSeconds(0.1f);
        SpawnHealthText(currentDamaged);
        isOnDamaged = false;
    }

    private void SpawnHealthText(int damage)
    {
        GameObject textObject = PoolManager.Spawn(healthTextObject, transform.position, transform.rotation);
        UITextFloating textFloating = textObject.GetComponent<UITextFloating>();
        textFloating.SetText(damage);       
    }

  
}
