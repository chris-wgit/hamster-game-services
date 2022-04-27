using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DFT.DataManager
{
    [CreateAssetMenu(fileName = "StringVariable", menuName = "Data/StringVariable")]
    public class StringVariableSO : ScriptableObject
    {

        public string Value;

        public virtual void SetValue(string value)
        {
            Value = value;
        }
        public virtual void SetValue(StringVariableSO value)
        {
            Value = value.Value;
        }
    }
}

