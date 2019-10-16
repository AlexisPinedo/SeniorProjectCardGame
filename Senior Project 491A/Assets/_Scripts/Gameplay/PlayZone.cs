using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Photon.Pun;

public class PlayZone : MonoBehaviourPunCallbacks
{
    /* Triggers when a PlayerCard is dragged into the Play Zone */

    private static PlayZone _instance;

    public static PlayZone Instance
    {
        get => _instance;
    }

    public static bool cardInPlayZone = false;

    public static PlayerCardDisplay cardInZone;

    public delegate void _CardPlayed(PlayerCard cardPlayed);

    public static event _CardPlayed CardPlayed;

    private PhotonView RPCCardSelected;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (photonView.IsMine)
        {
            if (other.transform.parent.gameObject.GetComponent<HandContainer>() == null)
            {
                return;
            }

            //Debug.Log("Card has entered");
            cardInPlayZone = true;
            cardInZone = other.gameObject.GetComponent<PlayerCardDisplay>();

            RPCCardSelected = cardInZone.GetComponent<PhotonView>();
            this.photonView.RPC("RPCOnTriggerEnter2D", RpcTarget.Others, RPCCardSelected.ViewID);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (photonView.IsMine)
        {
            //Debug.Log("Card has left");
            cardInPlayZone = false;
            cardInZone = null;

            this.photonView.RPC("RPCOnTriggerExit2D", RpcTarget.Others);
        }
    }

    private void Update()
    {
        if (cardInPlayZone)
        {
            if (photonView.IsMine)
            {
                if (!Input.GetMouseButton(0))
                {
                    HandleCardPlayed();
                    this.photonView.RPC("RPCPlayZoneUpdate", RpcTarget.Others);
                }
            }
        }
    }

    private void HandleCardPlayed()
    {
        // Card stuff
        Hand tpHand = TurnManager.Instance.turnPlayer.hand;
        PlayerCardDisplay cardDisplay = cardInZone;
        PlayerCard cardPlayed = cardDisplay.card;
        
        Destroy(cardInZone.gameObject);
        
        TurnManager.Instance.turnPlayer.Power += cardPlayed.CardAttack;
        TurnManager.Instance.turnPlayer.Currency += cardPlayed.CardCurrency;

        if (!cardPlayed.CardName.Equals("Phantom"))
        {
            tpHand.hand.Remove(cardPlayed);
            TurnManager.Instance.turnPlayer.graveyard.graveyard.Add(cardPlayed);
        }

        CardPlayed?.Invoke(cardPlayed);
        cardInPlayZone = false;
        cardInZone = null;
    }

    [PunRPC]
    private void RPCOnTriggerEnter2D(int cardID)
    {
        PhotonView foundCard = PhotonView.Find(cardID);
        if (foundCard)
        {
            cardInPlayZone = true;
            cardInZone = foundCard.GetComponent<PlayerCardDisplay>();
        }
        else
        {
            Debug.Log("Photon View not found. CardID: " + cardID);
        }
    }

    [PunRPC]
    private void RPCOnTriggerExit2D(int cardID)
    {
        PhotonView foundCard = PhotonView.Find(cardID);
        if (foundCard)
        {
            cardInPlayZone = false;
            cardInZone = null;
        }
        else
        {
            Debug.Log("Photon View not found. CardID: " + cardID);
        }
    }

    [PunRPC]
    private void RPCPlayZoneUpdate()
    {
        HandleCardPlayed();
    }

}
