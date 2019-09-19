using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

/**
    Defines the Player class, i.e, everything that the Player can do and
    has access to during a match.
 */
[CreateAssetMenu]
public class Player : ScriptableObject
{
    [SerializeField] public Hand hand;
    [SerializeField] private Deck deck;
    
    private int currency;
    public int Currency
    {
        get => currency;
        set => currency = value;
    }

    private int power;
    public int Power
    {
        get => power;
        set => power = value;
    }

    public Graveyard graveyard;
    [SerializeField] private List<PlayerCard> rewardPile;


}




//public class Player : ScriptableObject
//{
//public delegate void _CurrencyUpdated(int value);
    //public static event _CurrencyUpdated CurrencyChanged;

    //public delegate void _PowerUpdated(int value);
    //public static event _PowerUpdated PowerChanged;

    ///* Player specific data  */
    //[SerializeField] public Hand hand;
    //[SerializeField] private PlayerDeck deck;
    //[SerializeField] private int currency;
    //[SerializeField] private int power;
    //[SerializeField] public Graveyard graveyard;
    //[SerializeField] private List<PlayerCard> rewardPile;

    ///* Reference to the game's Turn Manager */
    ////[SerializeField] private TurnManager turnManager;

    //private bool drawn = false;

    //void Start()
    //{
    //    // ???
    //}

    ////-------------------//
    ////----- GETTERS -----//
    ////-------------------//
    //public int GetCurrency()
    //{
    //    return this.currency;
    //}
    //public int GetPower()
    //{
    //    return this.power;
    //}

    //public void AddToPlayerGraveyard(PlayerCard purchasedCard)
    //{
    //    graveyard.AddToGrave(purchasedCard);
    //}
    ////-------------------//
    ////----- SETTERS -----//
    ////-------------------//

    //// ...for Currency
    //public void SetCurrency(int newCurrency)
    //{
    //    if (newCurrency >= 0)
    //    {
    //        this.currency = newCurrency;
    //    }

    //    CurrencyChanged?.Invoke(currency);
    //}
    //public void AddCurrency(int currency)
    //{
    //    this.currency += currency;
    //    CurrencyChanged?.Invoke(this.currency);

    //}
    //public void SubtractCurrency(int currency)
    //{
    //    if (this.currency - currency <= 0)
    //    {
    //        this.currency = 0;
    //    }
    //    else
    //    {
    //        this.currency -= currency;
    //    }
    //    CurrencyChanged?.Invoke(this.currency);
    //}

    //// ...for Power
    //public void SetPower(int newPower)
    //{
    //    if (newPower >= 0)
    //    {
    //        this.power = newPower;
    //    }
    //    PowerChanged?.Invoke(power);
    //}
    //public void AddPower(int power)
    //{
    //    if (power > 0)
    //    {
    //        this.power += power;
    //    }
    //    PowerChanged?.Invoke(this.power);
    //}
    //public void SubtractPower(int power)
    //{
    //    if (this.power - power <= 0)
    //    {
    //        this.power = 0;
    //    }
    //    else
    //    {
    //        this.power -= power;
    //    }
    //    PowerChanged?.Invoke(this.power);
    //}

    //void Update()
    //{
    //    // Check if the player has pressed the "draw" button
    //    if (Input.GetKeyDown("space") && !drawn)
    //    {
    //        Debug.Log("initial draw");
    //        InitialDraw();
    //        drawn = true;
    //    }
    //    else if (Input.GetKeyDown("space") && drawn)
    //    {
    //        DrawCard();
    //    }
    //    else if (Input.GetKeyDown("left ctrl"))
    //    {
    //        hand.SendHandToGraveyard();
    //    }
    //}

    ///* Adds a card to your hand */
    //private void DrawCard()
    //{
    //    hand.AddCard();
    //}

    //private void InitialDraw()
    //{
    //    hand.TurnStartDraw();
    //}
//}
