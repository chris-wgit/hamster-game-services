using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Vector3 Event Channel")]
public class Vector3EventChanelSO : ScriptableObject
{
    public UnityAction<Vector3> OnEventRaised;
    public void RaiseEvent(Vector3 value)
    {
        OnEventRaised.Invoke(value);
    }
}

