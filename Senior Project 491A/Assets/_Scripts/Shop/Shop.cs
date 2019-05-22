using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private int MAX_SHOP_ITEMS = 6;
    public ShopDeck shopDeck;
    public List<PlayerCard> cardsInShop;

    public void OnEnable()
    {
        PurchaseHandler.CardBought += HandleCardPurchased;
    }

    public void OnDisable()
    {
        PurchaseHandler.CardBought -= HandleCardPurchased;
    }

    private void HandleCardPurchased(Card cardPurchased)
    {
        // TODO
        cardsInShop.Remove((PlayerCard)cardPurchased);
    }

}
