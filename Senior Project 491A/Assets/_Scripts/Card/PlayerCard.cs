using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using System;

/// <summary>
/// This class contains the data components for card that are player cards
/// Defines a player Card as a scriptable object inherits from card
/// </summary>
[CreateAssetMenu(menuName = "Card/Player Card")]
public class PlayerCard : Card
{
    //All this stuff below belongs in Player Card Class
    //This class uses many properties allowing all the variables to be read but not modified
    //========================================================
    //public bool inShop = true;

    [SerializeField] private int _cardCost;
    public int CardCost
    {
        get { return _cardCost; }
    }

    [SerializeField] private int _cardAttack;
    public int CardAttack
    {
        get { return _cardAttack; }
    }

    [SerializeField] private int _cardCurrency;
    public int CardCurrency
    {
        get { return _cardCurrency; }
    }

    [SerializeField]
    private List<CardTypes> CardEffectRequirement = new List<CardTypes>();

    public List<CardTypes> cardEffectRequirments
    {
        get { return CardEffectRequirement; }
    }

    public List<Sprite> cardCostsIcons = new List<Sprite>();
    
    
    
}
