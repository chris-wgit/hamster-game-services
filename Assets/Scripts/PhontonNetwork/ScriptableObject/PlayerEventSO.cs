using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

namespace DFT.EventManager
{
    [CreateAssetMenu(fileName = "Player Event",menuName = "PhotonEvents/Player Event", order =1)]
    public class PlayerEventSO : ScriptableObject
    {
        public UnityAction<Player> OnEventRaised;
        public void RaiseEvent(Player sender)
        {
            OnEventRaised.Invoke(sender);
        }
    }
}