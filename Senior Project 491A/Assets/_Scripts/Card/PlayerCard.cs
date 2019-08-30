using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCard : Card
{

    //All this stuff below belongs in Player Card Class
    //========================================================
    public bool inShop = true;
    public int cardCost;
    public int cardCurrency;
    public int cardAttack;

    private void Awake()
    {
        cardEffect = GetComponent<CardEffect>();
    }

    //This Enum has a reference for which attribute the card is. 
    public enum CardTypes
    {
        MythicalCreature = 0,
        Entertainer = 1,
        Magic = 2,
        Nature = 3,
        Warrior = 4,
        None = 5
    }

    //Saves the type the card is
    public CardTypes cardType;

    //Should this be on Player card???
    public Vector2 spotOnGrid;


    //When purchasing card from shop call this method from an event trigger
    public bool PurchaseCard()
    {
        Player player = TurnManager.Instance.turnPlayer;

        if (cardCost <= player.GetCurrency())
        {
            player.SubtractCurrency(cardCost);
            inShop = false;

            return true;
        }
        else
        {
            //Debug.Log("Cannot buy too broke");

        }
        return false;
    }

    public void SetCoord(Vector2 newSpot)
    {
        this.transform.position = newSpot;
    }
}
