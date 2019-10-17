using System;
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

    public bool inShopSelectionState = false;

    private int cardsPurchased;

    private int cardsToPurchase;

    private CardType.CardTypes compareType = CardType.CardTypes.All;
    
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

        cardsToPurchase = cardsToSelect;
        
        GameEventManager.Instance.AddStateToQueue(this);
    }

    public override void EventState()
    {
        Debug.Log("In Free shop cards event");
        this.enabled = true;
        
        PurchaseEventTriggered?.Invoke();
        
        inShopSelectionState = true;
    }

    public void DisableShopSelectionState()
    {
        //ButtonInputManager.DisableButtonsInList();
        compareType = CardType.CardTypes.All;
        
        PurchaseEventEnded?.Invoke();

        this.enabled = false;
        
        GameEventManager.Instance.EndEvent();
    }

    private void FreeCardPurchase(PlayerCardDisplay cardClicked)
    {
        return;
        if (!ValidateSameCostRequirement())
        {
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
            return true;
        
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
