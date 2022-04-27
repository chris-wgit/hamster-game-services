using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AbilityCooldown: CooldownTimer
{
    [ShowInInspector]
    public int MaxSkillCharge { get; private set; }
    [ShowInInspector]
    public int CurrentSkillCharge { get; private set; }

    public UnityAction<int> OnSkillChargeChanged;
    [ShowInInspector]
    public float TotalStackTime { get { return TotalTime * MaxSkillCharge; } }
    [ShowInInspector]
    public float TotalElapsedTime { get { return (TotalStackTime - TimeStackRemaining)>TotalStackTime?TotalStackTime:(TotalStackTime - TimeStackRemaining); } }
    [ShowInInspector]
    public float TimeStackRemaining { get { return TotalStackTime - (CurrentSkillCharge*TotalTime + TimeElapsed); } }

    public AbilityCooldown(float time,int maxCharge = 1,int currentCharge = 1, bool recurring = false)
    {
        TotalTime = time;
        IsRecurring = recurring;
        TimeRemaining = TotalTime;

        MaxSkillCharge = maxCharge;
        CurrentSkillCharge = currentCharge;

    }

    public void SetCurrentSkillStack(int value)
    {
        CurrentSkillCharge = value;
    }

    public override void StartCooldown()
    {
        base.StartCooldown();
    }

    public override void Update()
    {
        base.Update();
        if (!IsActive)
        {
            if (CurrentSkillCharge < MaxSkillCharge) StartCooldown();
        }
    }

    public override void Invoke()
    {
        base.Invoke();

        CurrentSkillCharge++;
        OnSkillChargeChanged?.Invoke(CurrentSkillCharge);
        if (CurrentSkillCharge < MaxSkillCharge && !IsActive)
        {
            StartCooldown();
        }
    }

    public void ProcessAbilityCooldown()
    {
        if (IsSkillAvailable())
        {
            CurrentSkillCharge--;
            OnSkillChargeChanged?.Invoke(CurrentSkillCharge);
        }

        if(!IsActive) StartCooldown();
    }

    public bool IsSkillAvailable()
    {
       return CurrentSkillCharge > 0 ? true : false;
    }
}
