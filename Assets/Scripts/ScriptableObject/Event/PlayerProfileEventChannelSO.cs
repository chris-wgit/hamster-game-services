using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have no arguments (Example: Exit game event)
/// </summary>

[CreateAssetMenu(menuName = "Events/Playfab /Player Profile Event Channel")]
public class PlayerProfileEventChannelSO : EventChannelBaseSO
{
	public UnityAction<GetPlayerProfileResult> OnEventRaised;

	public void RaiseEvent(GetPlayerProfileResult result)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(result);
	}
}


