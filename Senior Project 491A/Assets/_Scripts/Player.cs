using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player : ScriptableObject
{
    public int currency;
    public int power;
    public Hand hand = ScriptableObject.CreateInstance<Hand>();
    public Deck deck = ScriptableObject.CreateInstance<Deck>();
    public Graveyard graveyard = ScriptableObject.CreateInstance<Graveyard>();
    public List<Card> rewardPile = new List<Card>();
    
}
