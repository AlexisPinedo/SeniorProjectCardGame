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
    Dictionary<Vector2, PlayerCard> cardPositionMap = new Dictionary<Vector2, PlayerCard>();

    //Make sure to separate this script into a Player Card component not Card, still need to implement a proper deck class for Player and for Enemy
    void Start()
    {
        //Deal 5 cards and save their locations
        for(shopItems = 0; shopItems < MAX_SHOP_ITEMS; shopItems++)
        {
            Debug.Log("Init cards delt");
            shopDeck.Shuffle();
            PlayerCard shopCard = (PlayerCard)shopDeck.DrawCard();
            Vector2 cardPosition = shopGrid.GetNearestPointOnGrid(spot);
            shopCard.transform.position = cardPosition;
            shopCard.inShop = true;
            cardsInShop.Add(shopCard);
            cardPositionMap.Add(cardPosition, shopCard);
            Instantiate(shopCard, parentObject.transform);
            spot.x += 2.0f;
        }
        initialDeal = false;
    }

    private void OnEnable()
    {
        DragCard.CardPurchased += ManagePurchase;
    }

    private void OnDisable()
    {
        DragCard.CardPurchased -= ManagePurchase;
    }

    void ManagePurchase(PlayerCard cardBought)
    {
        Debug.Log("Event launched" + shopDeck.GetDeckSize());

        PlayerCard found = cardsInShop.Find(i => i.spotOnGrid == cardBought.spotOnGrid);

        Debug.Log("Dealing new card to shop");
        PlayerCard nextShopCard = (PlayerCard)shopDeck.DrawCard();

        nextShopCard.gameObject.transform.position = found.gameObject.transform.position;
        Instantiate(nextShopCard, parentObject.transform);
        nextShopCard.inShop = true;
        cardsInShop.Add(nextShopCard);

        cardsInShop.Remove(found);
        
        //cardsInShop.Remove(cardBought);
        Destroy(cardBought.gameObject);
        Debug.Log("Destroyed card bought");



        //If cards are delt
        //if(!initialDeal)
        //{
        ////Cycle though all cards in the shop
        //foreach (PlayerCard availableCard in cardsInShop)
        //{
        //    //Debug.Log(availableCard.inShop);
        //    //If a card is no longer in the shop, it is purchased
        //    if(!availableCard.inShop)
        //    {
        //        Debug.Log(availableCard.inShop);
        //        Debug.Log("Found purchased card");
        //foreach(var pair in cardPositionMap)
        //{
        //    Debug.Log("Searching for card pair");
        //    if(pair.Value == cardBought)
        //    {
        //        Debug.Log("Dealt new card to shop");
        //        PlayerCard shopCard = (PlayerCard)shopDeck.DrawCard();
        //        shopCard.transform.position = pair.Key;
        //        shopCard.inShop = true;
        //        cardsInShop.Add(shopCard);
        //    }
        //}

        // }
        //}
        //}
    }
}
