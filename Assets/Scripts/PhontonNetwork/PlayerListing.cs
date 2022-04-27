using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

namespace DesertFoxTeam
{
    public class PlayerListing : MonoBehaviour
    {
        public Player _player { get; private set; }

        public TextMeshProUGUI _characterName;

        public void SetPlayerInfor(Player player)
        {
            _player = player;
        }

        public void RemovePlayer()
        {
            _player = null;
            _characterName.text = " ";
        }
    }
}

