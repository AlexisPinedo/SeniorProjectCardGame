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
    
    public void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        UIHandler.StartBattleClicked += HandleStartBattleButtonClick;
        UIHandler.EndTurnClicked += HandleEndTurnButtonClick;
    }

    public void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
        UIHandler.StartBattleClicked -= HandleStartBattleButtonClick;
        UIHandler.EndTurnClicked -= HandleEndTurnButtonClick;
    }

    private void HandleEndTurnButtonClick()
    {
        PhotonNetwork.RaiseEvent(NetworkOwnershipTransferManger.endTurnEvent, null, raiseEventOptions, sendOptions);
    }

    private void HandleStartBattleButtonClick()
    {
        PhotonNetwork.RaiseEvent(NetworkOwnershipTransferManger.startBattleEvent, null, raiseEventOptions, sendOptions);
    }
    
    private void OnEvent(EventData photonEvent)
    {
        byte recievedCode = photonEvent.Code;
        
        //After removing from UIHandler the code now executes the events not allowing either player to react
        //if (recievedCode == NetworkOwnershipTransferManger.endTurnEvent)
            //UIHandler.Instance.EndTurnButtonOnClick();
        //if (recievedCode == NetworkOwnershipTransferManger.startBattleEvent)
            //UIHandler.Instance.StartBattleButtonOnClick();
    }
}
