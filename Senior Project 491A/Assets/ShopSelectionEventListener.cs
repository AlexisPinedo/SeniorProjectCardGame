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

    private int cardsPurchased;

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
        DragCard.ShopCardClicked += CardPurchase;
    }

    private void OnDisable()
    {
        DragCard.ShopCardClicked -= CardPurchase;
    }

    public void EnableShopSelectionState(int cardsToSelect, CardType.CardTypes typeToCompare = CardType.CardTypes.All)
    {
        //ButtonInputManager.DisableButtonsInList();
        compareType = typeToCompare;
        StartCoroutine(SelectionState(cardsToSelect));
    }

    public void DisableShopSelectionState()
    {
        //ButtonInputManager.DisableButtonsInList();
        inShopSelectionState = false;
        compareType = CardType.CardTypes.All;
    }

    private void CardPurchase(PlayerCardDisplay cardClicked)
    {
        if (!inShopSelectionState)
            return;
        
        if (cardClicked.card.CardType == compareType || compareType == CardType.CardTypes.All)
        {
            TurnManager.Instance.turnPlayer.graveyard.graveyard.Add(cardClicked.card);
        
            cardClicked.TriggerCardPurchasedEvent();
        
            Destroy(cardClicked.gameObject);

            cardsPurchased++; 
            
            Debug.Log("card purchased for free");

        }
        else
        {
            Debug.Log("must select card of correct type");
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

        while (cardsPurchased < cardsToSelect)
        {
            yield return null;
        }
        
        Debug.Log("Purchase event ended");

        DisableShopSelectionState();
        
        PurchaseEventEnded.Invoke();
    }
}
