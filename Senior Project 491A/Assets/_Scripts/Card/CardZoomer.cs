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
    public Vector2 originalPosition;
    

    private void Awake()
    {
        originalPosition = this.transform.position;
    }

    public void OnMouseEnter()
    {
        if (!DragCard.cardHeld)
        {
            ZoomInOnCard();
            if(!PhotonNetwork.OfflineMode)
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
        if(!PhotonNetwork.OfflineMode)
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
        if(!PhotonNetwork.OfflineMode)
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
        //Debug.Log("zoom in " + photonView.ViewID);
        if (transform.parent.gameObject.GetComponent<HandContainer>() == null)
        {
            //Debug.Log("enter");
            transform.localScale = new Vector2(1.5F, 1.5F); //zooms in the object
            Vector2 newPosition = new Vector2(0, -1);
            transform.position = new Vector2(newPosition.x + originalPosition.x, newPosition.y + originalPosition.y);
        }
        //player card
        else
        {
            //Debug.Log("enter");
            transform.localScale = new Vector2(1.5F, 1.5F); //zooms in the object
            Vector2 newPosition = new Vector2(0, 1);
            transform.position = new Vector2(newPosition.x + originalPosition.x, newPosition.y + originalPosition.y);

        }
    }

    private void ZoomOutOfCard()
    {
        //Debug.Log("zoom out " +photonView.ViewID);
        transform.localScale = new Vector2(1, 1);  //returns the object to its original state
        if (!DragCard.cardHeld)
            transform.position = originalPosition;
    }
}