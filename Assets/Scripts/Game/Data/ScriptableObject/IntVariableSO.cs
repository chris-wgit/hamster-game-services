using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DFT.DataManager
{
    [CreateAssetMenu(fileName = "IntVariable", menuName = "Data/IntVariable")]
    public class IntVariableSO : ScriptableObject
    {
#if UNITY_EDITOR
        public string Description;
#endif
        public string Name;
        public int Value;

        public virtual void SetValue(int value)
        {
            Value = value;
        }
        public virtual void SetValue(IntVariableSO value)
        {
            Value = value.Value;
        }

        public virtual void ApplyChange(int value)
        {
            Value += value;
        }

        public virtual void ApplyChange(IntVariableSO value)
        {
            Value += value.Value;
        }
    }
}

