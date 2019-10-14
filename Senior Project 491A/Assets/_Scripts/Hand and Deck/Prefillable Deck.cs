﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Prefillable decks are decks that can be created before run time
/// Creating a deck from this class will allow you to add cards into the game
/// before the game starts
/// </summary>
public abstract class PrefillableDeck : Deck
{
    
    [SerializeField] protected List<Card> cardsToAdd = new List<Card>();
    [SerializeField] protected int cardCopies;
    
    /// <summary>
    /// When the deck is enabled it will push the cards into the deck based on how many copies
    /// to add and then shuffle
    /// </summary>
     protected virtual void OnEnable()
     {        
         foreach (Card card in cardsToAdd)
         {
             for (int i = 0; i < cardCopies; i++)
             {
                 cardsInDeck.Push(card);
             }
         }
         //f(RandomNumberNetworkGenerator.Instance != null)
            
         //Debug.Log("cards added");
     }
 }
