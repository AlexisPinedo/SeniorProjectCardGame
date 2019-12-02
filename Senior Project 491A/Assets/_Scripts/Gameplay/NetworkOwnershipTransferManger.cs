using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkOwnershipTransferManger : MonoBehaviourPunCallbacks
{
    public static Photon.Realtime.Player photonPlayer1, photonPlayer2, currentPhotonPlayer, pendingPhotonPlayer;

    public static RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };

    public static SendOptions sendOptions = new SendOptions { Reliability = true };

    [SerializeField]
    private ShopContainer shopCards;

    [SerializeField]
    private NotificationWindowEvent notficationWindow;

    [SerializeField]
    private FieldContainer minionCards;

    [SerializeField]
    private BossCardDisplay bossCard;
    
    public static byte endTurnEvent = (byte)'0';
    public static byte startBattleEvent = (byte)'1';


    private void Awake()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Photon is online...");

            //assign player 1 and player 2 for referencee
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (player.IsMasterClient)
                    photonPlayer1 = currentPhotonPlayer = player;
                else
                    photonPlayer2 = pendingPhotonPlayer = player;
            }

            Debug.Log("Photon Player 1: " + photonPlayer1.NickName);
            Debug.Log("Photon Player 2: " + photonPlayer2.NickName);

            notficationWindow.photonView.TransferOwnership(currentPhotonPlayer);
        }
        else
        {
            // Enabling offline mode allows all photon code to be ignored
            Debug.Log("Photon is offline...");
            PhotonNetwork.OfflineMode = true;
            currentPhotonPlayer = photonPlayer1 = photonPlayer2 = PhotonNetwork.LocalPlayer;
        }
    }

    private void OnEnable()
    {
        if (!PhotonNetwork.OfflineMode)
            TurnPlayerManager.PlayerSwitched += TransferObjects;
    }

    private void OnDisable()
    {
        if (!PhotonNetwork.OfflineMode)
            TurnPlayerManager.PlayerSwitched -= TransferObjects;
    }
    
    public void TransferObjects()
    {
        bossCard.photonView.TransferOwnership(currentPhotonPlayer);

        notficationWindow.photonView.TransferOwnership(currentPhotonPlayer);
        Debug.Log("notfication window transfered");

        PhotonView[] minionTransfer = minionCards.GetComponentsInChildren<PhotonView>();
        for (int i = 0; i < minionTransfer.Length; i++)
            minionTransfer[i].TransferOwnership(currentPhotonPlayer);

        PhotonView[] shopTransfer = shopCards.GetComponentsInChildren<PhotonView>();
        for (int i = 0; i < shopTransfer.Length; i++)
            shopTransfer[i].TransferOwnership(currentPhotonPlayer);

        Debug.Log("All items transfered.");
    }
    
    
}
