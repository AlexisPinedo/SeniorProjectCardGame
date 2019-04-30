using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

/**
    Defines the Player class, i.e, everything that the Player can do and
    has access to during a match.
 */
public class Player : MonoBehaviour
{
    /* Player's Hand */
    [SerializeField]
    private Hand hand;

    /* Player's Deck */
    [SerializeField]
    private Deck deck;

    /* Player's currency count */
    [SerializeField]
    private int currency;

    /* Player's power count */
    [SerializeField]
    private int power;

    /* Player's Graveyard */
    [SerializeField]
    private Graveyard graveyard;

    /* Player's Reward Pile, i.e., defeated enemies */
    [SerializeField]
    private List<PlayerCard> rewardPile;

    [SerializeField]
    private TurnManager turnManager;

    void Start()
    {
        // TODO
    }

    //-------------------//
    //----- GETTERS -----//
    //-------------------//
    public int getCurrency()
    {
        return this.currency;
    }
    public int getPower()
    {
        return this.power;
    }

    //-------------------//
    //----- SETTERS -----//
    //-------------------//

    // ...for Currency
    public void setCurrency(int newCurrency)
    {
        if (newCurrency >= 0)
        {
            this.currency = newCurrency;
        }
    }
    public void addCurrency(int currency)
    {
        this.currency += currency;
    }
    public void subtractCurrency(int currency)
    {
        if (this.currency - currency <= 0)
        {
            this.currency = 0;
        }
        else
        {
            this.currency -= currency;
        }
    }

    // ...for Power
    public void setPower(int newPower)
    {
        if (newPower >= 0)
        {
            this.power = newPower;
        }
    }
    public void addPower(int power)
    {
        if (power > 0)
        {
            this.power += power;
        }
    }
    public void subtractPower(int power)
    {
        if (this.power - power <= 0)
        {
            this.power = 0;
        }
        else
        {
            this.power -= power;
        }
    }

    void Update()
    {
        // Check if the player has pressed the "draw" button
        if (turnManager.IsPlayerOnesTurn() && Input.GetKeyDown("space") && hand.GetHandCount() < 6)
        {
            // Get card from deck
            hand.AddCard();
        }
    }
}
