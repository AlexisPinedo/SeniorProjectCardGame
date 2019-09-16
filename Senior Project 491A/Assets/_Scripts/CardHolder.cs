<<<<<<< HEAD
﻿using TMPro;
=======
﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
>>>>>>> 972666df8fb555092af7063e88eb7d6b759ecd18
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Holds information pertinent to all types of Cards in the game.
/// </summary>
public abstract class CardHolder : MonoBehaviourPun
{
    protected const byte LOAD_CARD_EVENT = 1;

    [SerializeField] protected SpriteRenderer cardArtDisplay;
    [SerializeField] protected SpriteRenderer typeIcon;
    [SerializeField] protected SpriteRenderer cardBorder;
    [SerializeField] protected SpriteRenderer cardEffectTextBox;
    [SerializeField] protected SpriteRenderer cardNameTextBox;
    [SerializeField] protected TextMeshPro nameText;
    [SerializeField] protected TextMeshPro cardEffectText;

    protected virtual void Awake()
    {
        Debug.Log("CardHolder: Awake()");
        LoadCardIntoContainer();
    }
    
    protected virtual void OnDestroy()
    {
        ClearCardFromContainer();
    }

    protected virtual void OnEnable()
    {
<<<<<<< HEAD
        Debug.Log("CardHolder: OnEnable()");
=======
        //Debug.Log("card had been enabled ");
>>>>>>> 972666df8fb555092af7063e88eb7d6b759ecd18
        LoadCardIntoContainer();
    }

    protected virtual void OnDisable()
    {
        // Debug.Log("card had been enabled ");
        ClearCardFromContainer();
    }
    
    protected virtual void LoadCardIntoContainer()
    {

    }
    
    protected virtual void ClearCardFromContainer()
    {

    }
<<<<<<< HEAD
=======

    protected virtual void OnMouseDown()
    {
        
    }
>>>>>>> 972666df8fb555092af7063e88eb7d6b759ecd18
}
