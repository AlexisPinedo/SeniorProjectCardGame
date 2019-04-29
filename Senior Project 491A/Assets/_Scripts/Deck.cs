using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    //Singleton pattern to ensure only one deck is created globally.
    // private Deck instance = null;

    [SerializeField]
    public Stack<Card> cardsInDeck = new Stack<Card>();

    // Reference for the player's graveyard
    public Graveyard playersGraveyard;
    
    [SerializeField]
    public Card gameCard;

    void Awake()
    {
        print("Deck.cs, Start()");
        
        // Reference player's components
        playersGraveyard = this.GetComponentInParent<Graveyard>();
        for (int i =0; i < 20; i++)
        {
            Card copy = Instantiate(gameCard);
            AddCard(copy);
        }
        // fillDeck();
    }

    private void fillDeck()
    {
        cardsInDeck.Push(gameCard);
    }

    public Stack<Card> getDeck()
    {
        return this.cardsInDeck;
    }

    public Card RevealTopCard()
    {
        return cardsInDeck.Peek();
    }

    public Card DrawCard()
    {
        return cardsInDeck.Pop();
    }

    public void AddCard(Card card)
    {
        cardsInDeck.Push(card);
    }

    public void AddToGraveYard(Card card)
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
            Card value = deckList[k];
            deckList[k] = deckList[n];
            deckList[n] = value;
        }
        cardsInDeck = new Stack<Card>(deckList);
    }
}
