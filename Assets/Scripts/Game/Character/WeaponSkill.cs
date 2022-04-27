using Cysharp.Threading.Tasks;
using Photon.Pun;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WeaponSkill : CharacterAbility, ISkillAbility
{

    public GameObject attackPosition;

    private bool inAttack = false;

    private Vector3 attackDirection;

    public Ability ability;

    private float actionTime;

    private Coroutine action_routine = null;


    #region Initializing

    protected override void Awake()
    {
        base.Awake();
        ability = new Ability();
    }

    public void InitCharacterSkill()
    {
        ability  = GetAbility(_character.weaponData.weaponSkill);

        switch (_character.weaponData.shootPosition)
        {
            case ShootPos.None:
                break;
            case ShootPos.LeftHandler:
                SetUpShootPosition(_character.characterEquip.handleL.GetComponentInChildren<WeaponHandler>()).Forget();
                break;
            case ShootPos.RightHandler:
                SetUpShootPosition(_character.characterEquip.handleR.GetComponentInChildren<WeaponHandler>()).Forget();
                break;
        }

        if (photonView.IsMine)
        {
            SetupInputController();
            _character.hUDController.reloadUI.Initializing(ability);
        }
    }

    //Need to wait animation moved to attack position
    private async UniTaskVoid SetUpShootPosition(WeaponHandler weapon)
    {
        await UniTask.Delay(1000);
        if (weapon.spawnPos != null) attackPosition.transform.position = weapon.spawnPos.position;
    }

    private void SetupInputController()
    {
        _inputController.OnRightStickPerformed += HandleInput;

        isInputInitialized = true;
    }

    private Ability GetAbility(ScriptableSkill skill)
    {
        Ability data = new Ability();
        data.skillData = skill;
        data.maxCharge = skill.maxCharge;
        data.SetCharge(skill.initialCharge ? data.maxCharge : 0);
        data.currentCD = skill.initialCharge ? 0 : skill.cooldown;
        return data;
    }

    private void OnDisable()
    {
        if (isInputInitialized)
        {
            _inputController.OnRightStickPerformed -= HandleInput;
        }

    }

    #endregion

    #region Attack_Function

    public override void HandleInput(Vector2 input)
    {
        if (!ability.skillData.IsCharacterStateCanAction(_character)) return;
        //Check cooldown
        if (ability.currentCharge <=0) return;

        if (input == Vector2.zero)
        {
            StartAction(GetTargetDirection());
        }
        else
        {
            Vector3 destination = ability.skillData.DestinationFromInput(input, normalizedTransform);
            StartAction(destination);
        }

    }

    private void StartAction(Vector3 input)
    {
        attackDirection = input;

        inAttack = true;

        _character.MovementState.ChangeState(ability.skillData.characterState);

        ability.ProcessCooldown();

        TriggerAction(0.2f, () => ProcessWeaponAttack());

    }



    private void ProcessWeaponAttack()
    {
        EventHandlerFX.ShakeEvent(ability.skillData.shakeFeedbackPower);

        CallCharacterSkillRPC();
    }

    private void CallCharacterSkillRPC()
    {
        short[] data = NetworkData.NetworkTransform(attackPosition.transform);
        WeaponSkillAction(data);
        photonView.RPC("WeaponSkillRPC", RpcTarget.Others, data, PhotonNetwork.ServerTimestamp);

    }

    public void WeaponSkillAction(short[] inputData)
    {
        LocalTransform data = NetworkData.ConvertNetworkTransform(inputData);
        ability.skillData.Action(_character, data);
    }

    [PunRPC]
    public void WeaponSkillRPC(short[] inputData, int timestamp)
    {
        LocalTransform data = NetworkData.ConvertNetworkTransform(inputData);
        ability.skillData.Action(_character, data, timestamp);
    }
    private void OnFinishAction()
    {
        inAttack = false;
        actionTime = 0;
        _movementState.ChangeState(MovementStates.Idle);
    }
    #endregion

    protected override void Update()
    {
        if (!photonView.IsMine) return;
        if (!isInputInitialized) return;

        ProcessAbilityCooldown();

        if (inAttack)
        {
            actionTime += Time.deltaTime;
            if (actionTime >= ability.skillData.actionDuration) OnFinishAction();
        }

    }

    private void ProcessAbilityCooldown()
    {
        if (ability.currentCharge >= ability.maxCharge) return;
        if (ability.currentCD > 0) ability.currentCD -= Time.deltaTime;
        if (!(ability.currentCD <= 0)) return;
        ability.AddCharge();
    }

    protected void FixedUpdate()
    {
        if (inAttack)
        {
            _characterMovement.SetRotation(attackDirection, 6000f);
        }
    }


    public void TriggerAction(float duration, UnityAction callback = null)
    {
        action_routine = StartCoroutine(RunActionRoutine(duration, callback));
    }

    private IEnumerator RunActionRoutine(float action_duration, UnityAction callback = null)
    {
        yield return new WaitForSeconds(action_duration);

        if (callback != null)
            callback.Invoke();
    }
}

[Serializable]
public class Ability
{
    public ScriptableSkill skillData;
    public int currentCharge { get; private set; }
    public int maxCharge;
    public float currentCD;
    public Action<int> onCurrentChargeChanged;
    [ShowInInspector]
    public float totalCD { get { return skillData!=null?Mathf.Clamp((maxCharge - currentCharge - 1) * skillData.cooldown + currentCD,0,skillData.totalCooldown):0; } }
    public void ProcessCooldown()
    {
        currentCharge--;
        if(currentCD <= 0)
        {
            currentCD = skillData.cooldown;
        }
        onCurrentChargeChanged?.Invoke(currentCharge);
    }

    public void AddCharge()
    {
        currentCharge++;
        onCurrentChargeChanged?.Invoke(currentCharge);
        if (currentCharge < maxCharge) currentCD = skillData.cooldown;
    }

    public void SetCharge(int value)
    {
        currentCharge = value;
        onCurrentChargeChanged?.Invoke(currentCharge);
        if (currentCharge < maxCharge) currentCD = skillData.cooldown;
    }

}
