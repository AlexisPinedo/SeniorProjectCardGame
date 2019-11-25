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
public abstract class Deck<T> : ScriptableObject where T : Card
{
    /// <summary>
    /// The deck of cards represented in Stack form.
    /// </summary>
    [SerializeField] public Stack<T> cardsInDeck = new Stack<T>();
    
    //When the game starts we want to clear the cards in deck
    protected virtual void Awake()
    {
        cardsInDeck.Clear();
        //Debug.Log("Card have been cleared from the deck");
    }
    //Method that shuffles the deck. Possibly going to move out of card. 
}

