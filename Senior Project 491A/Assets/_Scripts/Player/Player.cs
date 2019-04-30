using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Hand hand;

    [SerializeField]
    private Deck deck;

    [SerializeField]
    private int currency;

    [SerializeField]
    private int power;

    [SerializeField]
    private Graveyard graveyard;

    [SerializeField]
    private List<PlayerCard> rewardPile;

    /* References the grid space for cards played during the turn */
    [SerializeField]
    private CreateGrid handGrid;

    void Start()
    {
        // Set private fields if they aren't already set
    }

    //Getters
    public int getCurrency()
    {
        return this.currency;
    }
    public int getPower()
    {
        return this.power;
    }

    // Setters...
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
        } else {
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
        } else {
            this.power -= power;
        }
    }

    void Update()
    {
        // Check if the player has pressed the "draw" button
        if (Input.GetKeyDown("space") && hand.getHandCount() < 6)
        {
            // Get card from deck
            hand.AddCard();
        }
    }
}
