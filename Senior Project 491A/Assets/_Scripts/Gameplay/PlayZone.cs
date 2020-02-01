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

    public Transform enlargementZone;

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
        TurnPlayerManager.PlayerSwitched += HandleNetworkActiveCollider;
    }

    private void OnDisable()
    {
        TurnPlayerManager.PlayerSwitched -= HandleNetworkActiveCollider;
    }

    private void Update()
    {
        if(NetworkOwnershipTransferManger.currentPhotonPlayer != PhotonNetwork.LocalPlayer)
            return;
        
        if (cardInPlayZone)
        {
            //if (!Input.GetMouseButton(0))
            //{
            //    HandleCardPlayed();
            //}
            if (Input.GetMouseButtonUp(0))
            {
                HandleCardPlayed();
            }
        }
    }
    
    private void HandleNetworkActiveCollider()
    {
        if(!PhotonNetwork.OfflineMode)
        {
            if (NetworkOwnershipTransferManger.currentPhotonPlayer.IsLocal)
                playZoneCollider.enabled = true;
            else
            {
                playZoneCollider.enabled = false;
            }
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.gameObject.GetComponent<HandContainer>() == null)
        {
            return;
        }

        //Debug.Log("Card has entered");
        cardInPlayZone = true;
        if(other.gameObject.GetComponent<PlayerCardDisplay>() != null)
            cardInZone = other.gameObject.GetComponent<PlayerCardDisplay>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ResetPlayZoneValues();
    }

    private void ResetPlayZoneValues()
    {
        cardInPlayZone = false;
        cardInZone = null;
    }

    private void HandleCardPlayed()
    {
        // Card stuff
        Hand tpHand = TurnPlayerManager.Instance.TurnPlayer.hand;
        PlayerCardDisplay cardDisplay = cardInZone;
        PlayerCard cardPlayed = cardDisplay.card;

        //StartCoroutine(TransformCard(cardInZone, enlargementZone.position));
        //AnimationManager.SharedInstance.PlayAnimation(cardInZone, enlargementZone.position, 0.25f, true, true,true);
        PlayerAnimationManager.SharedInstance.PlayAnimation(cardInZone, enlargementZone.position, 0.25f, true, true, true);
        
        TurnPlayerManager.Instance.TurnPlayer.Power += cardPlayed.CardAttack;
        TurnPlayerManager.Instance.TurnPlayer.Currency += cardPlayed.CardCurrency;

        if (!PhotonNetwork.OfflineMode)
            photonView.RPC("SendPlayerValues", RpcTarget.Others, TurnPlayerManager.Instance.TurnPlayer.Power, TurnPlayerManager.Instance.TurnPlayer.Currency);

        if (!cardPlayed.CardName.Equals("Phantom"))
            TurnPlayerManager.Instance.TurnPlayer.playerGraveyard.graveyard.Add(cardPlayed);
        
        tpHand.hand.Remove(cardPlayed);
        CardPlayed?.Invoke(cardPlayed);
        HasPlayed?.Invoke();
        cardInPlayZone = false;
        cardInZone = null;

        if (!PhotonNetwork.OfflineMode)
        {
            photonView.RPC("RemoteHandleCard", RpcTarget.Others, cardDisplay.photonView.ViewID);
            cardDisplay.photonView.RPC("DestroyCard", RpcTarget.Others);
        }
    }

    [PunRPC]
    private void SendPlayerValues(int powerValue, int currencyValue)
    {
        TurnPlayerManager.Instance.TurnPlayer.Power = powerValue;
        TurnPlayerManager.Instance.TurnPlayer.Currency = currencyValue;
    }

    [PunRPC]
    private void RemoteHandleCard(int cardID)
    {
        PhotonView foundCard = PhotonView.Find(cardID);
        if (foundCard)
        {
            PlayerCardDisplay playedCard = foundCard.GetComponent<PlayerCardDisplay>();
            PlayerCard cardPlayed = playedCard.card;
            //TurnPlayerManager.Instance.TurnPlayer.Power += cardPlayed.CardAttack;
            //TurnPlayerManager.Instance.TurnPlayer.Currency += cardPlayed.CardCurrency;

            if (!cardPlayed.CardName.Equals("Phantom"))
                TurnPlayerManager.Instance.TurnPlayer.playerGraveyard.graveyard.Add(cardPlayed);

            Hand tpHand = TurnPlayerManager.Instance.TurnPlayer.hand;
            tpHand.hand.Remove(cardPlayed);
            CardPlayed?.Invoke(cardPlayed);
            HasPlayed?.Invoke();
        }
        else
        {
            Debug.Log("Card with Photon ViewID " + cardID + " not found");
        }
    }



}
