using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class TransferOwnersihpManager : MonoBehaviour
{
    private static List<Itransferable> _transferables = new List<Itransferable>();
    private static List<MonoBehaviourPunCallbacks> _transferablesMono = new List<MonoBehaviourPunCallbacks>();
    private byte currentCardIdenrifier = (byte)'E';
    public static void AddTransferableObjectToList(Itransferable objectToAdd)
    {
        _transferables.Add(objectToAdd);
        
        objectToAdd.HandleTransfer();
    }
    
    public static void AddTransferableObjectToList(MonoBehaviourPunCallbacks objectToAdd)
    {
        _transferablesMono.Add(objectToAdd);
        
    }

    void HandleIdAssignment()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < _transferables.Count; i++)
            {
                if (PhotonNetwork.AllocateViewID(_transferablesMono))
                {
                    object[] data = {photonView.ViewID};

                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                    {
                        Receivers = ReceiverGroup.Others,
                        CachingOption = EventCaching.AddToRoomCache
                    };

                    SendOptions sendOptions = new SendOptions
                    {
                        Reliability = true
                    };

//                Debug.Log("MinionCard assigned ViewID: " + photonView.ViewID);
                    PhotonNetwork.RaiseEvent(currentCardIdenrifier, data, raiseEventOptions, sendOptions);
                }

                PassID();
            }
        }
    }

    public void OnEnable()
        {
            PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        }
        
        public void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
        }
        
            public void OnEvent(EventData photonEvent)
    {

        byte recievedCode = photonEvent.Code;
        if (recievedCode == currentCardIdenrifier)
        {
            object[] data = (object[])photonEvent.CustomData;
            int recievedPhotonID = (int)data[0];

            if (!TurnManager.photonViewIDs.Contains(recievedPhotonID))
            {
                photonView.ViewID = recievedPhotonID;
                TurnManager.photonViewIDs.Add(recievedPhotonID);

                Debug.Log("MinionCard RPC to assign PhotonView ID: " + photonView.ViewID);
                PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
            }
        }
        else
        {
            //Debug.Log("Event code not found");
        }
    }

    private void PassID()
    {
        for (int i = 0; i < _transferables.Count; i++)
        {
            //Run logic to pass id
        }
    }
    
    void SwapOwnership()
    {
        foreach (Itransferable transferable in _transferables)
        {
            
        }
    }
}
