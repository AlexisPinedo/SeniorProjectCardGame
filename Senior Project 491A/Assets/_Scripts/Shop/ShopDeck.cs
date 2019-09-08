using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShopDeck : Deck
{
    [SerializeField] private List<Card> cardsToAdd = new List<Card>();
    [SerializeField] private int cardCopies;

    protected override void Awake()
    {
        base.Awake();

    }

    private void OnEnable()
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
