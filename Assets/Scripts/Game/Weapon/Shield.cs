using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : CharacterAbility
{

    public bool isActive = false;

    public int shieldValue = 1000;

    [HideInInspector]
    public int currentShieldValue;

    public float shieldTime = 5f;

    public GameObject shieldFX;

    public override void Initialization()
    {
        base.Initialization();

        shieldFX.SetActive(false);
        currentShieldValue = 0;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (_conditionState.CurrentState != CharacterConditions.Normal) return;

        ActiveShield();
        if (_character.CanSendRPC()) photonView.RPC("ActiveShield", RpcTarget.Others);

    }


    [PunRPC]
    public virtual void ActiveShield()
    {
        currentShieldValue = shieldValue;
        shieldFX.SetActive(true);
        StartCoroutine(ActiveCoroutine());
    }

    private IEnumerator ActiveCoroutine()
    {
        isActive = true;
        float activatedTime = Time.time;
        while (Time.time - activatedTime < shieldTime)
        {
            yield return null;
        }
        DeactiveShield();
    }

    public void DamageShield(int value)
    {
        currentShieldValue -= value;
        if (currentShieldValue <= 0)
        {
            currentShieldValue = 0;
            DeactiveShield();
        }
    }

    private void DeactiveShield()
    {
        isActive = false;
        shieldFX.SetActive(false);
    }
}

