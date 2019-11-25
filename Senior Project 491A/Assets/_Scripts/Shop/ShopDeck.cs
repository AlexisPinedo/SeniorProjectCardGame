using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(menuName = "Deck/Shop Deck")]
public class ShopDeck : PrefillableDeck<PlayerCard>
{
    [SerializeField	] private List<PlayerCard> CardsToAddLaterInGame = new List<PlayerCard>();

    [SerializeField]
    private int turnCountToAddOtherCards;

    protected override void OnEnable()
    {
        History.CardHistoryComponentsUpdated += AddCardsLaterInGame;
        
        foreach (PlayerCard card in cardsToAdd)
        {
            for (int i = 0; i < cardCopies; i++)
            {
                if (card.CardCost <= 3)
                {
                    cardsInDeck.Push(card);
                }
                else
                {
                    CardsToAddLaterInGame.Add(card);
                }
                
            }
        }
    }

    private void OnDisable()
    {
        History.CardHistoryComponentsUpdated -= AddCardsLaterInGame;
        CardsToAddLaterInGame.Clear();
    }

    private void AddCardsLaterInGame()
    {
        if(History.Instance.TurnCount != turnCountToAddOtherCards)
            return;
        foreach (PlayerCard card in CardsToAddLaterInGame)
        {
            cardsInDeck.Push(card);
        }
        
        cardsInDeck = ShuffleDeck.Shuffle(this);

        History.CardHistoryComponentsUpdated -= AddCardsLaterInGame;
    }

    public void OnBeforeSerialize()
    {
        throw new NotImplementedException();
    }

    public void OnAfterDeserialize()
    {
        throw new NotImplementedException();
    }
}
