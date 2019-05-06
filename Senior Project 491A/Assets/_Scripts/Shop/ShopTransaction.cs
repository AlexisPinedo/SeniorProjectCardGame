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
    public TurnManager turnPlayer;
    public Player currentPlayer;

    void Start()
    {
        //Deal 5 cards and save their locations
        for (shopItems = 0; shopItems < MAX_SHOP_ITEMS; shopItems++)
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
            //Increment next spot position
            spawnPoint.x += 2.0f;
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
        currentPlayer = turnPlayer.turnPlayer;
        //Find the card in cardsInShop in the list that matches cardBought on the grid
        PlayerCard found = cardsInShop.Find(i => i.spotOnGrid == cardBought.spotOnGrid);
        //Add purchase to player graveyard
        currentPlayer.AddToPlayerGraveyard(found);
        //This spot is now open
        shopGrid.SetObjectPlacement(cardBought.spotOnGrid, false);
        //Destroy and remove the purchased card
        Destroy(cardBought.gameObject);
        cardsInShop.Remove(found);
        //Draw a new card from the shopDeck
        PlayerCard nextShopCard = (PlayerCard)shopDeck.DrawCard();
        //Check if the position is avaliable
        if (shopGrid.IsPlaceable(cardBought.spotOnGrid))
        {
            //Find the nearest avaliable spot and move it there
            nextShopCard.transform.position = cardBought.spotOnGrid;
            nextShopCard.spotOnGrid = cardBought.spotOnGrid;
            //Add to shop
            nextShopCard.inShop = true;
            cardsInShop.Add(nextShopCard);
            //Set map boolean
            shopGrid.SetObjectPlacement(nextShopCard.transform.position, true);
            Instantiate(nextShopCard, parentObject.transform);
        }
    }
}
