eusing System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayZone : MonoBehaviour
{
    /* Triggers when a PlayerCard is dragged into the Play Zone */

    private static PlayZone _instance;

    public static PlayZone Instance
    {
        get => _instance;
    }


    public static bool cardInPlayZone = false;
    public static PlayerCardHolder cardInZone;
    
    public delegate void _CardPlayed(PlayerCard cardPlayed);

    public static event _CardPlayed CardPlayed;
    
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
        if (other.transform.parent.gameObject.GetComponent<HandContainer>() == null)
        {
            return;
        }
        
        //Debug.Log("Card has entered");
        cardInPlayZone = true;
        cardInZone = other.gameObject.GetComponent<PlayerCardHolder>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Card has left");
        cardInPlayZone = false;
        cardInZone = null;
    }

    
    public void HandleCardPlayed()
    {
        //Debug.Log(col.gameObject.name + " has entered the scene");

        // Card stuff
        Hand tpHand = TurnManager.Instance.turnPlayer.hand;
        PlayerCardHolder cardHolder = cardInZone;
        PlayerCard cardPlayed = cardHolder.card;
        TurnManager.Instance.turnPlayer.Power += cardPlayed.CardAttack;
        TurnManager.Instance.turnPlayer.Currency += cardPlayed.CardCurrency;

        if (!cardPlayed.CardName.Equals("Phantom"))
        {
            tpHand.hand.Remove(cardPlayed);
            TurnManager.Instance.turnPlayer.graveyard.graveyard.Add(cardPlayed);
        }
        
        CardPlayed?.Invoke(cardPlayed);

        GameObject.Destroy(cardInZone.gameObject);
        
        cardInPlayZone = false;
        cardInZone = null;
    }
}
