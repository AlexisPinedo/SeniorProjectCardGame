using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTransaction : MonoBehaviour
{
    [SerializeField]
    private List<PlayerCard> shop = new List<PlayerCard>();
    [SerializeField]
    private int shopItems;
    [SerializeField]
    private int maxShopItems = 6;
    [SerializeField]
    private CreateGrid shopGrid;
    [SerializeField]
    public Deck shopDeck;
    [SerializeField]
    private Vector2 spot = new Vector2();
    
    void Start()
    {
        for(shopItems = 0; shopItems < maxShopItems; shopItems++)
        {
            shopDeck.Shuffle();
            Card shopCard = shopDeck.DrawCard();
            Vector2 cardPosition = shopGrid.GetNearestPointOnGrid(spot);
            shopCard.transform.position = cardPosition;
            spot.x += 2.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(shopItems < maxShopItems){
            Card shopCard = shopDeck.DrawCard();
            Vector2 cardPosition = shopGrid.GetNearestPointOnGrid(new Vector2());
            shopCard.transform.position = cardPosition;
        }
        
    }

    public void PurchaseItem(PlayerCard card)
    {
        if (shop.Contains(card))
        {
            shop.Remove(card);
        }
    }
}
