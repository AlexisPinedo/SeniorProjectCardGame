using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffects : MonoBehaviour
{
    [NonSerialized] public int cost;
    [NonSerialized] public int currency;
    [NonSerialized] public int power;

    public PlayerCard card;
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
    public void AddPower(int pwr)
    {
        turnPlayer.AddPower(pwr);
    }

    public void SubtractPower(int pwr)
    {
        turnPlayer.SubtractPower(pwr);
    }

    public void SetPower(int pwr)
    {
        turnPlayer.SetPower(pwr);
    }

    public void AddCurrency(int curr)
    {
        turnPlayer.AddCurrency(curr);
    }

    public void SubtractCurrency(int curr)
    {
        turnPlayer.SubtractCurrency(curr);
    }

    //TODO: Need to access Player's currency and set it????
    public void SetCurrency(int curr)
    {
        turnPlayer.SetCurrency(curr);
    }
}
