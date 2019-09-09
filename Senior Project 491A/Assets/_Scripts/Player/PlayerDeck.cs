using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDeck : Deck
{
    ///* Reference to the Player whose Deck this is */
    //[SerializeField] private GameObject playerObj;

    ///* Reference for the Player's Graveyard */
    //private Graveyard playersGraveyard;

    //private void Awake()
    //{
    //    // Set Graveyard from parent Player
    //    playersGraveyard = playerObj.GetComponentInChildren<Graveyard>();
        
    //    /// TODO: Can delete later
    //    //foreach (var card in testCards)
    //    //{
    //    //    Debug.Log("Adding " + card + " to graveyard");
    //    //    playersGraveyard.AddToGrave((PlayerCard)card);
    //    //}
    //}

    ///* Adds the PlayerCard to the Player's Graveyard */
    //public void AddToGraveYard(PlayerCard card)
    //{
    //    playersGraveyard.AddToGrave(card);
    //}
}
