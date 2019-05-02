using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : Deck
{
    /* Reference to the Player whose Deck this is */
    [SerializeField]
    private GameObject playerObj;

    public PlayerCard phantomCard;

    // Reference for the player's graveyard
    private Graveyard playersGraveyard;

    private void Awake()
    {
        // Reference player's components
        //SplayersGraveyard = playerObj.GetComponentInChildren<Graveyard>();

        // fillDeck();
        playersGraveyard = playerObj.GetComponentInChildren<Graveyard>();
        foreach (var card in testCards)
        {
            Debug.Log("Adding " + card + " to graveyard");
            playersGraveyard.addToGrave((PlayerCard)card);
        }
    }

    public void AddToGraveYard(PlayerCard card)
    {
        playersGraveyard.addToGrave(card);
    }
}
