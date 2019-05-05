using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCard : Card
{
    //public int cardCost;
    //public int cardCurrency;
    //public int cardAttack;

    //All this stuff below belongs in Player Card Class
    //========================================================
    public bool inShop = false;
    public int cardCost;
    public int cardCurrency;
    public int cardAttack;
    private Vector2 spotOnGrid;

    public OnPurchaseEffectBase effect;

    //When purchasing card from shop call this method from an event trigger
    public void PurchaseCard()
    {
        Player player = FindObjectOfType<TurnManager>().turnPlayer;

        if (cardCost <= player.GetCurrency())
        {
            player.SubtractCurrency(cardCost);
            player.AddToPlayerGraveyard(this);
        }

        effect?.Trigger(this);
        inShop = false;
    }

    private void Awake()
    {
        Debug.Log("Purchasing Card with Effect.");
        effect?.Trigger(this);
    }

    public void SetCoord(Vector2 newSpot)
    {
        this.transform.position = newSpot;
    }
}
