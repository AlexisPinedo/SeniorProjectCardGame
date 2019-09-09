using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shop : MonoBehaviourPun
{
    private int MAX_SHOP_ITEMS = 6;
    public ShopDeck shopDeck;
    public List<PlayerCard> cardsToPlaceInShopDeck;

    [SerializeField]
    private readonly PhotonView camPhotonView;

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
        cardsToPlaceInShopDeck.Remove((PlayerCard)cardPurchased);
    }
    
    void PlayShopCard()
    {
        shopDeck.Shuffle();
        PlayerCard shopCard = (PlayerCard)shopDeck.DrawCard();
        
       //shopCard.transform.position = shopGrid.GetNearestPointOnGrid(spawnPoint);
    }

    /*
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
     * 
     */

}
