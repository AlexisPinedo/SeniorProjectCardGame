using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Container class for the Deck scriptable object.
/// </summary>
public class PlayerDeckContainer : MonoBehaviour
{
    public PlayerDeck deck;

    private void Awake()
    {
        deck.cardsInDeck.Clear();
        TurnPlayerManager.Instance.TurnPlayer.Currency = 0;
        TurnPlayerManager.Instance.TurnPlayer.Power = 0;
    }
}
