using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoHealing : CharacterAbility
{
    public short healingAmount = 500;
    private float lastDamagesTime = -1f;
    [SerializeField]
    private float regenDelay = 5f;
    public float timeBetweenRegen = 2;

    [SerializeField]
    private float lastRegen = 0f;

    public GameObject healingFX;
    [SerializeField]
    private bool isHealing = false;

    public override void Initialization()
    {
        base.Initialization();

        _health.OnDamaged += ResetCount;
    }

    private void OnDisable()
    {
        _health.OnDamaged -= ResetCount;
    }

    private void ResetCount(int damage)
    {
        lastDamagesTime = Time.time;
        isHealing = false;
        healingFX.SetActive(false);
    }

    protected override void Update()
    {
        if (!isInputInitialized) return;
        if (_conditionState.CurrentState != CharacterConditions.Normal) return;

        if (_health.currentHealth < _health.maxHealth)
        {
            float timeCounted = Time.time - lastDamagesTime;
            if (timeCounted > regenDelay)
            {
                isHealing = true;
            }
        }
        else
        {
            isHealing = false;
            healingFX.SetActive(false);
        }

        if (isHealing)
        {
            lastRegen -= Time.deltaTime;
            if (lastRegen <= 0f)
            {
                _health.AddHealth(healingAmount);
                lastRegen = timeBetweenRegen;
                healingFX.SetActive(true);
            }
        }


    }


}

