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
    private PlayerDeck deck;

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

    private bool drawn = false;

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

    public void addToPlayerGraveyard(PlayerCard purchasedCard){
        graveyard.addToGrave(purchasedCard);
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
        FindObjectOfType<currencyDisplay>().UpdateCurrencyDisplay();
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

        FindObjectOfType<currencyDisplay>().UpdateCurrencyDisplay();
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
        if (Input.GetKeyDown("space") && !drawn)
        {
            Debug.Log("initial draw");
            initialDraw();
            drawn = true;
        }
    }

    /* Adds a card to your hand */
    void drawCard()
    {
        hand.AddCard();
    }

    private void initialDraw()
    {
        hand.turnStartDraw();
    }
}
