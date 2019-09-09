using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Represents the basic foundation of all Cards in the game, e.g., Player, Enemy, Boss, etc...
/// </summary>
public abstract class Card : MonoBehaviourPun
{
    /// <summary>
    /// The specific name of the card.
    /// </summary>
    public string cardName;
    
    /// <summary>
    /// Short description about the card, but not it's effect(s) in game.
    /// </summary>
    public string cardDescription;

    /// <summary>
    /// The Card's effect in game.
    /// </summary>
    public string cardEffect;

    /// <summary>
    /// The artwork for the card, stored as a ???
    /// </summary>
    public Sprite cardArtwork;
    
    /// <summary>
    /// The element, i.e. type, of the card.
    /// </summary>
    public Sprite cardElement;
}
