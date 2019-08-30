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

    //Should this be on Player card???
    public Vector2 spotOnGrid;


    //When purchasing card from shop call this method from an event trigger
    public bool PurchaseCard()
    {
        Player player = TurnManager.turnPlayer;

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
