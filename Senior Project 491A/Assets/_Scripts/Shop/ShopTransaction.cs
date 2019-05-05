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
    public List<PlayerCard> cardsInShop;
  
   
     void Start()
    {
        //Deal 5 cards and save their locations
        for(shopItems = 0; shopItems < MAX_SHOP_ITEMS; shopItems++)
        {
            Debug.Log("Init cards delt");
            shopDeck.Shuffle();
            //Draw a card
            PlayerCard shopCard = (PlayerCard)shopDeck.DrawCard();
            //Find nearest avaliable spot and move it there
            shopCard.transform.position = shopGrid.GetNearestPointOnGrid(spawnPoint);
            shopCard.spotOnGrid = shopCard.transform.position;
            //Add to shop
            shopCard.inShop = true;
            cardsInShop.Add(shopCard);
            //Set spot map boolean
            shopGrid.SetObjectPlacement(shopCard.transform.position, true);
            Instantiate(shopCard, parentObject.transform);
            //Increment next spot position.
            spawnPoint.x += 2.0f;
        }

        foreach (var pair in shopGrid.objectPlacements)
        {
            Debug.Log(pair.Key + " = " + pair.Value);
        }

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

        //Find clicked on card with the cardsInShop
        PlayerCard found = cardsInShop.Find(i => i.spotOnGrid == cardBought.spotOnGrid);

        shopGrid.SetObjectPlacement(cardBought.spotOnGrid, false);
        Debug.Log("Setting false at purchased card " + cardBought + " at location " + cardBought.spotOnGrid);
        Destroy(cardBought.gameObject);
        cardsInShop.Remove(found);
        Debug.Log("Destroyed card bought");

        ////Find nearest point on the grid.
        //var location = shopGrid.GetNearestPointOnGrid(cardBought.spotOnGrid);
        PlayerCard nextShopCard = (PlayerCard)shopDeck.DrawCard();

        if (shopGrid.IsPlaceable(cardBought.spotOnGrid))
        {
            nextShopCard.transform.position = cardBought.spotOnGrid;
            nextShopCard.spotOnGrid = cardBought.spotOnGrid;
            nextShopCard.inShop = true;
            cardsInShop.Add(nextShopCard);
            shopGrid.SetObjectPlacement(nextShopCard.transform.position, true);
            Instantiate(nextShopCard, parentObject.transform);
        }
    }
}
