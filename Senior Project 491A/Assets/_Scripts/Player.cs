using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ScriptableObject
{
    private static Hand hand = ScriptableObject.CreateInstance<Hand>();
    private static Deck deck = ScriptableObject.CreateInstance<Deck>();
    private static Graveyard graveyard = ScriptableObject.CreateInstance<Graveyard>();
    static List<Card> rewardPile = new List<Card>();
    
}
