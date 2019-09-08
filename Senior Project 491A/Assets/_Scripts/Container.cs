using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Container : MonoBehaviour
{
    [FormerlySerializedAs("container")] public PlayerCardHolder holder;
    public Grid containerGrid;
    
    protected virtual void InitialCardDisplay()
    {

    }
}
