using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using Photon.Pun;

//using UnityEditorInternal;

/// <summary>
/// The container object for the Shop, which itself can contain PlayerCardContainers.
/// </summary>
public class ShopContainer : PlayerCardContainer
{
    private static ShopContainer _instance;

    // Singleton pattern
    public static ShopContainer Instance
    {
        get { return _instance; }
    }
    public ShopDeck shopDeck;

    public delegate void _cardDrawnLocationCreated(PlayerCardDisplay cardDrawn, Vector3 freeSpot);

    public static event _cardDrawnLocationCreated CardDrawnLocationCreated;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            //Debug.Log("\tDestroying ShopContainer");
            Destroy(this);
        }
        else
        {
            //Debug.Log("\tShopContainer Instance = this");
            _instance = this;
        }

        //else
        //Debug.Log("attempted to shuffle but still waiting for instance of random value...");
    }

    /// <summary>
    /// Maximum number of cards in the shop at any given time.
    /// </summary>
    private int shopCardCount = 6;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("trying to shuffle deck");
        shopDeck.cardsInDeck = ShuffleDeck.Shuffle(shopDeck);
        DrawStartingHand();
    }

    private void OnEnable()
    {
        PlayerCardDisplay.CardPurchased += DisplayNewCard;
    }

    private void OnDisable()
    {
        PlayerCardDisplay.CardPurchased -= DisplayNewCard;
    }
    

    /// <summary>
    /// Initializes the Shop's card's placements.
    /// </summary>
    protected override void DrawStartingHand()
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

        // PlayerCardDisplay
        display.card = cardDrawn;

        //PlayerCardDisplay cardDisplay = Instantiate(display, containerCardGrid.freeLocations.Pop(), Quaternion.identity, this.transform);
        PlayerCardDisplay cardDisplay = Instantiate(display, spawnPostion.transform.position, Quaternion.identity, this.transform);
        
        NetworkIDAssigner.AssignID(cardDisplay.gameObject.GetPhotonView());

        cardDisplay.enabled = true;

        Vector3 finalCardDestination = containerCardGrid.freeLocations.Pop();

        // Trasnforms the card position to the grid position
        //StartCoroutine(TransformCardPosition(cardDisplay, finalCardDestination));
        //AnimationManager.SharedInstance.PlayAnimation(cardDisplay, finalCardDestination, 0.3f, storeOriginalPosition: true, isShopCard: true);
        ShopAnimationManager.SharedInstance.PlayAnimation(cardDisplay, finalCardDestination, 0.3f, storeOriginalPosition: true);

        Vector3 freeSpot = finalCardDestination;
        
        CardDrawnLocationCreated?.Invoke(cardDisplay, freeSpot);
        
        if (!containerCardGrid.cardLocationReference.ContainsKey(freeSpot))
            containerCardGrid.cardLocationReference.Add(freeSpot, cardDisplay);
        else
            containerCardGrid.cardLocationReference[freeSpot] = cardDisplay;
    }

  

    private void DisplayNewCard(PlayerCardDisplay cardBought)
    {
        Vector3 freeSpot = cardBought.GetComponent<CardZoomer>().originalPosition;

        containerCardGrid.freeLocations.Push(freeSpot);
        HandleDisplayOfACard();
    }


}
