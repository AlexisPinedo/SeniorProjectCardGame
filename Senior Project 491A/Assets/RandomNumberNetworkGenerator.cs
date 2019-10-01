using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RandomNumberNetworkGenerator : MonoBehaviourPun, IPunObservable
{
    public static System.Random randomNumber = new System.Random();

    private void Awake()
    {
        if(PhotonNetwork.IsMasterClient)
            randomNumber = new System.Random();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(randomNumber);
        }
        else
        {
            randomNumber = (System.Random) stream.ReceiveNext();
        }
    }
}
