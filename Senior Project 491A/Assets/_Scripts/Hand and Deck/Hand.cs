using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    Defines the Player's Hand
 */
public class Hand : MonoBehaviour
{
    /* Max size of the Player's Hand */
    private static int handSize = 6;

    /* List of Cards currently in the Hand - order not necessary */
    [SerializeField]
    private List<Card> hand;

    /* CURRENT number of cards in the Player's Hand */
    private int cardsInHand = 0;

    /* Reference for the Player's Deck - set in Start() */
    private Deck deck;

    /* Player's Grid for the cards in their Hand - set in Start()*/
    private CreateGrid handGrid;

    /* Spawn point for card */
    private Vector2 spot = new Vector2();

    /* Reference to the Player whose Hand it is */
    [SerializeField]
    private GameObject playerObj;
    [SerializeField]
    private GameObject playerSpace;

    void Start()
    {
        // Set references for Deck and Hand Grid
        deck = playerObj.GetComponentInChildren<Deck>();
        handGrid = playerSpace.GetComponentInChildren<CreateGrid>();
    }

    /* Adds a card to the Hand from the Deck */
    public void AddCard()
    {
        Card cardDrawn;
        Vector2 spawnPoint;

        // Draw card & add it to the Hand
        cardDrawn = deck.DrawCard();
        hand.Add(cardDrawn);

        // Figure out where to display the Card
        spawnPoint = handGrid.GetNearestPointOnGrid(spot);
        cardDrawn.transform.position = spawnPoint;

        // Move to the next spot on the grid
        spot.x += 2.0f;

        cardsInHand += 1;
        Debug.Log("Cards in hand = " + cardsInHand);
    }

    //-------------------//
    //----- GETTERS -----//
    //-------------------//
    public List<Card> GetHand()
    {
        return this.hand;
    }
    public int GetHandCount()
    {
        return this.cardsInHand;
    }

    public void DiscardCard(Card card)
    {
        if (hand.Contains(card))
        {
            hand.Remove(card);
            cardsInHand -= 1;
        }
    }
}
