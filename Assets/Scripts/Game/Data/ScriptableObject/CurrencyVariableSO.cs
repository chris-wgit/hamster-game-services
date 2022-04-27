using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DFT.DataManager
{
    [CreateAssetMenu(fileName = "CurrencyVariable", menuName = "UserData/CurrencyVariable")]
    public class CurrencyVariableSO : IntVariableSO
    {
        [Header("Updated Value Event")]
        public VoidEventChannelSO OnUpdatedCurrencyVariable;

        public override void SetValue(int value)
        {
            base.SetValue(value);
            if(OnUpdatedCurrencyVariable!= null)
            {
                OnUpdatedCurrencyVariable.RaiseEvent();
            }
        }
    }
}

