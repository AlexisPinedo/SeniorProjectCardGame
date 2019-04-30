using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    //Singleton pattern to ensure only one deck is created globally.
    // private Deck instance = null;

    [SerializeField]
    private Stack<PlayerCard> cardsInDeck = new Stack<PlayerCard>();

    // DEBUG: Change later to grab cards from a single-source?
    [SerializeField]
    private PlayerCard gameCard;

    public List<Card> testCards = new List<Card>(10);

    /* Reference to the Player whose Deck this is */
    [SerializeField]
    private GameObject playerObj;
    
    // Reference for the player's graveyard
    private Graveyard playersGraveyard;

    void Awake()
    {
        // Reference player's components
        playersGraveyard = playerObj.GetComponentInChildren<Graveyard>();

        fillDeck();
    }

    /// TODO
    // Temp fix until we get more cards in the system
    private void fillDeck()
    {
        for (int i = 0; i < testCards.Count; i++)
        {
            Card copy = Instantiate(testCards[i]);
            copy.transform.parent = this.transform;
            AddCard((PlayerCard)copy);
        }
    }

    public Stack<PlayerCard> getDeck()
    {
        return this.cardsInDeck;
    }

    public PlayerCard RevealTopCard()
    {
        return cardsInDeck.Peek();
    }

    public PlayerCard DrawCard()
    {
        return cardsInDeck.Pop();
    }

    public void AddCard(PlayerCard card)
    {
        cardsInDeck.Push(card);
    }

    public void AddToGraveYard(PlayerCard card)
    {
        playersGraveyard.AddToGrave(card);
    }

    public void Shuffle()
    {
        System.Random random = new System.Random();
        var deckList = cardsInDeck.ToArray();
        int n = deckList.Length;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            PlayerCard value = deckList[k];
            deckList[k] = deckList[n];
            deckList[n] = value;
        }
        cardsInDeck = new Stack<PlayerCard>(deckList);
    }
}
