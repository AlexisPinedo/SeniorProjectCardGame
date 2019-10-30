﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
/// <summary>
/// This class handles the in game display of each card.
/// The class holds components relative to all card displays
/// Each card component will have its data read and stored here
/// this is used to relay each card's information to the player
/// </summary>
///

public abstract class CardDisplay : MonoBehaviourPunCallbacks, Itransferable
{
    [SerializeField] protected SpriteRenderer cardArtDisplay;
    [SerializeField] protected SpriteRenderer typeIcon;
    [SerializeField] protected SpriteRenderer cardBorder;
    [SerializeField] protected SpriteRenderer cardEffectTextBox;
    [SerializeField] protected SpriteRenderer cardNameTextBox;
    [SerializeField] protected TextMeshPro nameText;
    [SerializeField] protected TextMeshPro cardEffectText;
    
    [SerializeField]
    protected BoxCollider2D cardDisplayCollider;
    

    protected virtual void Awake()
    {
        cardDisplayCollider = GetComponent<BoxCollider2D>();
        TransferOwnersihpManager.AddTransferableObjectToList(this);
        SetId();
        LoadCardIntoDisplay();
    }
    
    /// <summary>
    /// This method like the others will handle destruction and creation of the displays
    /// running virtual methods of what to do when this happens 
    /// </summary>
    protected virtual void OnDestroy()
    {
        ClearCardFromDisplay();
    }

    protected virtual void OnEnable()
    {
        Event_Base.GameStatePausingEventTriggered += DisableBoxCollider;
        Event_Base.GameStatePausingEventEnded += EnableBoxCollider;
        //Debug.Log("CardDisplay: OnEnable()");
        LoadCardIntoDisplay();
        
    }

    protected virtual void OnDisable()
    {
        Event_Base.GameStatePausingEventTriggered -= DisableBoxCollider;
        Event_Base.GameStatePausingEventEnded -= EnableBoxCollider;
        ClearCardFromDisplay();
    }
    
    protected virtual void LoadCardIntoDisplay()
    {

    }
    
    protected virtual void ClearCardFromDisplay()
    {

    }

    protected virtual void OnMouseDown()
    {
        
    }
    
    protected virtual void DisableBoxCollider()
    {
        //Debug.Log("disabling collider for " + nameText);
        if (this != null)
        {
            if(cardDisplayCollider.enabled)
                cardDisplayCollider.enabled = false;
        }
    }

    public virtual void EnableBoxCollider()
    {
        if(this != null)
            if(!cardDisplayCollider.enabled)
                cardDisplayCollider.enabled = true;
    }

    public int id { get; set; }

    public void SetId()
    {
        //run logic to assign id depending if you are master or not
    }

    public void DoTransfer()
    {
        //What we want to happen when we do transfer
    }

    public void HandleTransfer()
    {
        
    }

    public void OnTransfer()
    {
    }
}
