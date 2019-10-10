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
    private bool offline;
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
        offline = PhotonNetworkManager.IsOffline;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (offline || photonView.IsMine)
        {
            if (other.transform.parent.gameObject.GetComponent<HandContainer>() == null)
            {
                return;
            }

            //Debug.Log("Card has entered");
            cardInPlayZone = true;
            cardInZone = other.gameObject.GetComponent<PlayerCardDisplay>();
            if(!offline)
                photonView.RPC("RPCOnTriggerEnter2D", RpcTarget.Others, cardInZone);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (offline || photonView.IsMine)
        {
            //Debug.Log("Card has left");
            cardInPlayZone = false;
            cardInZone = null;
            if (!offline)
                photonView.RPC("RPCOnTriggerExit2D", RpcTarget.Others);
        }
    }

    private void Update()
    {
        if (cardInPlayZone)
        {
            if (offline || photonView.IsMine)
            {
                if (!Input.GetMouseButton(0))
                {
                    HandleCardPlayed();
                    if (!offline)
                        photonView.RPC("RPCPlayZoneUpdate", RpcTarget.Others);
                }
            }
        }
    }

    private void HandleCardPlayed()
    {
        //Debug.Log(col.gameObject.name + " has entered the scene");
        // Card stuff
        Hand tpHand = TurnManager.Instance.turnPlayer.hand;
        PlayerCardDisplay cardDisplay = cardInZone;
        PlayerCard cardPlayed = cardDisplay.card;
        
        GameObject.Destroy(cardInZone.gameObject);
        
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
    private void RPCOnTriggerEnter2D(PlayerCardDisplay RPCcardInZone)
    {
        cardInZone = RPCcardInZone;
    }

    [PunRPC]
    private void RPCOnTriggerExit2D()
    {
        cardInZone = null;
    }

    [PunRPC]
    private void RPCPlayZoneUpdate(PlayerCardDisplay cardClicked)
    {
        HandleCardPlayed();
    }

}
