using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;


public class NetworkUIEventRaiser : MonoBehaviour
{
    public RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
    public SendOptions sendOptions = new SendOptions { Reliability = true };

    private static NetworkUIEventRaiser _instance;

    public static NetworkUIEventRaiser Instance => _instance;

    private void Awake()
    {
        if (_instance != this && _instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(_instance.gameObject);
        }

    }


    public void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        UIHandler.StartBattleClicked += SendBattleButtonClickEvent;
    }

    public void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
        UIHandler.StartBattleClicked -= SendBattleButtonClickEvent;
    }

    public void SendEndTurnClickEvent()
    {

        if(!PhotonNetwork.OfflineMode)
            PhotonNetwork.RaiseEvent(NetworkOwnershipTransferManger.endTurnEvent, null, raiseEventOptions, sendOptions);
        //Debug.Log("sent end turn event");
    }

    public void SendBattleButtonClickEvent()
    {
        if (!PhotonNetwork.OfflineMode)
        {
            PhotonNetwork.RaiseEvent(NetworkOwnershipTransferManger.startBattleEvent, null, raiseEventOptions, sendOptions);
            UIHandler.StartBattleClicked -= SendBattleButtonClickEvent;
        }
    }

    private void OnEvent(EventData photonEvent)
    {
        byte recievedCode = photonEvent.Code;

        if (recievedCode == NetworkOwnershipTransferManger.endTurnEvent)
        {
            UIHandler.Instance.RasieEventEndTurnButtonOnClick();
            //Debug.Log("recieved end turn event");
        }

        if (recievedCode == NetworkOwnershipTransferManger.startBattleEvent)
            UIHandler.Instance.RaiseEventStartBattleButtonOnClick();
    }
}
