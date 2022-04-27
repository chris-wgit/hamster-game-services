using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Photon Event Channel")]
public class PhotonEventChannel : EventChannelBaseSO
{
	public UnityAction OnEventRaised;

	public void RaiseEvent()
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke();
	}
}
