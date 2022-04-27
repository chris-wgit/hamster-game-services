using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This class is used for Events that have one int argument.
/// Example: An Achievement unlock event, where the int is the Achievement ID.
/// </summary>

[CreateAssetMenu(menuName = "Events/Short Event Channel")]
public class ShortEventChannelSO : EventChannelBaseSO
{
	public UnityAction<short> OnEventRaised;
	public void RaiseEvent(short value)
	{
		OnEventRaised.Invoke(value);
	}
}
