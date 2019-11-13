using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkOwnershipTransferManger : MonoBehaviourPunCallbacks
{
    public static Photon.Realtime.Player photonPlayer1, photonPlayer2, currentPhotonPlayer;
    
    [SerializeField]
    private ShopContainer shopCards;

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
                    photonPlayer1 = player;
                else
                    photonPlayer2 = player;
            }

            currentPhotonPlayer = photonPlayer1;

            Debug.Log("Photon Player 1: " + photonPlayer1.NickName);
            Debug.Log("Photon Player 2: " + photonPlayer2.NickName);
        }
        else
        {
            //enabling offline mode allows all photon code to be ignored
            PhotonNetwork.OfflineMode = true;
            currentPhotonPlayer = photonPlayer1 = photonPlayer2 = PhotonNetwork.LocalPlayer;
            Debug.Log("Photon is offline...");
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

    private void SetCurrentPhotonPlayer()
    {
        if(TurnPlayerManager.Instance.TurnPlayer == TurnPlayerManager.Instance.Player1)
            currentPhotonPlayer = photonPlayer1;
        else if(TurnPlayerManager.Instance.TurnPlayer == TurnPlayerManager.Instance.Player2)
            currentPhotonPlayer = photonPlayer2;
    }
    
    public void TransferObjects()
    {
        
        bossCard.photonView.TransferOwnership(currentPhotonPlayer);

        PhotonView[] minionTransfer = minionCards.GetComponentsInChildren<PhotonView>();
        for (int i = 0; i < minionTransfer.Length; i++)
            minionTransfer[i].TransferOwnership(currentPhotonPlayer);

        PhotonView[] shopTransfer = shopCards.GetComponentsInChildren<PhotonView>();
        for (int i = 0; i < shopTransfer.Length; i++)
            shopTransfer[i].TransferOwnership(currentPhotonPlayer);

        Debug.Log("All items transfered.");
    }
    
    
}
