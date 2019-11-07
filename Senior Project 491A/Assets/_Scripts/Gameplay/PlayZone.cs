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
    public static event Action HasPlayed;

    private PhotonView RPCCardSelected;

    private BoxCollider2D playZoneCollider;

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

        playZoneCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        HandleNetworkActiveCollider();
    }

    private void OnEnable()
    {
        TurnManager.PlayerSwitched += HandleNetworkActiveCollider;
    }

    private void OnDisable()
    {
        TurnManager.PlayerSwitched -= HandleNetworkActiveCollider;
    }

    private void HandleNetworkActiveCollider()
    {
        if (TurnManager.currentPhotonPlayer.IsLocal)
            playZoneCollider.enabled = true;
        else
        {
            playZoneCollider.enabled = false;
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (photonView.IsMine)
        //{
            if (other.transform.parent.gameObject.GetComponent<HandContainer>() == null)
            {
                return;
            }

            //Debug.Log("Card has entered");
            cardInPlayZone = true;
            if(other.gameObject.GetComponent<PlayerCardDisplay>() != null)
                cardInZone = other.gameObject.GetComponent<PlayerCardDisplay>();

            RPCCardSelected = cardInZone.GetComponent<PhotonView>();
            //RPCCardSelected.RPC("RPCOnTriggerEnter2D", RpcTarget.Others, RPCCardSelected.ViewID);
        //}
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //if (photonView.IsMine)
        //{
            //Debug.Log("Card has left");
            cardInPlayZone = false;
            cardInZone = null;

            //this.photonView.RPC("RPCOnTriggerExit2D", RpcTarget.Others);
       // }
    }

    private void Update()
    {
        if(TurnManager.currentPhotonPlayer != PhotonNetwork.LocalPlayer)
            return;
        
        if (cardInPlayZone)
        {
            if (!Input.GetMouseButton(0))
            {
                if (PhotonNetwork.OfflineMode)
                {
                    HandleCardPlayed();
                }
                else
                {
                    this.photonView.RPC("RPCPlayZoneUpdate", RpcTarget.All);
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
        HasPlayed?.Invoke();
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
            if (foundCard.GetComponent<PlayerCardDisplay>() != null)
            {
                cardInZone = foundCard.GetComponent<PlayerCardDisplay>();
            }
            else
            {
                Debug.Log("Card container was null");
            }
        }
        else
        {
            Debug.Log("Photon View not found. CardID: " + cardID);
        }
    }

    [PunRPC]
    private void RPCOnTriggerExit2D()
    {

        cardInPlayZone = false;
        cardInZone = null;

    }

    [PunRPC]
    private void RPCPlayZoneUpdate()
    {
        HandleCardPlayed();
        //HasPlayed?.Invoke();
    }

}
