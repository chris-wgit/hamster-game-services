using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    private Character _character;
    private Shield _shield;
    private PhotonView photonView;

    public event Action<int> OnCurrentHealthChanged;
    public event Action<int> OnMaxHealthChanged;
    public event Action<int> OnDamaged;

    public int currentHealth { get; private set; }

    public int maxHealth { get; private set; }

    public bool isDamageable { get; private set; }

    private void Awake()
    {
        _character = GetComponent<Character>();
        _shield = GetComponent<Shield>();
        photonView = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        _character.OnCharacterInitialized += Initializing;
        _character.OnCharacterReset += Initializing;
    }
    private void OnDisable()
    {
        _character.OnCharacterInitialized -= Initializing;
        _character.OnCharacterReset -= Initializing;
    }

    public void Initializing(CharacterType characterType)
    {
        ResetHealth((short)_character.data.health);
    }

    public void Initializing()
    {
        ResetHealth((short)_character.data.health);
    }

    public bool IsDamageable(Character attacker)
    {
        isDamageable = false;
        if (attacker == _character) isDamageable = false;
        if (attacker.Team != _character.Team)
        {
            isDamageable = true;
        }
        if (_character.ConditionState.CurrentState == CharacterConditions.Dead) isDamageable = false;

        return isDamageable;
    }

    public void ApplyDamage(int damage, Character attacker)
    {
        if (!photonView.IsMine) return;
        Debug.Log("Damaged");
        if (_character.ConditionState.CurrentState == CharacterConditions.Invulrnerable) return;
        if (_shield != null)
        {
            if (_shield.isActive)
            {
                _shield.DamageShield(damage);
                return;
            }
        }

        currentHealth -= (short)damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            Player killer = attacker.gameObject.GetComponent<PhotonView>().Owner;

            _character.MasterCharacterDead(killer);

        }
        else
        {
            SetHealth((short)currentHealth);
            if(_character.ConditionState.CurrentState == CharacterConditions.Normal
                &&_character.MovementState.CurrentState != MovementStates.Attacking)
            {
                StartCoroutine(CoDamagedState());
            }
        }

        if (_character.CanSendRPC())
        {
            photonView.RPC("SetHealth", RpcTarget.Others, (short)currentHealth);
        }
        OnDamaged?.Invoke(damage);

        EventHandlerFX.FlashEvent();
    }

    private IEnumerator CoDamagedState()
    {
        _character.ConditionState.ChangeState(CharacterConditions.Damaged);
        yield return new WaitForSeconds(0.5f);
        _character.ConditionState.ChangeState(CharacterConditions.Normal);
    }

    [Button(ButtonSizes.Large)]
    public void ApplyDamate(int damage)
    {
        if (!photonView.IsMine) return;
        if (_shield != null)
        {
            if (_shield.isActive)
            {
                _shield.DamageShield(damage);
                return;
            }
        }

        currentHealth -= (short)damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        else
        {
            SetHealth((short)currentHealth);

            if (_character.ConditionState.CurrentState == CharacterConditions.Normal
                && _character.MovementState.CurrentState != MovementStates.Attacking)
            {
                StartCoroutine(CoDamagedState());
            }
        }

        if (_character.CanSendRPC())
        {
            photonView.RPC("SetHealth", RpcTarget.Others, (short)currentHealth);
        }
        OnDamaged?.Invoke(damage);

        EventHandlerFX.FlashEvent();
    }

    //Add current health with value
    public void AddHealth(short value)
    {
        currentHealth += value;
        SetHealth((short)currentHealth);
        if (_character.CanSendRPC())
        {
            photonView.RPC("SetHealth", RpcTarget.Others, (short)currentHealth);
        }

    }

    [PunRPC]
    private void ResetHealth(short value)
    {
        maxHealth = value;
        currentHealth = value;
        OnCurrentHealthChanged?.Invoke(value);
        OnMaxHealthChanged?.Invoke(value);
    }

    [PunRPC]
    //Set health at value
    private void SetHealth(short value)
    {
        currentHealth = value;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        OnCurrentHealthChanged?.Invoke(currentHealth);
    }

}

