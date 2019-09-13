using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HandContainer : PlayerCardContainer
{

    public Hand hand;
    public Deck playerDeck;
    public Graveyard playerGrave;
    public PlayerCard phantomCard;

    [SerializeField]
    private int DefaultHandSize = 5;

    private void Start()
    {
        InitialCardDisplay();
    }


    protected override void InitialCardDisplay()
    {
        PlayerCard cardDrawn = null;
        for (int i = 0; i < DefaultHandSize; i++)
        {
            if (playerDeck.cardsInDeck.Count > 0)
                cardDrawn = (PlayerCard)playerDeck.cardsInDeck.Pop();
            else
            {
                if (playerGrave.graveyard.Count > 0)
                {
                    foreach (var card in playerGrave.graveyard)
                    {
                        playerDeck.cardsInDeck.Push(card);
                        playerGrave.graveyard.Remove(card);
                    }
                    playerDeck.Shuffle();
                }
                else
                {
                    //Draw a phantom card
                    //Debug.Log("Drawing Phantom Card");
                    cardDrawn = phantomCard;
                }
            }

            if (cardDrawn == null)
            {
                //Debug.Log("null card");
                return;
            }

            holder.card = cardDrawn;
            Instantiate(holder, containerGrid.freeLocations.Pop(), Quaternion.identity, this.transform);
            hand.hand.Add(cardDrawn);

            base.InitialCardDisplay();
        }
    }

    /// <summary>
    /// Places a player's card onto the hand grid.
    /// </summary>
    /// <param name="card"></param>
    private void PlaceCard(PlayerCard cardToPlace)
    {
        //Vector2 spawnPoint;
        //spawnPoint = handGrid.GetNearestPointOnGrid(cardSpot);

        //if (handGrid.IsPlaceable(spawnPoint))
        //{
        //    card.SetCoord(spawnPoint);
        //    hand.Add(card);

        //    inHandObjects.Add(Instantiate(card, this.transform).gameObject);
        //    cardsInHand += 1;
        //    cardSpot.x += handGrid.size;
        //}
    }
}
