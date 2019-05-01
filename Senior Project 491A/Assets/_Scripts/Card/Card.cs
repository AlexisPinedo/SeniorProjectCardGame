using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public string cardName;
    public string cardDescription;
    public string cardEffect;

    public Sprite cardArtwork;
    public Sprite cardElement;

    //Enemy Card components
    public BossTurnCardPlayer manager;
    public CreateGrid bossZones;

    //All this stuff below belongs in Player Card Class
    //========================================================
    public bool inShop = false;
    public int cardCost;
    public int cardCurrency;
    public int cardAttack;

    //When purchasing card from shop call this method from an event trigger
    void purchaseCard()
    {
        if (inShop)
        {
            Player player = new Player();
            if (cardCurrency < player.getCurrency())
            {
                player.subtractCurrency(cardCurrency);
                player.addToPlayerGraveyard(this);
            }
        }


        inShop = false;
    }
}
