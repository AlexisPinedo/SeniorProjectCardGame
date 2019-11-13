using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// This abstract class is the base class for all containers in the game
/// containers act as a organized location for all card displays instantiated into the game
/// </summary>
public abstract class Container : MonoBehaviour
{
    /// <summary>
    /// The grid where the Cards in this Container are instantiated too.
    /// These are a series of nodes that can be incremented or decrimented
    /// </summary>
    [Tooltip("The grid where the Cards are shown")]
    public CardGrid containerCardGrid;
    
    /// <summary>
    /// Initializes card placements on the Container's grid.
    /// This abstract method will run through instantiating every location
    /// set in the grid.
    /// </summary>
    protected virtual void DrawStartingHand()
    {
        Debug.Log("Container: am I overridden?");
    }

}
