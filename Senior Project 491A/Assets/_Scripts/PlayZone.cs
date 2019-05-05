using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayZone : MonoBehaviour
{
    /* References the Match's TurnManager */
    public TurnManager manager;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " has entered the scene");

        Hand tpHand = manager.turnPlayer.GetComponentInChildren<Hand>();

        // Card stuff
        PlayerCard card = col.gameObject.GetComponent<PlayerCard>();
        Debug.Log("Card Name: " + card.cardName);
        
        int currency = card.cardCurrency;
        int atk = card.cardAttack;
        Debug.Log("Currency: " + currency);
        Debug.Log("Attack: " + atk);

        tpHand.SendToGraveyard(card);
        GameObject.Destroy(card.gameObject);
    }
}
