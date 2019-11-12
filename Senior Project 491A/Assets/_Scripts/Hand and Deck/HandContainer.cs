using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using Photon.Pun;
/// <summary>
/// 
/// </summary>
public class HandContainer : PlayerCardContainer
{
    public Hand hand;
    public PlayerDeck playerDeck;
    public PlayerGraveyard playerGraveyard;
    public PlayerCard phantomCard;
    public PlayerCard cardDrawn;

    [SerializeField] private int DefaultHandSize = 5;
    
    private void Awake()
    {
        DrawStartingHand();
    }

    private void OnEnable()
    {
        TurnPhaseManager.PlayerTurnEnded += DestroyHand;
        TurnPhaseManager.PlayerTurnStarted += DrawStartingHand;
        containerGrid.onGridResize += ChangeCardPositions;
    }

    private void OnDisable()
    {
        //Debug.Log("Hand container has been disabled");
        TurnPhaseManager.PlayerTurnEnded -= DestroyHand;
        TurnPhaseManager.PlayerTurnStarted -= DrawStartingHand;
        containerGrid.onGridResize -= ChangeCardPositions;
    }


    protected override void DrawStartingHand()
    {
        for (int i = 0; i < DefaultHandSize; i++)
        {
            DrawCard();
        }
    }

    public void DrawCard()
    {
        cardDrawn = null;

        if (playerDeck.cardsInDeck.Count > 0)
        {
            cardDrawn = (PlayerCard)playerDeck.cardsInDeck.Pop();
        }
        else
        {
            if (playerGraveyard.graveyard.Count > 0)
            {
                for (int j = 0; j < playerGraveyard.graveyard.Count; j++)
               {
                   playerDeck.cardsInDeck.Push(playerGraveyard.graveyard[j]);
                   playerGraveyard.graveyard.Remove(playerGraveyard.graveyard[j]);
               }

               playerDeck.cardsInDeck = ShuffleDeck.Shuffle(playerDeck);
               
               cardDrawn = (PlayerCard)playerDeck.cardsInDeck.Pop();
            }
            else
            {
                // Draw a phantom card
                cardDrawn = phantomCard;
            }
        }

        if (cardDrawn == null)
        {
            Debug.Log("null card");
            return;
        }

        // Set the PlayerCardContainer's PlayerCardDisplay to the cardDrawn
        display.card = cardDrawn;

        if (containerGrid.freeLocations.Count == 0)
        {
            //Debug.Log("Stack is empty ");
            return;
        }

        // Place it on the grid!
        //PlayerCardDisplay cardDisplay = Instantiate(display, containerGrid.freeLocations.Pop(), Quaternion.identity, this.transform);
        PlayerCardDisplay cardDisplay = Instantiate(display, spawnPostion.transform.position, Quaternion.identity, this.transform);

        PhotonView cardDisplayPhotonView = cardDisplay.gameObject.GetPhotonView();
        if (cardDisplayPhotonView.ViewID == 0)
            cardDisplayPhotonView.ViewID = CardDisplay.photonIdCounter++;
        else
            Debug.Log("Already has an assigned ID");

        cardDisplayPhotonView.TransferOwnership(NetworkOwnershipTransferManger.currentPhotonPlayer);

        Vector3 tempCardDestination = containerGrid.freeLocations.Pop();

        // Trasnforms the card position to the grid position
        StartCoroutine(TransformCardPosition(cardDisplay, tempCardDestination));

        if (!containerGrid.cardLocationReference.ContainsKey(tempCardDestination))
            containerGrid.cardLocationReference.Add(tempCardDestination, cardDisplay);
        else
            containerGrid.cardLocationReference[tempCardDestination] = cardDisplay;
    
        hand.hand.Add(cardDrawn);
    }


    public void DrawExtraCard()
    {
        containerGrid.xValUnits += 1;
        DrawCard();
    }

    private void DestroyHand()
    {
        foreach (var locationReferenceKeyValuePair in containerGrid.cardLocationReference)
        {
            if (locationReferenceKeyValuePair.Value != null)
            {
                PlayerCardDisplay cardDisplay = (PlayerCardDisplay)locationReferenceKeyValuePair.Value;

                if (cardDisplay.card == null)
                {
                    Debug.Log("No card in Hand Display game object");
                    return;
                }
                
                if(cardDisplay.card.CardType != CardTypes.None)
                {                    
                    Debug.Log("Add card to grave");
                    playerGraveyard.graveyard.Add(cardDisplay.card);
                }

                Destroy(locationReferenceKeyValuePair.Value.gameObject);
            }
            containerGrid.freeLocations.Push(locationReferenceKeyValuePair.Key);
            
        }
    }

    private void ChangeCardPositions()
    {
        for (int i = containerGrid.cardLocationReference.Count - 1; i > 0 ; i++)
        {
            if(containerGrid.freeLocations.Count == 0)
                break;
            
            Vector2 newLocation = containerGrid.freeLocations.Pop();

            var location = containerGrid.cardLocationReference.ElementAt(i);

            CardDisplay locationElement = location.Value;
                
            locationElement.gameObject.transform.position = newLocation;
            
            var oldLocation = location.Key;

            containerGrid.cardLocationReference.Remove(oldLocation);
            containerGrid.cardLocationReference.Add(newLocation, locationElement);
        }
    }

    /// <summary>
    /// Places a playerGraveyard's card onto the hand grid.
    /// </summary>
    /// <param name="card"></param>
    private void PlaceCard(PlayerCard cardToPlace)
    {
        //Vector2 spawnPoint;
        //spawnPoint = handGrid.GetNearestPointOnGrid(cardSpot);

        //if (handGrid.IsPlaceable(spawnPoint))
        //{
        //    card.SetCoord(spawnPoint);
        //    hand.Add(card);

        //    inHandObjects.Add(Instantiate(card, this.transform).gameObject);
        //    cardsInHand += 1;
        //    cardSpot.x += handGrid.size;
        //}
    }
}
