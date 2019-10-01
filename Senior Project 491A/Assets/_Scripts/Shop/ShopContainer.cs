using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// The container object for the Shop, which itself can contain PlayerCardContainers.
/// </summary>
public class ShopContainer : PlayerCardContainer
{
    // Singleton pattern
    public static ShopContainer Instance { get; set; } = new ShopContainer();
    static ShopContainer() { }
    private ShopContainer() { }
    public ShopDeck shopDeck;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("\tDestroying ShopContainer");
            Destroy(this);
        }
        else
        {
            Debug.Log("\tShopContainer Instance = this");
            Instance = this;
        }
    }

    /// <summary>
    /// Maximum number of cards in the shop at any given time.
    /// </summary>
    private int shopCardCount = 6;


    //public void SerializeState (PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(shopDeck);
    //    }
    //    else
    //    {
    //        shopDeck = (ShopDeck)stream.ReceiveNext();
    //    }
    //}

    //public void OnPhotonSerializeView( PhotonStream stream, PhotonMessageInfo info)
    //{
    //    Debug.Log("... i think its syncing?");
    //    SerializeState(stream, info);
    //}

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
            raiseEventOptions.CachingOption = EventCaching.DoNotCache;
            raiseEventOptions.Receivers = ReceiverGroup.All;
            SendOptions sendOptions = new SendOptions();

            object content = shopDeck;
            //.GetHashCode();

            PhotonNetwork.RaiseEvent(0, content, raiseEventOptions, sendOptions);


            Debug.Log("Raise event sent..");
        }
        InitialCardDisplay();
    }

    private void OnEnable()
    {
        PurchaseHandler.CardPurchased += DisplayNewCard;
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PurchaseHandler.CardPurchased -= DisplayNewCard;
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        Debug.Log("Event recieved...");
        photonEvent.Code = 0;

        //myObject = photonEvent.CustomData;
        //shopDeck = (ShopDeck) myObject;
    }

    /// <summary>
    /// Initializes the Shop's card's placements.
    /// </summary>
    protected override void InitialCardDisplay()
    {
        for (int i = 0; i < shopCardCount; i++)
        {
            if (shopDeck.cardsInDeck.Count <= 0)
            {
                Debug.Log("Shop deck is " + shopDeck.cardsInDeck.Count);
                return;
            }

            HandleDisplayOfACard();

            // Draw a Card from the ShopDeck
        }

    }

    private void HandleDisplayOfACard()
    {
        PlayerCard cardDrawn = null;
        cardDrawn = (PlayerCard)shopDeck.cardsInDeck.Pop();

        // PlayerCardHolder
        holder.card = cardDrawn;

        PlayerCardHolder cardHolder = Instantiate(holder, containerGrid.freeLocations.Pop(), Quaternion.identity, this.transform);
        cardHolder.enabled = true;

        Vector3 freeSpot = cardHolder.gameObject.transform.position;

        if (!containerGrid.cardLocationReference.ContainsKey(freeSpot))
            containerGrid.cardLocationReference.Add(freeSpot, cardHolder);
        else
            containerGrid.cardLocationReference[freeSpot] = cardHolder;
    }

    private void DisplayNewCard(PlayerCardHolder cardBought)
    {
        Vector3 freeSpot = cardBought.gameObject.transform.position;

        containerGrid.freeLocations.Push(freeSpot);
        HandleDisplayOfACard();
    }
}
