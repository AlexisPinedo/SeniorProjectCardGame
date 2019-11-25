using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An abstract class that defines a Container specific for PlayerCards.
/// containers act as a organized location for all card displays instantiated into the game
/// </summary>
public abstract class PlayerCardContainer : Container
{
    //The player card containers need a reference to the player card display to load in the player card components
    public PlayerCardDisplay display;
    public GameObject spawnPostion;


}
