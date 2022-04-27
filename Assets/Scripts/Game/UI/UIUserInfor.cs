using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUserInfor : MonoBehaviour
{
    public PlayerDataSO playerData;



    public void EditUsername()
    {
        EventHandlerPlayfab.UpdateUserNameAction(() => playerData.OnUserDataUpdated?.Invoke());
    }
}
