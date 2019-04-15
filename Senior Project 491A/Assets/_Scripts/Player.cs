using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player : ScriptableObject
{
    private int currency;
    private int power;
    private Hand hand = ScriptableObject.CreateInstance<Hand>();
    private Deck deck = ScriptableObject.CreateInstance<Deck>();
    private Graveyard graveyard = ScriptableObject.CreateInstance<Graveyard>();
    private List<Card> rewardPile = new List<Card>();
    
}
