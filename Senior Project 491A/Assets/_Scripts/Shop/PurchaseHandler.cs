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

    public Transform GraveyardPosition;

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
        if (TurnPlayerManager.Instance.TurnPlayer.Currency >= cardSelected.card.CardCost)
        {
            //StartCoroutine(TransformCardPosition(cardSelected, GraveyardPosition.position));
            AnimationManager.SharedInstance.PlayAnimation(cardSelected, GraveyardPosition.position, 0.5f, storeOriginalPosition: true,shouldDestroy: true);

            TurnPlayerManager.Instance.TurnPlayer.playerGraveyard.graveyard.Add(cardSelected.card);

            TurnPlayerManager.Instance.TurnPlayer.Currency -= cardSelected.card.CardCost;
            
            cardSelected.TriggerCardPurchasedEvent();
            
            
        }
        else
        {
            Debug.Log("Cannot purchase. Not enough currency");
        }
    }

    //IEnumerator TransformCardPosition(PlayerCardDisplay cardDisplay, Vector3 cardDestination)
    //{
    //    float currentLerpTime = 0;
    //    float lerpTime = 0.5f;

    //    Vector3 startPos = cardDisplay.transform.position;

    //    while (cardDisplay.transform.position != cardDestination)
    //    {
    //        currentLerpTime += Time.deltaTime;
    //        if (currentLerpTime >= lerpTime)
    //        {
    //            currentLerpTime = lerpTime;
    //        }

    //        float Perc = currentLerpTime / lerpTime;

    //        cardDisplay.transform.position = Vector3.Lerp(startPos, cardDestination, Perc);


    //        yield return new WaitForEndOfFrame();
    //    }

    //    cardDisplay.GetComponent<CardZoomer>().OriginalPosition = cardDestination;
    //    cardDisplay.GetComponent<DragCard>().OriginalPosition = cardDestination;

    //    Destroy(cardDisplay.gameObject);
    //}
}

