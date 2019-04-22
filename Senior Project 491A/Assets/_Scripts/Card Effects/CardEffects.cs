using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffects : MonoBehaviour
{
    [NonSerialized] public int cost;
    [NonSerialized] public int currency;
    [NonSerialized] public int power;

    public Card card;
    private Player turnPlayer;

    void Start()
    {
        turnPlayer = FindObjectOfType<Player>();
    }

    // Start is called before the first frame update
    public CardEffects()
    {
        cost = card.cardCost;
        currency = card.cardCurrency;
        power = card.cardAttack;
    }

    //Does this add the power on the card itself or the Game Session
    public void addPower(int pwr)
    {
        turnPlayer.power += pwr;
    }

    public void subtractPower(int pwr)
    {
        turnPlayer.power -= pwr;
    }

    public void setPower(int pwr)
    {
        turnPlayer.power = pwr;
    }

    public void addCurrency(int curr)
    {
        turnPlayer.currency += curr;
    }

    public void subtractCurrency(int curr)
    {
        turnPlayer.currency -= curr;
    }

    //TODO: Need to access Player's currency and set it????
    public void setCurrency(int curr)
    {
        turnPlayer.currency = curr;
    }



}
