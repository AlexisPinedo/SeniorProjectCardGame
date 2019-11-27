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
            if (!Input.GetMouseButton(0) && !AnimationManager.SharedInstance.CardAnimActive)
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
        if(!PhotonNetwork.OfflineMode)
            cardDisplay.photonView.RPC("DestroyCard", RpcTarget.Others);

        //StartCoroutine(TransformCard(cardInZone, enlargementZone.position));
        //Destroy(cardInZone.gameObject);
        AnimationManager.SharedInstance.PlayAnimation(cardInZone, enlargementZone.position, 0.25f, true, true,true);
        
        TurnPlayerManager.Instance.TurnPlayer.Power += cardPlayed.CardAttack;
        TurnPlayerManager.Instance.TurnPlayer.Currency += cardPlayed.CardCurrency;

        photonView.RPC("SendPlayerValues", RpcTarget.Others,
    TurnPlayerManager.Instance.TurnPlayer.Power, TurnPlayerManager.Instance.TurnPlayer.Currency);

        if (!cardPlayed.CardName.Equals("Phantom"))
        {
            tpHand.hand.Remove(cardPlayed);
            TurnPlayerManager.Instance.TurnPlayer.playerGraveyard.graveyard.Add(cardPlayed);
        }

        CardPlayed?.Invoke(cardPlayed);
        HasPlayed?.Invoke();
        cardInPlayZone = false;
        cardInZone = null;
    }

    [PunRPC]
    private void SendPlayerValues(int powerValue, int currencyValue)
    {
        TurnPlayerManager.Instance.TurnPlayer.Power = powerValue;
        TurnPlayerManager.Instance.TurnPlayer.Currency = currencyValue;
    }



}
