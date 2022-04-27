using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerListingsView : MonoBehaviour
{
    [SerializeField]
    private Text text;

    private Player player;

    
    private void Start()
    {
        
    }

    public void SetPlayerInfo(Player _player)
    {
        player = _player;

        text.text = player.NickName;
    }

   
}
