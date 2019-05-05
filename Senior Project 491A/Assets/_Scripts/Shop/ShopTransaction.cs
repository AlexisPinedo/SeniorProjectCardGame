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
    public ShopDeck shopDeck;
    public GameObject parentObject;

    [SerializeField]
    private Vector2 spot = new Vector2();
    private bool initialDeal = true;
    
    public List<PlayerCard> cardsInShop;
    Dictionary<PlayerCard, Vector2> cardPositionMap = new Dictionary<PlayerCard, Vector2>();

    //Make sure to separate this script into a Player Card component not Card, still need to implement a proper deck class for Player and for Enemy
    void Awake()
    {
        //Deal 5 cards and save their locations
        for(shopItems = 0; shopItems < MAX_SHOP_ITEMS; shopItems++)
        {
            shopDeck.Shuffle();
            PlayerCard shopCard = (PlayerCard)shopDeck.DrawCard();
            Vector2 cardPosition = shopGrid.GetNearestPointOnGrid(spot);
            shopCard.transform.position = cardPosition;
            shopCard.inShop = true;
            cardsInShop.Add(shopCard);
            cardPositionMap.Add(shopCard, cardPosition);
            Instantiate(shopCard, parentObject.transform);
            spot.x += 2.0f;
        }
        initialDeal = false;
    }

    void Update()
    {
        //If cards are delt
        if(!initialDeal)
        {
            //Cycle though all cards in the shop
            foreach (PlayerCard availableCard in cardsInShop)
            {
                //If a card is no longer in the shop, it is purchased
                if(!availableCard.inShop)
                {
                    Vector2 availablePosition = cardPositionMap[availableCard];
                    PlayerCard shopCard = (PlayerCard)shopDeck.DrawCard();
                    shopCard.transform.position = availablePosition;
                    shopCard.inShop = true;
                    cardsInShop.Add(shopCard);
                    Instantiate(shopCard, parentObject.transform);
                }
            }
        }

        // if(cardsDelt && shopItems < MAX_SHOP_ITEMS)
        // {
        //     PlayerCard shopCard = (PlayerCard)shopDeck.DrawCard();
        //     Vector2 cardPosition = shopGrid.GetNearestPointOnGrid(new Vector2());
        //     shopCard.transform.position = cardPosition;
        //     shopItems++;
        // }

        
    }

}
