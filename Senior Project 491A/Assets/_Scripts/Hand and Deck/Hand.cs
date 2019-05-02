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
    private List<PlayerCard> hand;

    /* CURRENT number of cards in the Player's Hand */
    private int cardsInHand = 0;

    /* Reference for the Player's Deck - set in Start() */
    private PlayerDeck deck;

    private Graveyard graveyard;

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
        deck = playerObj.GetComponentInChildren<PlayerDeck>();
        graveyard = playerObj.GetComponentInChildren<Graveyard>();
        handGrid = playerSpace.GetComponentInChildren<CreateGrid>();
    }

    /* Adds a card to the Hand from the Deck.
        If there are no cards in the Deck but there is a graveyard then
        shuffle the graveyard and put it into the deck.
        NOTE: No phantom cards are drawn using this technique
     */
    public void AddCard()
    {
        Card cardDrawn;
        Vector2 spawnPoint;

        // Draw cards from the deck if there are cards in the Deck
        if (deck.getDeck().Count > 0)
        {
            Debug.Log("Deck has " + deck.getDeck().Count + " cards");
            cardDrawn = deck.DrawCard();
            //cardDrawn.transform.SetParent(this.transform);
            //hand.Add(cardDrawn);
        }
        else
        {
            Debug.Log("Deck is empty but the graveyard has " +
            graveyard.getGraveyard().Count + " cards");
            // Add graveyard to Deck and shuffle
            List<PlayerCard> gyard = graveyard.getGraveyard();
            foreach (var card in gyard)
            {
                deck.AddCard(card);
            }
            deck.Shuffle();

            cardDrawn = deck.DrawCard();
        }

        // Figure out where to display the Card
        spawnPoint = handGrid.GetNearestPointOnGrid(spot);
        cardDrawn.transform.position = spawnPoint;

        // Move to the next spot on the grid
        spot.x += 2.0f;

        Instantiate(cardDrawn, this.transform);

        cardsInHand += 1;
        Debug.Log("Cards in hand = " + cardsInHand);
    }

    /* A unique draw that is only done at the beginning of the Player's turn 
        If there are no cards in the Deck but there is a graveyard then
        shuffle the graveyard and put it into the deck.
        If there are no cards in the Deck and no graveyard, then draw
        a phantom card.
    */
    public void turnStartDraw()
    {
        Card cardDrawn;
        Vector2 spawnPoint;
        cardsInHand = 0;
        bool graveyardAdded = false;

        while (cardsInHand != 6)
        {
            // Draw cards from the deck if there are cards in the Deck
            if (deck.getDeck().Count > 0)
            {
                Debug.Log("Deck has " + deck.getDeck().Count + " cards");
                cardDrawn = deck.DrawCard();
            }
            else
            {
                if (graveyard.getGraveyard().Count > 0 && !graveyardAdded)
                {
                    Debug.Log("Deck is empty but the graveyard has " +
                    graveyard.getGraveyard().Count + " cards");
                    // Add graveyard to Deck and shuffle
                    List<PlayerCard> gyard = graveyard.getGraveyard();
                    foreach (var card in gyard)
                    {
                        deck.AddCard(card);
                    }
                    graveyardAdded = true;
                    deck.Shuffle();

                    cardDrawn = deck.DrawCard();
                }
                else
                {
                    // Draw a phantom card
                    Debug.Log("Deck is empty and graveyard is empty");
                    cardDrawn = deck.phantomCard;
                }
            }

            // Figure out where to display the Card
            spawnPoint = handGrid.GetNearestPointOnGrid(spot);
            cardDrawn.transform.position = spawnPoint;

            // Move to the next spot on the grid
            spot.x += 2.0f;

            Instantiate(cardDrawn, this.transform);

            cardsInHand += 1;
            Debug.Log("Cards in hand = " + cardsInHand);
        }
    }

    void UpdateHandDisplay()
    {

    }

    //-------------------//
    //----- GETTERS -----//
    //-------------------//
    public List<PlayerCard> GetHand()
    {
        return this.hand;
    }
    public int GetHandCount()
    {
        return this.cardsInHand;
    }

    public void DiscardCard(PlayerCard card)
    {
        if (hand.Contains(card))
        {
            hand.Remove(card);
            cardsInHand -= 1;
        }
    }
}
