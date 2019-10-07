using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// An abstract class that defines a Container for different types of Cards.
/// </summary>
public abstract class Container : MonoBehaviour
{
    /// <summary>
    /// The grid where the Cards in this Container are shown.
    /// </summary>
    [Tooltip("The grid where the Cards are shown")]
    public  Grid containerGrid;
    
    /// <summary>
    /// Initializes card placements on the Container's grid.
    /// </summary>
    protected virtual void InitialCardDisplay()
    {
        Debug.Log("Container: am I overridden?");
    }

}
