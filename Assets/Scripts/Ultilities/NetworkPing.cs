using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPing : MonoBehaviour
{
    public UITextBehaviour pingText;

    private void Update()
    {
        GetPing();
    }
    public void GetPing()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            float ping = PhotonNetwork.GetPing();
            pingText.SetText("Ping: " + ping);
        }
        else
        {
            pingText.SetText("Ping: 0");
        }
    }
}
