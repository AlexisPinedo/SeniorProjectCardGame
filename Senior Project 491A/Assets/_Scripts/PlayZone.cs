using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayZone : MonoBehaviour
{
    /* References the Match's TurnManager */
    public TurnManager manager;

    /* Triggers when a PlayerCard is dragged into the Play Zone */
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " has entered the scene");

        // Card stuff
        Hand tpHand = manager.turnPlayer.GetComponentInChildren<Hand>();
        PlayerCard card = col.gameObject.GetComponent<PlayerCard>();
        manager.turnPlayer.AddCurrency(card.cardCurrency);
        manager.turnPlayer.AddPower(card.cardAttack);

        tpHand.SendToGraveyard(card);
        GameObject.Destroy(card.gameObject);
    }
}
