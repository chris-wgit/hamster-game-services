using UnityEngine;
using UnityEngine.Events;

public class CooldownTimer
{
    public float TimeRemaining { get; protected set; }
    public float TotalTime { get; protected set; }
    public bool IsRecurring { get; protected set; }
    public bool IsActive { get; protected set; }
    public int TimesCounted { get; protected set; }

    public float TimeElapsed => TotalTime - TimeRemaining;
    public float PercentElapsed => TimeElapsed / TotalTime;
    public bool IsCompleted => TimeRemaining <= 0;

    public event UnityAction OnTimerCompleteEvent;

    public event UnityAction<float> OnTimeRemainingChanged;

    //public CooldownTimer(float time, bool recurring = false)
    //{
    //    TotalTime = time;
    //    IsRecurring = recurring;
    //    TimeRemaining = TotalTime;
    //}

    public virtual void Initialization(float time, bool recurring = false)
    {
        TotalTime = time;
        IsRecurring = recurring;
        TimeRemaining = TotalTime;
    }

    public virtual void StartCooldown()
    {
        if (IsActive) { TimesCounted++; }
        TimeRemaining = TotalTime;
        IsActive = true;
        //if (TimeRemaining <= 0)
        //{
        //    TimerCompleteEvent?.Invoke();
        //}
    }

    public virtual void StartCooldown(UnityAction onComplete)
    {
        OnTimerCompleteEvent = onComplete;
        if (IsActive) { TimesCounted++; }
        TimeRemaining = TotalTime;
        IsActive = true;
    }

    public virtual void Start(float time)
    {
        TotalTime = time;
        StartCooldown();
    }

    public virtual void Update()
    {
        if (TimeRemaining > 0 && IsActive)
        {
            TimeRemaining -= Time.deltaTime;
            OnTimeRemainingChanged?.Invoke(TimeRemaining);
            if (TimeRemaining <= 0)
            {
                if (IsRecurring)
                {
                    TimeRemaining = TotalTime;
                }
                else
                {
                    IsActive = false;
                }
                Invoke();
                TimesCounted++;
            }
        }
    }

    public virtual void Invoke()
    {
        OnTimerCompleteEvent?.Invoke();
    }

    public virtual void Pause()
    {
        IsActive = false;
    }

    public virtual void Restart()
    {
        TimeRemaining = TotalTime;
    }

    public virtual void AddTime(float time)
    {
        TimeRemaining += time;
        TotalTime += time;
    }
}