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
                spot.x += 2.0f;
            }
        }
        cardsDelt = true;
    }

    // Update is called once per frame
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

    void PurchaseItem(Card card)
    {
        if (shop.Contains(card))
        {
            
            shopItems--;
            shop.Remove(card);

        }
    }
}
