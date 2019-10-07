using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Holds information pertinent to all types of Cards in the game.
/// </summary>
public abstract class CardHolder : MonoBehaviour
{
    protected const byte LOAD_CARD_EVENT = 1;

    [SerializeField] protected SpriteRenderer cardArtDisplay;
    [SerializeField] protected SpriteRenderer typeIcon;
    [SerializeField] protected SpriteRenderer cardBorder;
    [SerializeField] protected SpriteRenderer cardEffectTextBox;
    [SerializeField] protected SpriteRenderer cardNameTextBox;
    [SerializeField] protected TextMeshPro nameText;
    [SerializeField] protected TextMeshPro cardEffectText;

    private BoxCollider2D cardCollider;

    protected virtual void Awake()
    {
        //Debug.Log("CardHolder: Awake()");
        LoadCardIntoContainer();
    }
    
    protected virtual void OnDestroy()
    {
        ClearCardFromContainer();
    }

    protected virtual void OnEnable()
    { 
        //Debug.Log("CardHolder: OnEnable()");
        LoadCardIntoContainer();

        UIHandler.NotificationWindowEnabled += DisableCollider;

        NotificationWindow.NotificatoinWindoClosed += EnableCollider;
    }

    protected virtual void OnDisable()
    {
        ClearCardFromContainer();
        
        UIHandler.NotificationWindowEnabled -= DisableCollider;

        NotificationWindow.NotificatoinWindoClosed -= EnableCollider;
    }
    
    protected virtual void LoadCardIntoContainer()
    {

    }
    
    protected virtual void ClearCardFromContainer()
    {

    }

    protected virtual void OnMouseDown()
    {

    }
    protected virtual void DisableCollider()
    {
        cardCollider = GetComponent<BoxCollider2D>();

        Debug.Log("Disabling card collider");
        if(cardCollider != null)
            cardCollider.enabled = false;
    }

    protected virtual void EnableCollider()
    {
        cardCollider = GetComponent<BoxCollider2D>();

        Debug.Log("Enabling card collider");
        
        if(cardCollider != null)
            cardCollider.enabled = true; 
    }
}
