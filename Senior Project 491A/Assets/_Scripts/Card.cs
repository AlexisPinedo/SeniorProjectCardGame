using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public string cardDescription;
    public string cardEffect;

    public Sprite cardArtwork;
    public Sprite cardElement;

    public int cardCost;
    public int cardCurrency;
    public int cardAttack;
}
