using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

/// <summary>
/// A Deck as a scriptable object.
/// This class is mainly contained within a DeckContainer.
/// The deck is stack of card game objects used for draws and card manipulation
/// </summary>
// This will allow to create a new deck in the project folder
[CreateAssetMenu(menuName = "Deck/Player Deck")]
public class Deck : ScriptableObject
{
    /// <summary>
    /// The deck of cards represented in Stack form.
    /// </summary>
    [SerializeField] public Stack<Card> cardsInDeck = new Stack<Card>();

    //When the game starts we want to clear the cards in deck
    protected virtual void Awake()
    {
        cardsInDeck.Clear();
        Debug.Log("Card have been cleared from the deck");
    }
    //Method that shuffles the deck. Possibly going to move out of card. 
}





//Obsolete method of the deck no longer used. 

//public class Deck : MonoBehaviour
//{
//public delegate void _cardDrawn(Card aCard);
//public static event _cardDrawn CardDrawn;

//[SerializeField]
//private int cardCopies;

//public List<Card> cardsToPlaceInDeck = new List<Card>(10);

//private void Awake()
//{
//    FillDeck();
//}

///// TODO
//// Temp fix until we get more cards in the system
//protected void FillDeck()
//{
//    foreach (var card in cardsToPlaceInDeck)
//    {
//        for (int i = 0; i < cardCopies; i++)
//        {
//            Card copy = card;
//            AddCard((Card)copy);
//        }
//    }
//    Shuffle();
//}

//public Stack<Card> GetDeck()
//{
//    return this.cardsInDeck;
//}

//public int GetDeckSize()
//{
//    return this.cardsInDeck.Count;
//}

//public Card RevealTopCard()
//{
//    return cardsInDeck.Peek();
//}

///* Draws a card from the deck and publishes the event for subscribers */
//public Card DrawCard()
//{
//    Card cardPopped = cardsInDeck.Pop();

//    // Notify subscribers, if any
//    if (CardDrawn != null)
//    {
//        //Debug.Log("Notifying subscribers");
//        CardDrawn(cardPopped);
//    }

//    return cardPopped;
//}

//public void AddCard(Card card)
//{
//    cardsInDeck.Push(card);
//}

//public void Shuffle()
//{
//    System.Random random = new System.Random();
//    var deckList = cardsInDeck.ToArray();
//    int n = deckList.Length;
//    while (n > 1)
//    {
//        n--;
//        int k = random.Next(n + 1);
//        Card value = deckList[k];
//        deckList[k] = deckList[n];
//        deckList[n] = value;
//    }
//    cardsInDeck = new Stack<Card>(deckList);
//}

//}
