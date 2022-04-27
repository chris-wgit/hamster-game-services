using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSkill : CharacterAbility, ISkillAbility
{
    public Transform attackPosition;

    public Ability ability;

    private Coroutine action_routine = null;

    #region Initializing

    protected override void Awake()
    {
        base.Awake();
        ability = new Ability();
    }

    public void InitCharacterSkill()
    {
        ability = GetAbility(_character.data.additionSkill);

        if (photonView.IsMine)
        {
            SetupInputController();
            UIControllerManager.instance.skillButton2.Initialization(ability);
        }
    }

    private void SetupInputController()
    {
        _inputController.OnAction3 += HandleInput;

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
            _inputController.OnAction3 -= HandleInput;
        }

    }

    #endregion

    #region Attack_Function

    public override void HandleInput()
    {
        if (!ability.skillData.IsCharacterStateCanAction(_character)) return;
        //Check cooldown
        if (ability.currentCharge <= 0) return;

        StartAction();
    }

    private void StartAction()
    {
        _character.MovementState.ChangeState(ability.skillData.characterState);

        ability.ProcessCooldown();

        ability.skillData.Action(_character);

        TriggerAction(ability.skillData.actionDuration, () => OnFinishAction());

    }


    private void OnFinishAction()
    {
        ability.skillData.EndAction();
    }
    #endregion

    protected override void Update()
    {
        if (!photonView.IsMine) return;
        if (!isInputInitialized) return;

        ProcessAbilityCooldown();

    }

    private void ProcessAbilityCooldown()
    {
        if (ability.currentCharge >= ability.maxCharge) return;
        if (ability.currentCD > 0) ability.currentCD -= Time.deltaTime;
        if (!(ability.currentCD <= 0)) return;
        ability.AddCharge();
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
