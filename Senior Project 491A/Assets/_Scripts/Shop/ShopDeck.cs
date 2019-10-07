using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(menuName = "Deck/Shop Deck")]
public class ShopDeck : PrefillableDeck
{
    [SerializeField	] private List<PlayerCard> CardsToAddLaterInGame = new List<PlayerCard>();

    [SerializeField]
    private int turnCountToAddOtherCards;

    protected override void OnEnable()
    {
        History.TurnCounterUpdated += AddCardsLaterInGame;
        
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


        if (RandomNumberNetworkGenerator.Instance != null)
        {
            Debug.Log("trying to shuffle deck");

            Shuffle();
        }
    }

    private void OnDisable()
    {
        History.TurnCounterUpdated -= AddCardsLaterInGame;
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
        
        Shuffle();

        History.TurnCounterUpdated -= AddCardsLaterInGame;
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
