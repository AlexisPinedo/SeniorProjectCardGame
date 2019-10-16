using System;
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

public abstract class CardDisplay : MonoBehaviourPunCallbacks
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
        NotificationWindowEvent.NotificationWindowOpened += DisableBoxCollider;
        NotificationWindowEvent.NotificatoinWindoClosed += EnableBoxCollider;
        //Debug.Log("CardDisplay: OnEnable()");
        LoadCardIntoDisplay();
        
    }

    protected virtual void OnDisable()
    {
        NotificationWindowEvent.NotificationWindowOpened -= DisableBoxCollider;
        NotificationWindowEvent.NotificatoinWindoClosed -= EnableBoxCollider;
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
        cardDisplayCollider.enabled = false;
    }

    protected virtual void EnableBoxCollider()
    {
        cardDisplayCollider.enabled = true;
    }
    
}
