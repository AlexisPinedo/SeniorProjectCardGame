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
    /* Player specific data  */
    [SerializeField] public Hand hand;
    [SerializeField] private PlayerDeck deck;
    [SerializeField] private int currency;
    [SerializeField] private int power;
    [SerializeField] public Graveyard graveyard;
    [SerializeField] private List<PlayerCard> rewardPile;

    /* Reference to the game's Turn Manager */
    [SerializeField] private TurnManager turnManager;

    private bool drawn = false;

    void Start()
    {
        // TODO
    }

    //-------------------//
    //----- GETTERS -----//
    //-------------------//
    public int GetCurrency()
    {
        return this.currency;
    }
    public int GetPower()
    {
        return this.power;
    }

    public void AddToPlayerGraveyard(PlayerCard purchasedCard)
    {
        graveyard.AddToGrave(purchasedCard);
    }
    //-------------------//
    //----- SETTERS -----//
    //-------------------//

    // ...for Currency
    public void SetCurrency(int newCurrency)
    {
        if (newCurrency >= 0)
        {
            this.currency = newCurrency;
        }
    }
    public void AddCurrency(int currency)
    {
        this.currency += currency;
    }
    public void SubtractCurrency(int currency)
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
    public void SetPower(int newPower)
    {
        if (newPower >= 0)
        {
            this.power = newPower;
        }
    }
    public void AddPower(int power)
    {
        if (power > 0)
        {
            this.power += power;
        }
    }
    public void SubtractPower(int power)
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
            InitialDraw();
            drawn = true;
        }
        else if (Input.GetKeyDown("space") && drawn)
        {
            DrawCard();
        }
        else if (Input.GetKeyDown("left ctrl"))
        {
            hand.SendHandToGraveyard();
        }
    }

    /* Adds a card to your hand */
    private void DrawCard()
    {
        hand.AddCard();
    }

    private void InitialDraw()
    {
        hand.TurnStartDraw();
    }
}
