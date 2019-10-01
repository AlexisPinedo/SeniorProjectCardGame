using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class ShopNetworkRelay : MonoBehaviourPun, IPunObservable
{
    private void Awake()
    {
        PhotonNetwork.SerializationRate = 20;
        PhotonNetwork.SendRate = 20;
    }

    private void Start()
    {
//        if (PhotonNetwork.IsMasterClient)
//        {
//            RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
//            raiseEventOptions.CachingOption = EventCaching.DoNotCache;
//            raiseEventOptions.Receivers = ReceiverGroup.All;
//            SendOptions sendOptions = new SendOptions();
//
//            object content = shopDeck;
//            //.GetHashCode();
//
//            PhotonNetwork.RaiseEvent(0, content, raiseEventOptions, sendOptions);
//
//
//            Debug.Log("Raise event sent..");
//        }
    }
    
    public void OnEvent(EventData photonEvent)
    {
        Debug.Log("Event recieved...");
        photonEvent.Code = 0;

        //myObject = photonEvent.CustomData;
        //shopDeck = (ShopDeck) myObject;
    }

    public void SerializeState (PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("Setup stream going to send data");
        if (stream.IsWriting)
        {
            stream.SendNext(ShopContainer.Instance.shopDeck);
        }
        else
        {
            ShopContainer.Instance.shopDeck = (ShopDeck)stream.ReceiveNext();
        }
    }
    

    public void OnPhotonSerializeView( PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("... i think its syncing?");
        SerializeState(stream, info);
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
