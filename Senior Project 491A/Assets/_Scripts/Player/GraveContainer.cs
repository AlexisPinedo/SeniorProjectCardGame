using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveContainer : MonoBehaviour
{
    public Graveyard grave;

    private void Awake()
    {
        grave.graveyard.Clear();
    }
}
