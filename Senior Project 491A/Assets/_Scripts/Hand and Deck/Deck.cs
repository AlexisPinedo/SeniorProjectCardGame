using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Deck : MonoBehaviour
{
    //Singleton pattern to ensure only one deck is created globally.
    // private Deck instance = null;

    public delegate void _cardDrawn(Card aCard);
    public static event _cardDrawn CardDrawn;


    [SerializeField]
    private Stack<Card> cardsInDeck = new Stack<Card>();

    [SerializeField]
    private int cardCopies;

    public List<Card> testCards = new List<Card>(10);

    /// TODO
    // Temp fix until we get more cards in the system
    protected void fillDeck()
    {
        foreach (var card in testCards)
        {
            for (int i = 0; i < cardCopies; i++)
            {
                Card copy = card;
                //copy.transform.parent = this.transform;
                Debug.Log("copy: " + copy);
                AddCard((Card)copy);
            }
        }
        Shuffle();
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
        Card cardPopped = cardsInDeck.Pop();
        if (CardDrawn != null)
        {
            CardDrawn(cardPopped);
        }
        return cardPopped;

    }

    public void AddCard(Card card)
    {
        Debug.Log("Card: " + card.cardName + " has been pushed onto the stack");
        cardsInDeck.Push(card);
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

