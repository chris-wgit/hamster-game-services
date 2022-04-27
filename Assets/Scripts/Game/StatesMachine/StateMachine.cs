using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StateChangeEvent<T> where T : struct
{
    public GameObject Target;
    public StateMachine<T> TargetStateMachine;
    public T NewState;
    public T PreviousState;

    public StateChangeEvent(StateMachine<T> stateMachine)
    {
        Target = stateMachine.Target;
        TargetStateMachine = stateMachine;
        NewState = stateMachine.CurrentState;
        PreviousState = stateMachine.PreviousState;
    }

}
public interface IStateMachine
{
    bool TriggerEvents { get; set; }
}
public class StateMachine<T> : IStateMachine where T : struct
{
    public bool TriggerEvents { get; set; }
    //The name of target game object
    public GameObject Target;
    //Current character's movement state
    public T CurrentState { get; protected set; }
    //Previous character's movement state
    public T PreviousState { get; protected set; }

    public Action<T> OnStateChange;


    public StateMachine(GameObject target, bool triggerEvents)
    {
        this.Target = target;
        this.TriggerEvents = triggerEvents;
    }

    public virtual void ChangeState(T newState)
    {
        //If new state is current one, do nothing
        if (newState.Equals(CurrentState)) return;
        //Store previous movement state
        PreviousState = CurrentState;
        CurrentState = newState;

        OnStateChange?.Invoke(newState);

    }

    public virtual void RestorePreviousState()
    {
        CurrentState = PreviousState;
        OnStateChange?.Invoke(CurrentState);
    }

}

