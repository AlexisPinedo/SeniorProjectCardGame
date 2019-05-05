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
    private Vector2 spawnPoint = new Vector2();
    private bool initialDeal = true;
    
    public List<PlayerCard> cardsInShop;
   //Dictionary<Vector2, PlayerCard> cardPositionMap = new Dictionary<Vector2, PlayerCard>();

    //Make sure to separate this script into a Player Card component not Card, still need to implement a proper deck class for Player and for Enemy
    void Start()
    {
        //Deal 5 cards and save their locations
        for(shopItems = 0; shopItems < MAX_SHOP_ITEMS; shopItems++)
        {
            Debug.Log("Init cards delt");
            shopDeck.Shuffle();
            PlayerCard shopCard = (PlayerCard)shopDeck.DrawCard();
            Vector2 cardPosition = shopGrid.GetNearestPointOnGrid(spawnPoint);
            shopCard.transform.position = cardPosition;
            shopCard.spotOnGrid = cardPosition;
            shopCard.inShop = true;
            cardsInShop.Add(shopCard);
            shopGrid.SetObjectPlacement(cardPosition, true);
            Instantiate(shopCard, parentObject.transform);
            spawnPoint.x += 2.0f;
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

        Debug.Log("Deck cards remaining: " + shopDeck.GetDeckSize());

        PlayerCard found = cardsInShop.Find(i => i.spotOnGrid == cardBought.spotOnGrid);

        Destroy(cardBought.gameObject);
        Debug.Log("Destroyed card bought");

        var location = shopGrid.GetNearestPointOnGrid(spawnPoint);

        if (shopGrid.IsPlaceable(location))
        {
            PlayerCard nextShopCard = (PlayerCard)shopDeck.DrawCard();
            nextShopCard.gameObject.transform.position = location;
            nextShopCard.spotOnGrid = location;
            nextShopCard.inShop = true;
            cardsInShop.Add(nextShopCard);
        }

        cardsInShop.Remove(found);


        //Debug.Log("Deck cards remaining: " + shopDeck.GetDeckSize());

      
        //foreach(var pair in cardPositionMap){
        //    Debug.Log("Searching for card in cardPositionMap...");
        //    if(pair.Value == found)
        //    {
        //        Debug.Log("MATCH" + found + " == " + pair.Value);
        //        PlayerCard nextShopCard = (PlayerCard)shopDeck.DrawCard();
        //        Debug.Log("DEALING" + nextShopCard + "IN SPOT OF" + found);
        //        Vector2 availablePosition = pair.Key;
        //        nextShopCard.gameObject.transform.position = availablePosition;
        //        cardPositionMap.Remove(pair.Key);
        //        cardPositionMap.Add(availablePosition, nextShopCard);
        //        Instantiate(nextShopCard, parentObject.transform);
        //        nextShopCard.inShop = true;
        //        cardsInShop.Add(nextShopCard);
        //        cardsInShop.Remove(found);
        //        break;
        //    }
        //    else
        //    {
        //        Debug.Log("NOT A MATCH.");
        //    }
        //}


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
