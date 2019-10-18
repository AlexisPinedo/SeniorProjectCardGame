using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Photon.Pun;


public class PurchaseHandler : MonoBehaviourPunCallbacks
{
    private static PurchaseHandler _instance;

    public delegate void _CardPurchased(PlayerCardDisplay cardBought);

    public static event _CardPurchased CardPurchased;

    public static PurchaseHandler Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void OnEnable()
    {
        DragCard.ShopCardClicked += HandlePurchase;
        FreeShopSelectionEvent.PurchaseEventTriggered += UnSubHandlePurchase;
        FreeShopSelectionEvent.PurchaseEventEnded += SubHandlePurchase;
    }

    private void OnDisable()
    {
        DragCard.ShopCardClicked -= HandlePurchase;
        FreeShopSelectionEvent.PurchaseEventTriggered -= UnSubHandlePurchase;
        FreeShopSelectionEvent.PurchaseEventEnded += SubHandlePurchase;
    }

    private void UnSubHandlePurchase()
    {
        DragCard.ShopCardClicked -= HandlePurchase;

    }

    private void SubHandlePurchase()
    {
        DragCard.ShopCardClicked += HandlePurchase;
    }
    
    private void HandlePurchase(PlayerCardDisplay cardSelected)
    {
        //Debug.Log("Handling Purchase");
        if (TurnManager.Instance.turnPlayer.Currency >= cardSelected.card.CardCost)
        {
            TurnManager.Instance.turnPlayer.graveyard.graveyard.Add(cardSelected.card);

            TurnManager.Instance.turnPlayer.Currency -= cardSelected.card.CardCost;
            
            cardSelected.TriggerCardPurchasedEvent();
            
            Destroy(cardSelected.gameObject);
        }
        else
        {
            Debug.Log("Cannot purchase. Not enough currency");
        }
    }
}




//public class PurchaseHandler : MonoBehaviour
//{
//    public delegate void _CardBought(Card cardBuying);
//    public static event _CardBought CardBought;
//
//    private Player turnPlayer;
//
//    public void Start()
//    {
//        turnPlayer = TurnManager.Instance.turnPlayer;
//    }
//
//    public bool isPurchasable(Card cardClicked)
//    {
//        bool canBePurchased;
//        // TODO
//
//        canBePurchased = true;
//
//        return canBePurchased;
//    }
//
//    public void PurchaseCard(Card cardBuying)
//    {
//        CardBought?.Invoke(cardBuying);
//    }
//
//}
