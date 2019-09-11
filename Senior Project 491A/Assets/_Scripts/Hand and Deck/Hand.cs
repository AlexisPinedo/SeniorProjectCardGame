using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines what is part of a Player's Hand.
/// </summary>
[CreateAssetMenu]
public class Hand : ScriptableObject
{
    [SerializeField] public List<PlayerCard> hand;
    //private int cardsInHand = 0;
    //[SerializeField] private CardContainer cardContainter;

    private void Awake()
    {
        hand.Clear();
    }

   
}





//public class Hand : MonoBehavior
    //{
        ///* Hand-specific refereneces*/
        //[SerializeField] private List<PlayerCard> hand;
        //public List<GameObject> inHandObjects;
        //private int cardsInHand = 0;

        ///* Player references */
        //[SerializeField] private GameObject playerObj;
        //[SerializeField] private GameObject playerSpace;
        //private PlayerDeck deck;
        //private Graveyard graveyard;
        //private CreateGrid handGrid;

        ///* Spawn point for card */
        //private Vector2 cardSpot;

        //void Awake()
        //{
        //    // Set references for Deck and Hand Grid
        //    deck = playerObj.GetComponentInChildren<PlayerDeck>();
        //    graveyard = playerObj.GetComponentInChildren<Graveyard>();
        //    handGrid = playerSpace.GetComponentInChildren<CreateGrid>();
        //    cardSpot = new Vector2();
        //}

        ///* Adds a card to the Hand from the Deck.
        //    If there are no cards in the Deck but there is a graveyard then
        //    shuffle the graveyard and put it into the deck.
        //    NOTE: No phantom cards are drawn using this technique
        // */
        ///// <summary>
        ///// Adds a card to the Hand from the Deck. If there are no cards in the Deck but there is a graveyard then
        ///// we shuffle the graveyard and put it into the deck.
        ///// </summary>
        //public void AddCard()
        //{
        //    PlayerCard cardDrawn;
        //    bool deckIsEmpty = deck.GetDeck().Count == 0;
        //    bool graveIsEmpty = graveyard.GetGraveyard().Count == 0;

        //    if (deckIsEmpty && graveIsEmpty)
        //    {
        //        //Debug.Log("No cards to draw");
        //        cardDrawn = null;
        //    }
        //    else
        //    {
        //        if (deckIsEmpty && !graveIsEmpty)
        //        {
        //            graveyard.MoveToDeck(deck);
        //        }

        //        cardDrawn = (PlayerCard)deck.DrawCard();
        //    }

        //    if (cardDrawn != null)
        //    {
        //        if (cardsInHand > 5)
        //        {
        //            handGrid.ResizeGrid(handGrid.size * 0.88f, handGrid.xValUnits + 1);
        //            cardSpot = new Vector2();

        //            foreach (GameObject card in inHandObjects)
        //            {
        //                // Change their coordinates
        //                Vector2 spawnPoint = handGrid.GetNearestPointOnGrid(cardSpot);
        //                card.transform.position = spawnPoint;
        //                cardSpot.x += handGrid.size;
        //            }
        //        }

        //        PlaceCard(cardDrawn);
        //    }
        //}

        ///// <summary>
        ///// Removes a player card if it exists in the Player's Hand
        ///// </summary>
        ///// <param name="cardAffected"></param>
        //public void RemoveCard(PlayerCard cardAffected)
        //{
        //    if (cardAffected != null && hand.Contains(cardAffected))
        //    {
        //        if (cardsInHand > 6)
        //        {
        //            /// TODO: Resize to normal
        //            /// ???
        //            //Debug.Log("RemoveCard, cardsInHand > 6 not finished");
        //            cardsInHand -= 1;
        //        }
        //        else
        //        {
        //            hand.Remove(cardAffected);
        //            cardsInHand -= 1;
        //        }
        //    }
        //}

        ///// <summary>
        ///// A unique draw that is only done at the beginning of the Player's turn.
        ///// It functions similar to AddCard(), except this function initializes 
        ///// phantom cards.
        ///// </summary>
        //public void TurnStartDraw()
        //{
        //    PlayerCard cardDrawn;
        //    cardsInHand = 0;
        //    bool graveyardAdded = false;

        //    while (cardsInHand != 6)
        //    {
        //        // Draw cards from the deck if there are cards in the Deck
        //        if (deck.GetDeck().Count > 0)
        //        {
        //            cardDrawn = (PlayerCard)deck.DrawCard();
        //        }
        //        else
        //        {
        //            if (graveyard.GetGraveyard().Count > 0 && !graveyardAdded)
        //            {
        //                graveyard.MoveToDeck(deck);
        //                graveyardAdded = true;

        //                cardDrawn = (PlayerCard)deck.DrawCard();
        //            }
        //            else
        //            {
        //                // Draw a phantom card
        //                cardDrawn = (PlayerCard)deck.phantomCard;
        //            }
        //        }

        //        if (cardDrawn == null)
        //        {
        //            //Debug.Log("null card");
        //        }

        //        PlaceCard(cardDrawn);
        //    }

        //    foreach (var card in inHandObjects)
        //    {
        //        PlayerCard cardComponent = card.GetComponent<PlayerCard>();
        //        cardComponent.inShop = false;
        //        DragCard component = card.GetComponent<DragCard>();
        //        component.draggable = true;
        //    }
        //}

        ///// <summary>
        ///// Places a player's card onto the hand grid.
        ///// </summary>
        ///// <param name="card"></param>
        //private void PlaceCard(PlayerCard card)
        //{
        //    Vector2 spawnPoint;
        //    spawnPoint = handGrid.GetNearestPointOnGrid(cardSpot);

        //    if (handGrid.IsPlaceable(spawnPoint))
        //    {
        //        card.SetCoord(spawnPoint);
        //        hand.Add(card);

        //        inHandObjects.Add(Instantiate(card, this.transform).gameObject);
        //        cardsInHand += 1;
        //        cardSpot.x += handGrid.size;
        //    }
        //}

        ///// <summary>
        ///// Sends a card to the player's graveyard from that player's hand.
        ///// </summary>
        ///// <param name="cardPlayed"></param>
        //public void SendToGraveyard(PlayerCard cardPlayed)
        //{
        //    if (cardPlayed != null)
        //    {
        //        // Find the index of the card within the Hand
        //        int i;
        //        for (i = 0; i < hand.Count; i++)
        //        {
        //            if (cardPlayed.cardName == hand[i].cardName)
        //            {
        //                //Debug.Log("Found at index: " + i);
        //                break;
        //            }
        //        }

        //        if (i < hand.Count)
        //        {
        //            // Card was found -> Add to Graveyard
        //            if (hand[i] != (PlayerCard)deck.phantomCard)
        //            {
        //                graveyard.AddToGrave(hand[i]);
        //            }

        //            hand.RemoveAt(i);


        //            if (cardsInHand > 6)
        //            {
        //                //Debug.Log("Card -> Graveyard && Resizing");
        //            }

        //            cardsInHand -= 1;
        //        }
        //        else
        //        {
        //            //Debug.Log("Card not found in hand");
        //        }
        //    }
        //}

        ///// <summary>
        ///// Sends the player's hand to the graveyard and resizes the hand grid.
        ///// </summary>
        //public void SendHandToGraveyard()
        //{
        //    foreach (var card in hand)
        //    {
        //        if (card != (PlayerCard)deck.phantomCard)
        //        {
        //            graveyard.AddToGrave(card);
        //        }
        //    }
        //    hand.Clear();

        //    foreach (var card in inHandObjects)
        //    {
        //        if (card != null)
        //        {
        //            GameObject.Destroy(card);
        //        }
        //    }
        //    inHandObjects.Clear();

        //    handGrid.ResizeGrid(2, 6);
        //    cardSpot = new Vector2();
        //}

        //void UpdateHandDisplay()
        //{

        //}

        ////-------------------//
        ////----- GETTERS -----//
        ////-------------------//
        //public List<PlayerCard> GetHand()
        //{
        //    return this.hand;
        //}
        //public int GetHandCount()
        //{
        //    return this.cardsInHand;
        //}
    //}
