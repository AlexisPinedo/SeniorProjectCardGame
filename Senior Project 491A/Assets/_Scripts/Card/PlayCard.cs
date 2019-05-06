using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCard : MonoBehaviour
{
    private PlayerCard card;
    private TurnManager currentPlayer;

    void Awake()
    {
        card = GetComponent<PlayerCard>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //Add currency to player
        //card.cardCurrency; 
        //Add card types played?
    }

    void OnTriggerExit2D(Collider2D other)
    {
       //SCENARIO: Subtract currency from player total, this only does this if the player brings the card into the field and then takes it out..opting not to play it
       
    }
}
