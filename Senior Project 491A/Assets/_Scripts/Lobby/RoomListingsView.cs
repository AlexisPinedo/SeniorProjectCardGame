using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListingsView : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private RoomListing _roomListing;

    private List<RoomListing> _listings = new List<RoomListing>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            //Removed from rooms list.
            if (info.RemovedFromList)
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            }

            //Added to rooms list.
            else
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index == -1)
                {
                    RoomListing listing = Instantiate(_roomListing, _content);
                    if (listing != null)
                    {
                        listing.SetRoomInfo(info);
                        _listings.Add(listing);
                    }
                }
            }
        }
    }
}
