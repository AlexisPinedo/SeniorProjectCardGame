using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RandomNumberNetworkGenerator : MonoBehaviourPunCallbacks, IPunObservable
{
    public System.Random randomNumber = new System.Random();

    private const byte DECK_RANDOM_EVENT = 0;

    private static RandomNumberNetworkGenerator _instance;

    public static RandomNumberNetworkGenerator Instance { get { return _instance; } }

    private void Awake()
    {
        if(_instance != this && _instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("ReceiveRandomDeckValueFromMaster", RpcTarget.AllBufferedViaServer, 18);
            Debug.Log("sent RPC!");
        }
    }

    [PunRPC]
    private void ReceiveRandomDeckValueFromMaster(System.Random randomValue)
    {
        Debug.Log("recieved value..");
        randomNumber = randomValue;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(randomNumber);
        }
        else
        {
            randomNumber = (System.Random)stream.ReceiveNext();
        }
        //((IPunObservable)Instance).OnPhotonSerializeView(stream, info);
    }

    //public void OnEnable()
    //{
    //    PhotonNetwork.AddCallbackTarget(this);
    //}

    //public void OnDisable()
    //{
    //    PhotonNetwork.RemoveCallbackTarget(this);
    //}

    //public void OnEvent(EventData photonEvent)
    //{
    //    Debug.Log("Event recieved...");
    //    if (photonEvent.Code == DECK_RANDOM_EVENT)
    //    {
    //        object[] data = (object[])photonEvent.CustomData;
    //        randomNumber = (System.Random)data[0];
    //        Debug.Log("Event handeled...");
    //    }
    //}

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(randomNumber);
    //    }
    //    else
    //    {
    //        randomNumber = (System.Random)stream.ReceiveNext();
    //    }
    //}
}
