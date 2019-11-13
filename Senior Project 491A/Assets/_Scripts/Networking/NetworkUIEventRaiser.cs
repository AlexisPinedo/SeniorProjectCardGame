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
        UIHandler.EndTurnClicked += SendEndTurnClickEvent;
    }

    public void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
        UIHandler.StartBattleClicked -= SendBattleButtonClickEvent;
        UIHandler.EndTurnClicked -= SendEndTurnClickEvent;
    }

    public void SendEndTurnClickEvent()
    {
        PhotonNetwork.RaiseEvent(NetworkOwnershipTransferManger.endTurnEvent, null, raiseEventOptions, sendOptions);
    }

    public void SendBattleButtonClickEvent()
    {
        PhotonNetwork.RaiseEvent(NetworkOwnershipTransferManger.startBattleEvent, null, raiseEventOptions, sendOptions);
    }

    private void OnEvent(EventData photonEvent)
    {
        byte recievedCode = photonEvent.Code;

        if (recievedCode == NetworkOwnershipTransferManger.endTurnEvent)
        {
            Debug.Log($"End Event received {recievedCode}");
            UIHandler.Instance.EndTurnButtonOnClick();
        }

        if (recievedCode == NetworkOwnershipTransferManger.startBattleEvent)
        {
            Debug.Log($"Start Event received {recievedCode}");
            UIHandler.Instance.StartBattleButtonOnClick();
        }
    }
}
