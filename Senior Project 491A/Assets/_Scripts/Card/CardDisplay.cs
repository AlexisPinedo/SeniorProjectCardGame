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
public abstract class CardDisplay : MonoBehaviourPunCallbacks
{
    protected const byte LOAD_CARD_EVENT = 1;

    [SerializeField] protected SpriteRenderer cardArtDisplay;
    [SerializeField] protected SpriteRenderer typeIcon;
    [SerializeField] protected SpriteRenderer cardBorder;
    [SerializeField] protected SpriteRenderer cardEffectTextBox;
    [SerializeField] protected SpriteRenderer cardNameTextBox;
    [SerializeField] protected TextMeshPro nameText;
    [SerializeField] protected TextMeshPro cardEffectText;

    private bool offline;

    protected virtual void Awake()
    {
//        offline = PhotonNetworkManager.IsOffline;
//
//        Debug.Log(this.nameText + " from CardDisplay is owned by " + this.photonView.OwnerActorNr);
//
//        if (!offline && this.photonView.Owner != PhotonNetwork.MasterClient)
//        {
//
//            Debug.Log("From Cardholder.cs, transfering card ownership to Master Client");
//
//            this.photonView.TransferOwnership(PhotonNetwork.MasterClient);
//        }

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
        //Debug.Log("CardDisplay: OnEnable()");
        LoadCardIntoDisplay();
    }

    protected virtual void OnDisable()
    {
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
}
