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
    
    private void Start()
    {
        if(History.Instance.TurnCount == 0)
            DrawStartingHand();
    }

    private void OnEnable()
    {
        TurnPhaseManager.EndingPlayerTurn += DestroyHand;
        TurnPhaseManager.PlayerTurnStarted += DrawStartingHand;
        containerCardGrid.onGridResize += ChangeCardPositions;
    }

    private void OnDisable()
    {
        //Debug.Log("Hand container has been disabled");
        TurnPhaseManager.EndingPlayerTurn -= DestroyHand;
        TurnPhaseManager.PlayerTurnStarted -= DrawStartingHand;
        containerCardGrid.onGridResize -= ChangeCardPositions;
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
            cardDrawn = playerDeck.cardsInDeck.Pop();
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
               
               cardDrawn = playerDeck.cardsInDeck.Pop();
            }
            else
            {
                // Draw a phantom card
                cardDrawn = phantomCard;
            }
        }

        if (cardDrawn == null)
        {
            //Debug.Log("null card");
            return;
        }

        // Set the PlayerCardContainer's PlayerCardDisplay to the cardDrawn
        display.card = cardDrawn;

        if (containerCardGrid.freeLocations.Count == 0)
        {
            //Debug.Log("Stack is empty ");
            return;
        }

        // Place it on the grid!
        //PlayerCardDisplay cardDisplay = Instantiate(display, containerCardGrid.freeLocations.Pop(), Quaternion.identity, this.transform);
        PlayerCardDisplay cardDisplay = Instantiate(display, spawnPostion.transform.position, Quaternion.identity, this.transform);

        //Debug.Log("Created card " + cardDisplay.card.name);
        
        PhotonView cardDisplayPhotonView = cardDisplay.gameObject.GetPhotonView();

        NetworkIDAssigner.AssignID(cardDisplayPhotonView);

        cardDisplayPhotonView.TransferOwnership(NetworkOwnershipTransferManger.currentPhotonPlayer);

        Vector3 tempCardDestination = containerCardGrid.freeLocations.Pop();

        // Trasnforms the card position to the grid position
        //StartCoroutine(TransformCardPosition(cardDisplay, tempCardDestination));
        //AnimationManager.SharedInstance.PlayAnimation(cardDisplay, tempCardDestination, 0.3f, storeOriginalPosition: true);
        PlayerAnimationManager.SharedInstance.PlayAnimation(cardDisplay, tempCardDestination, 0.3f, storeOriginalPosition: true);

        if (!containerCardGrid.cardLocationReference.ContainsKey(tempCardDestination))
            containerCardGrid.cardLocationReference.Add(tempCardDestination, cardDisplay);
        else
            containerCardGrid.cardLocationReference[tempCardDestination] = cardDisplay;
    
        hand.hand.Add(cardDrawn);
    }


    public void DrawExtraCard()
    {
        containerCardGrid.xValUnits += 1;
        DrawCard();
    }

    private void DestroyHand()
    {
        //Debug.Log("Destroying hand");

        foreach (var locationReferenceKeyValuePair in containerCardGrid.cardLocationReference)
        {
            if (locationReferenceKeyValuePair.Value != null)
            {
                //Debug.Log("card display found");

                PlayerCardDisplay cardDisplay = (PlayerCardDisplay)locationReferenceKeyValuePair.Value;

                if (cardDisplay.card == null)
                {
                    //Debug.Log("No card in Hand Display game object");
                    containerCardGrid.freeLocations.Push(locationReferenceKeyValuePair.Key);
                    continue;
                }
                //Debug.Log("card display card not null");

                if(cardDisplay.card.CardType != CardTypes.None)
                {                    
                    Debug.Log("Add card to grave");
                    playerGraveyard.graveyard.Add(cardDisplay.card);
                }
                //Debug.Log("Removing card from hand");

                hand.hand.Remove(cardDisplay.card);
                Destroy(locationReferenceKeyValuePair.Value.gameObject);
            }
            //Debug.Log("pushing onto free locations" + locationReferenceKeyValuePair.Key);
            containerCardGrid.freeLocations.Push(locationReferenceKeyValuePair.Key);

        }
    }

    private void ChangeCardPositions()
    {
        for (int i = containerCardGrid.cardLocationReference.Count - 1; i > 0 ; i++)
        {
            if(containerCardGrid.freeLocations.Count == 0)
                break;
            
            Vector2 newLocation = containerCardGrid.freeLocations.Pop();

            var location = containerCardGrid.cardLocationReference.ElementAt(i);

            PlayerCardDisplay locationElement = (PlayerCardDisplay)location.Value;
                
            locationElement.gameObject.transform.position = newLocation;
            
            var oldLocation = location.Key;

            containerCardGrid.cardLocationReference.Remove(oldLocation);
            containerCardGrid.cardLocationReference.Add(newLocation, locationElement);
        }
    }

    /// <summary>
    /// Places a playerGraveyard's card onto the hand grid.
    /// </summary>
    /// <param name="card"></param>
    private void PlaceCard(PlayerCard cardToPlace)
    {
        //Vector2 spawnPoint;
        //spawnPoint = handCardGrid.GetNearestPointOnGrid(cardSpot);

        //if (handCardGrid.IsPlaceable(spawnPoint))
        //{
        //    card.SetCoord(spawnPoint);
        //    hand.Add(card);

        //    inHandObjects.Add(Instantiate(card, this.transform).gameObject);
        //    cardsInHand += 1;
        //    cardSpot.x += handCardGrid.size;
        //}
    }
}
