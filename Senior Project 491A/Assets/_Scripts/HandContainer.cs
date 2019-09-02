using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandContainer : MonoBehaviour
{

    public Hand hand;
    public Deck playerDeck;
    public Graveyard playerGrave;
    public PlayerCardContainer container;
    public PlayerCard phantomCard;
    public CreateGrid handGrid;

    [SerializeField]
    private int DefaultHandSize = 5;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TurnStartDraw()
    {
        PlayerCard cardDrawn = null;
        for (int i = 0; i < DefaultHandSize; i++)
        {
            if (playerDeck.cardsInDeck.Count > 0)
                cardDrawn = (PlayerCard)playerDeck.cardsInDeck.Pop();
            else
            {
                if (playerGrave.graveyard.Count > 0)
                {
                    foreach (var card in playerGrave.graveyard)
                    {
                        playerDeck.cardsInDeck.Push(card);
                        playerGrave.graveyard.Remove(card);
                    }
                    playerDeck.Shuffle();
                }
                else
                {
                    //Draw a phantom card
                    cardDrawn = phantomCard;
                }
            }

            if (cardDrawn == null)
            {
                //Debug.Log("null card");
            }
            PlaceCard(cardDrawn);
        }
    }

    /// <summary>
    /// Places a player's card onto the hand grid.
    /// </summary>
    /// <param name="card"></param>
    private void PlaceCard(PlayerCard cardToPlace)
    {
        container.card = cardToPlace;
        Instantiate(container);

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
