using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTransaction : MonoBehaviour
{
    [SerializeField]
    private int shopItems;
    [SerializeField]
    private int MAX_SHOP_ITEMS = 6;
    [SerializeField]
    private CreateGrid shopGrid;
    [SerializeField]
    public Deck shopDeck;

    [SerializeField]
    private Vector2 spot = new Vector2();
    private bool cardsDelt = false;
    

    //Make sure to separate this script into a Player Card component not Card, still need to implement a proper deck class for Player and for Enemy
    void Start()
    {
        if (!cardsDelt)
        {
            for(shopItems = 0; shopItems < MAX_SHOP_ITEMS; shopItems++)
            {
                shopDeck.Shuffle();
                Card shopCard = shopDeck.DrawCard();
                Vector2 cardPosition = shopGrid.GetNearestPointOnGrid(spot);
                shopCard.transform.position = cardPosition;
                shopCard.inShop = true;
                Instantiate(shopCard);

                spot.x += 2.0f;
            }
        }
        cardsDelt = true;
    }

    void Update()
    {
        if(cardsDelt && shopItems < MAX_SHOP_ITEMS)
        {
            Card shopCard = shopDeck.DrawCard();
            Vector2 cardPosition = shopGrid.GetNearestPointOnGrid(new Vector2());
            shopCard.transform.position = cardPosition;
            shopItems++;
        }
        
    }

    /**
    TODO: Get card gameobject from shop zone
    */
    void PurchaseItem(Card card, Player currentPlayer)
    {
        //if (shop.Contains(card))
        //{
        //    currentPlayer.addToPlayerGraveyard(card);
        //    shopItems--;
        //    shop.Remove(card);
        //}

    }
}
