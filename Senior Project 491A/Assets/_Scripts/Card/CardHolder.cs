using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
/// <summary>
/// Holds information pertinent to all types of Cards in the game.
/// </summary>
public abstract class CardHolder : MonoBehaviourPunCallbacks
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
//        Debug.Log(this.nameText + " from CardHolder is owned by " + this.photonView.OwnerActorNr);
//
//        if (!offline && this.photonView.Owner != PhotonNetwork.MasterClient)
//        {
//
//            Debug.Log("From Cardholder.cs, transfering card ownership to Master Client");
//
//            this.photonView.TransferOwnership(PhotonNetwork.MasterClient);
//        }

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
    }

    protected virtual void OnDisable()
    {
        ClearCardFromContainer();
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
}
