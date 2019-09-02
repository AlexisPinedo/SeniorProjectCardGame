using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayZone : MonoBehaviour
{
    /* Triggers when a PlayerCard is dragged into the Play Zone */
    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.name + " has entered the scene");

        // Card stuff
        Hand tpHand = TurnManager.turnPlayer.GetComponentInChildren<Hand>();
        PlayerCard card = col.gameObject.GetComponent<PlayerCard>();
        TurnManager.turnPlayer.AddCurrency(card.cardCurrency);
        TurnManager.turnPlayer.AddPower(card.cardAttack);

        if (!card.cardName.Equals("Phantom"))
        {
            //Debug.Log("Sending played card to grave");

            tpHand.SendToGraveyard(card);
        }


        GameObject.Destroy(card.gameObject);

    }
}
