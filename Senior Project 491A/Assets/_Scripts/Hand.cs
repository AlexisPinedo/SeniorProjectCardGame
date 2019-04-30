using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    /* Max size of the Player's Hand */
    private static int handSize = 6;

    /* List of Cards currently in the Hand - order not necessary */
    [SerializeField]
    private List<Card> hand;

    /* Current number of cards in the Player's Hand */
    private int cardsInHand = 0;

    /* Reference for the Player's Deck - set in Start() */
    [SerializeField]
    private Deck deck;

    /* Player's Grid for the cards in their Hand - set in Start()*/
    private CreateGrid handGrid;

    /* Spawn point for car */
    private Vector2 spot = new Vector2();

    // NOTE: Required for referencing the Player's Deck and HandGrid
    public GameObject playerSpace;
    public GameObject playerObj;

    void Start()
    {
        // Set references for Deck and Hand Grid
        deck = playerObj.GetComponentInChildren<Deck>();
        handGrid = playerSpace.GetComponentInChildren<CreateGrid>();
    }
    
    public void AddCard()
    {
        // Draw card from deck and add it to the Hand's list
        Card cardDrawn;
        Vector2 spawnPoint;

        cardDrawn = deck.DrawCard();
        Debug.Log(cardDrawn.ToString());  // DEBUG
        hand.Add(cardDrawn);

        spawnPoint = handGrid.GetNearestPointOnGrid(spot);
        cardDrawn.transform.position = spawnPoint;
        spot.x += 2.0f;

        cardsInHand += 1;
    }

    public List<Card> GetHand()
    {
        return hand;
    }

    public void DiscardCard(Card card)
    {
        if (hand.Contains(card))
        {
            hand.Remove(card);
            cardsInHand -= 1;
        }
    }

    public int getHandCount()
    {
        return this.cardsInHand;
    }
}
