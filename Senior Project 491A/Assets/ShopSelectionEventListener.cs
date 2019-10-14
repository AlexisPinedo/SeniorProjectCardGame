using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSelectionEventListener : MonoBehaviour
{
    public delegate void _purchaseEventTriggered();

    public static event _purchaseEventTriggered PurchaseEventTriggered;

    public delegate void _purchaseEventEnded();

    public static event _purchaseEventEnded PurchaseEventEnded;

    private static ShopSelectionEventListener _instance;

    public static ShopSelectionEventListener Instance
    {
        get { return _instance; }
    }

    public bool inShopSelectionState = false;

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
        DragCard.ShopCardClicked += CardPurchase;
    }

    private void OnDisable()
    {
        DragCard.ShopCardClicked -= CardPurchase;
    }

    public void EnableShopSelectionState(int cardsToSelect)
    {
        StartCoroutine(SelectionState(cardsToSelect));
    }
    
    public void EnableShopSelectionState(int cardsToSelect, CardType.CardTypes cardTypeToCompare)
    {
        StartCoroutine(SelectionState(cardsToSelect));
    }

    public void DisableShopSelectionState()
    {
        inShopSelectionState = false;
    }
    
    private void CardPurchase(PlayerCardDisplay cardClicked)
    {vcb
        TurnManager.Instance.turnPlayer.graveyard.graveyard.Add(cardClicked.card);
            
        cardClicked.TriggerCardPurchasedEvent();
            
        Destroy(cardClicked.gameObject);
        
    }
    
    private void CardPurchase(PlayerCardDisplay cardClicked,CardType.CardTypes lastCardPlayedType)
    {

        if (cardClicked.card.CardType == CardType.CardTypes.All || cardClicked.card.CardType == lastCardPlayedType)
        {
            TurnManager.Instance.turnPlayer.graveyard.graveyard.Add(cardClicked.card);
            
            cardClicked.TriggerCardPurchasedEvent();
            
            Destroy(cardClicked.gameObject);
        }
    }


    IEnumerator SelectionState(int cardsToSelect)
    {
        while (NotificationWindow.Instance.inNotificationState)
        {
            yield return null;
        }

        inShopSelectionState = true;
        
        PurchaseEventTriggered?.Invoke();
        Debug.Log("Purchase event triggered");

        while (inShopSelectionState)
        {
            yield return null;
        }
        
        Debug.Log("Purchase event ended");

        PurchaseEventEnded.Invoke();
    }
}
