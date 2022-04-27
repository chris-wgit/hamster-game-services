using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "PhotonEvents/Player Action Event", order = 2)]
public class PlayerActionEventChannelSO : ScriptableObject
{
    public UnityAction<Player> OnEventRaised;
    public void RaiseEvent(Player killer)
    {
        OnEventRaised.Invoke(killer);
    }
}

