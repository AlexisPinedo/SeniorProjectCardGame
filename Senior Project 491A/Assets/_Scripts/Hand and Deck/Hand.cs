using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private static int handSize = 6;

    [SerializeField]
    // private List<Card> hand = new List<Card>(handSize);
    private List<PlayerCard> hand;

    private int cardsInHand = 0;

    [SerializeField]
    private Deck deck;

    private CreateGrid handGrid;

    /* Spawn point for car */
    private Vector2 spot = new Vector2();

    void Start()
    {
        // TODO: Verify if this fix is suitable
        // Create GameObjects to find them by their name
        GameObject goDeck = GameObject.Find("Player1Deck");
        GameObject goHandGrid = GameObject.Find("Player1CardGrid");
        
        // Reference the player's components
        if (goDeck != null)
        {
            deck = goDeck.GetComponent<Deck>();
            print("Deck: " + deck);
        }
        if (goHandGrid != null)
        {
            handGrid = goHandGrid.GetComponent<CreateGrid>();
            print("Grid: " + handGrid);
        }
    }

    public void AddCard()
    {
        // Draw card from deck and add it to the Hand's list
        PlayerCard cardDrawn;
        Vector2 spawnPoint;

        cardDrawn = deck.DrawCard();
        Debug.Log(cardDrawn.ToString());  // DEBUG
        hand.Add(cardDrawn);

        spawnPoint = handGrid.GetNearestPointOnGrid(spot);
        cardDrawn.transform.position = spawnPoint;
        spot.x += 2.0f;

        cardsInHand += 1;
    }

    public List<PlayerCard> GetHand()
    {
        return hand;
    }

    public void DiscardCard(PlayerCard card)
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
