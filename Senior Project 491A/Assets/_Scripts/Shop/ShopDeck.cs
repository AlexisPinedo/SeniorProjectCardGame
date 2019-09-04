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

        foreach (Card card in cardsToAdd)
        {
            for (int i = 0; i < cardCopies; i++)
            {
                cardsInDeck.Push(card);

            }
        }
        Debug.Log("cards added");
    }
}
