using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The container object for the Shop, which itself can contain PlayerCardContainers.
/// </summary>
public class ShopContainer : PlayerCardContainer
{
    public ShopDeck shopDeck;
    
    //[SerializeField]
    //public PlayerCardHolder playerCardContainer;

    /// <summary>
    /// Maximum number of cards in the shop at any given time.
    /// </summary>
    private int shopCardCount = 6;

    // Start is called before the first frame update
    void Start()
    {
        InitialCardDisplay();
    }

    private void OnEnable()
    {
        PurchaseHandler.CardPurchased += DisplayNewCard;
    }
    
    private void OnDisable()
    {
        PurchaseHandler.CardPurchased -= DisplayNewCard;
    }

    /// <summary>
    /// Initializes the Shop's card's placements.
    /// </summary>
    protected override void InitialCardDisplay()
    {
        for (int i = 0; i < shopCardCount; i++)
        {
            if (shopDeck.cardsInDeck.Count <= 0)
            {
                Debug.Log("Shop deck is " + shopDeck.cardsInDeck.Count);
                return;
            }

            HandleDisplayOfACard();

            // Draw a Card from the ShopDeck
        }
    }

    private void HandleDisplayOfACard()
    {
        PlayerCard cardDrawn = null;
        cardDrawn = (PlayerCard)shopDeck.cardsInDeck.Pop();

        // PlayerCardHolder
        holder.card = cardDrawn;

        PlayerCardHolder cardHolder = Instantiate(holder, containerGrid.freeLocations.Pop(), Quaternion.identity, this.transform);
        cardHolder.enabled = true;
        
        Vector3 freeSpot = cardHolder.gameObject.transform.position;

        if (!containerGrid.cardLocationReference.ContainsKey(freeSpot))
            containerGrid.cardLocationReference.Add(freeSpot, cardHolder);
        else
            containerGrid.cardLocationReference[freeSpot] = cardHolder;
    }

    private void DisplayNewCard(PlayerCardHolder cardBought)
    {
        Vector3 freeSpot = cardBought.gameObject.transform.position;
        
        containerGrid.freeLocations.Push(freeSpot);
        HandleDisplayOfACard();
        
    }
}
