using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class HandContainer : PlayerCardContainer
{
    public Hand hand;
    public Deck playerDeck;
    public Graveyard playerGrave;
    public PlayerCard phantomCard;

    [SerializeField] private int DefaultHandSize = 5;

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
            {
                cardDrawn = (PlayerCard)playerDeck.cardsInDeck.Pop();
            }
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
                    // Draw a phantom card
                    cardDrawn = phantomCard;
                }
            }

            if (cardDrawn == null)
            {
                Debug.Log("null card");
                return;
            }

            // Set the PlayerCardContainer's PlayerCardHolder to the cardDrawn
            holder.card = cardDrawn;
            // Place it on the grid!
            Instantiate(holder, containerGrid.freeLocations.Dequeue(), Quaternion.identity, this.transform);
            // Add it to the HandContainer
            hand.hand.Add(cardDrawn);

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
