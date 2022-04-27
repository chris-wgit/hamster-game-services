using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonConnectionWarning : MonoBehaviour
{
    public GameObject warningPanel;
    public float warningValue= 80;
    private void Update()
    {
        if (!PhotonNetwork.IsConnected) return;

        float ping = PhotonNetwork.GetPing();
        if(ping > warningValue)
        {
            ToggleWarning(true);
        }
        else
        {
            ToggleWarning(false);
        }
    }

    void ToggleWarning(bool value)
    {
        warningPanel.SetActive(value);
    }
}
