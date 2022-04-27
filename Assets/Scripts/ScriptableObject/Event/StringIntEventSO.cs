using UnityEngine;
using UnityEngine.Events;

namespace DFT.EventManager
{
    [CreateAssetMenu(menuName = "Events/String Int Event Channel")]
    public class StringIntEventSO : ScriptableObject
    {
        public UnityAction<string, int> OnEventRaised;
        public void RaiseEvent(string name, int value)
        {
            OnEventRaised.Invoke(name,value);
        }
    }
}

