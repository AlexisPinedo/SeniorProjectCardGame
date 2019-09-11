using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrefillableDeck : Deck
{
    [SerializeField] protected List<Card> cardsToAdd = new List<Card>();
    [SerializeField] protected int cardCopies;
    
    
    protected virtual void OnEnable()
    {
        
        foreach (Card card in cardsToAdd)
        {
            for (int i = 0; i < cardCopies; i++)
            {
                cardsInDeck.Push(card);
            }
        }
        
        Shuffle();
        //Debug.Log("cards added");
    }
}
