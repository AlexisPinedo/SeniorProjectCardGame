﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeShopSelectionEvent : Event_Base
{
    public delegate void _purchaseEventTriggered();
    public static event _purchaseEventTriggered PurchaseEventTriggered;

    
    public delegate void _purchaseEventEnded();
    public static event _purchaseEventEnded PurchaseEventEnded;

    private static FreeShopSelectionEvent _instance;

    public static FreeShopSelectionEvent Instance
    {
        get { return _instance; }
    }
    
    private int cardsPurchased;
    
    private Queue<int> cardsToPurchaseQueue = new Queue<int>();

    private CardType.CardTypes compareType = CardType.CardTypes.All;

    private int cardsToPurchase = 0;
    
    private void Awake()
    {
        if (_instance == null && _instance != this)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnEnable()
    {
        DragCard.ShopCardClicked += FreeCardPurchase;
    }

    private void OnDisable()
    {
        DragCard.ShopCardClicked -= FreeCardPurchase;
    }

    public void EnableShopSelectionState(int cardsToSelect, CardType.CardTypes typeToCompare = CardType.CardTypes.All)
    {
        compareType = typeToCompare;
        
        cardsToPurchaseQueue.Enqueue(cardsToSelect);
        
        GameEventManager.Instance.AddStateToQueue(this);
    }

    public override void EventState()
    {
        Debug.Log("In Free shop cards event");
        
        ButtonInputManager.Instance.DisableButtonsInList();
        
        cardsToPurchase = cardsToPurchaseQueue.Dequeue();

        this.enabled = true;
        
        PurchaseEventTriggered?.Invoke();
    }

    public void DisableShopSelectionState()
    {
        compareType = CardType.CardTypes.All;
        
        PurchaseEventEnded?.Invoke();

        this.enabled = false;

        cardsPurchased = 0;
        
        ButtonInputManager.Instance.EnableButtonsInList();
        
        GameEventManager.Instance.EndEvent();
    }

    private void FreeCardPurchase(PlayerCardDisplay cardClicked)
    {
        if (!ValidateSameCostRequirement())
        {
            Debug.Log("could not validate free card purchase must exit");

            DisableShopSelectionState();
            return;
        }
        
        if (cardClicked.card.CardType == compareType || compareType == CardType.CardTypes.All)
        {
            TurnManager.Instance.turnPlayer.graveyard.graveyard.Add(cardClicked.card);
        
            cardClicked.TriggerCardPurchasedEvent();
        
            Destroy(cardClicked.gameObject);

            cardsPurchased++;

            if (cardsPurchased == cardsToPurchase)
            {
                Debug.Log("purchased required amount must exit");

                DisableShopSelectionState();
            }

            //Debug.Log("card purchased for free");
        }
        else
        {
            //Debug.Log("must select card of correct type");
        }
    }
    
    public bool ValidateSameCostRequirement()
    {
        if (compareType == CardType.CardTypes.All)
        {
            Debug.Log("Comparetype is all so you can buy anything");
            return true;
        }
        
        Dictionary<Vector2, CardDisplay> CardsinShop = ShopContainer.Instance.containerGrid.cardLocationReference;

        int cardsOfSameTypeOnField = 0;
        
        foreach (var cardLocationReference in CardsinShop)
        {
            PlayerCardDisplay cardDisplay = (PlayerCardDisplay) cardLocationReference.Value;

            if (cardDisplay.card.CardType == compareType)
                cardsOfSameTypeOnField++;
        }

        if (cardsOfSameTypeOnField >= (cardsToPurchase - cardsPurchased))
        {
            return true;
        }

        return false;
    }
}