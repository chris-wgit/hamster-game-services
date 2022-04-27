using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform content;

    [SerializeField]
    private RoomListingsView roomListing;

    [SerializeField]
    private PlayerListingsView playerListing;

    [SerializeField]
    private Transform playerContent;

    [SerializeField]
    private Transform spectatorContent;

    public List<RoomListingsView> listings = new List<RoomListingsView>();

    public override void OnEnable()
    {
        base.OnEnable();
    }
    public override void OnDisable()
    {
        base.OnDisable();
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        
        foreach(RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(listings[index].gameObject);
                    listings.RemoveAt(index);
                }
            }
            else
            {
                RoomListingsView listing = Instantiate(roomListing, content);
                if(listing != null)
                {
                    listing.SetRoomInfo(info);
                    listings.Add(listing);
                }
            }

        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerListingsView listview;

        if (newPlayer.NickName != "Spectator")
        {
             listview = Instantiate(playerListing, playerContent);
        }
        else
        {
             listview = Instantiate(playerListing, spectatorContent);
        }
        listview.SetPlayerInfo(newPlayer);
    }
}
