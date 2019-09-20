using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayZone : MonoBehaviour
{
    /* Triggers when a PlayerCard is dragged into the Play Zone */

    public delegate void _CardPlayed();

    public static event _CardPlayed CardPlayed;
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " has entered the scene");

        // Card stuff
        Hand tpHand = TurnManager.Instance.turnPlayer.hand;
        PlayerCardHolder cardHolder = col.gameObject.GetComponent<PlayerCardHolder>();
        PlayerCard cardPlayed = cardHolder.card;
        TurnManager.Instance.turnPlayer.Power += cardPlayed.CardAttack;
        TurnManager.Instance.turnPlayer.Currency += cardPlayed.CardCurrency;

        if (!cardPlayed.CardName.Equals("Phantom"))
        {
            tpHand.hand.Remove(cardPlayed);
            TurnManager.Instance.turnPlayer.graveyard.graveyard.Add(cardPlayed);
        }
        
        CardPlayed?.Invoke();

        GameObject.Destroy(col.gameObject);
    }
}
