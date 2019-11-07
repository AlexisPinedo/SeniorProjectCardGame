using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Photon.Pun;
using UnityEngine;

/// <summary>
/// This class handles the zooming of cards
/// It will scale based on the card you are highlighting over
/// </summary>
public class CardZoomer : MonoBehaviourPunCallbacks
{
    public Vector2 OriginalPosition;
    
    private void Awake()
    {
        OriginalPosition = this.transform.position;
    }

    public void OnMouseEnter()
    {
        if (!DragCard.cardHeld)
        {
            ZoomInOnCard();
            
            if(photonView.IsMine)
                photonView.RPC("CardHasEntered", RpcTarget.Others);
        }
    }
    
    [PunRPC]
    private void CardHasEntered()
    {
        ZoomInOnCard();
    }

    public void OnMouseExit()
    {
        ZoomOutOfCard();
        
        if(photonView.IsMine)
            photonView.RPC("CardHasExited", RpcTarget.Others);
    }

    [PunRPC]
    private void CardHasExited()
    {
        ZoomOutOfCard();
    }

    public void OnMouseDown()
    {
        ZoomOutOfCard();
        if(photonView.IsMine)
            photonView.RPC("CardZoomClicked", RpcTarget.Others);
    }

    [PunRPC]
    private void CardZoomClicked()
    {
        ZoomOutOfCard();
    }
    
    private void ZoomInOnCard()
    {
        if (transform.parent.gameObject.GetComponent<HandContainer>() == null)
        {
            transform.localScale = new Vector2(1.5F, 1.5F); //zooms in the object
            Vector2 newPosition = new Vector2(0, -1);
            transform.position = new Vector2(newPosition.x + OriginalPosition.x, newPosition.y + OriginalPosition.y);
        }
        //player card
        else
        {
            transform.localScale = new Vector2(1.5F, 1.5F); //zooms in the object
            Vector2 newPosition = new Vector2(0, 1);
            transform.position = new Vector2(newPosition.x + OriginalPosition.x, newPosition.y + OriginalPosition.y);
        }
    }

    private void ZoomOutOfCard()
    {
        transform.localScale = new Vector2(1, 1);  //returns the object to its original state
        if (!DragCard.cardHeld)
            transform.position = OriginalPosition;
    }
}