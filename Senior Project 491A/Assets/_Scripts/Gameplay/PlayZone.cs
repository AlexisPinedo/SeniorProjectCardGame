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

    private void Update()
    {
        if(TurnManager.currentPhotonPlayer != PhotonNetwork.LocalPlayer)
            return;
        
        if (cardInPlayZone)
        {
            if (!Input.GetMouseButton(0))
            {
                HandleCardPlayed();
            }
        }
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
        Hand tpHand = TurnManager.Instance.turnPlayer.hand;
        PlayerCardDisplay cardDisplay = cardInZone;
        PlayerCard cardPlayed = cardDisplay.card;
        cardDisplay.photonView.RPC("DestroyCard", RpcTarget.Others);
        
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
    

}
