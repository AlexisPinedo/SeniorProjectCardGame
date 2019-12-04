using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines what is part of a Player's Hand.
/// </summary>
[CreateAssetMenu(menuName = "Player Component/Hand")]
public class Hand : ScriptableObject
{
    /// <summary>
    /// List of PlayerCard objects.
    /// </summary>
    [SerializeField] public List<PlayerCard> hand;

    //private int cardsInHand = 0;
    //[SerializeField] private CardContainer cardContainter;

    private void OnEnable()
    {
        hand.Clear();
    }
       
}

