using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : ScriptableObject
{
    private List<Card> playerCards = new List<Card>();
    private Deck deck = ScriptableObject.CreateInstance<Deck>();
    
    //public Card playCard()
    //{
    //    Card selectedCard;
    //    
    //    return selectedCard;
    //}
    
    public void DrawCard()
    {
        playerCards.Add(deck.DrawCard());
    }
    
    public List<Card> GetHand()
    {
        return playerCards;
    }

    public void DiscardCard(Card card)
    {
        if (playerCards.Contains(card))
            playerCards.Remove(card);
    }
}
