﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonNetworkManager : MonoBehaviourPunCallbacks
{

    [SerializeField]
    GameObject player1Prefab, player2Prefab, shopPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Online Gameplay
        if (PhotonNetwork.IsConnected)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(player1Prefab.name, new Vector3(0, 0, 0), Quaternion.identity);
                PhotonNetwork.Instantiate(shopPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else
            {
                PhotonNetwork.Instantiate(player2Prefab.name, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
        //Offline Gameplay
        else
        {

        }
    }

}
