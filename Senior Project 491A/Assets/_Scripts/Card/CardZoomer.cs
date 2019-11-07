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
            
//            if(photonView.IsMine)
//                photonView.RPC("RPCOnMouseEnter", RpcTarget.Others);
        }
    }
    
//    [PunRPC]
//    private void RPCOnMouseEnter()
//    {
//        ZoomInOnCard();
//    }

    public void OnMouseExit()
    {
        
        ZoomOutOfCard();
        
//        if(photonView.IsMine)
//            photonView.RPC("RPCOnMouseExit", RpcTarget.Others);
    }

//    [PunRPC]
//    private void RPCOnMouseExit()
//    {
//        ZoomOutOfCard();
//    }

    public void OnMouseDown()
    {
        ZoomOutOfCard();
        
//        if(photonView.IsMine)
//            photonView.RPC("RPCOnMouseDown", RpcTarget.Others);
    }

//    [PunRPC]
//    private void RPCOnMouseDown()
//    {
//        ZoomOutOfCard();
//    }
    
    private void ZoomInOnCard()
    {
        Debug.Log("zoom in " + photonView.ViewID);
        if (transform.parent.gameObject.GetComponent<HandContainer>() == null)
        {
            //Debug.Log("enter");
            transform.localScale = new Vector2(1.5F, 1.5F); //zooms in the object
            Vector2 newPosition = new Vector2(0, -1);
            transform.position = new Vector2(newPosition.x + OriginalPosition.x, newPosition.y + OriginalPosition.y);
        }
        //player card
        else
        {
            //Debug.Log("enter");
            transform.localScale = new Vector2(1.5F, 1.5F); //zooms in the object
            Vector2 newPosition = new Vector2(0, 1);
            transform.position = new Vector2(newPosition.x + OriginalPosition.x, newPosition.y + OriginalPosition.y);

        }
    }

    private void ZoomOutOfCard()
    {
        Debug.Log(photonView.ViewID);
        transform.localScale = new Vector2(1, 1);  //returns the object to its original state
        if (!DragCard.cardHeld)
            transform.position = OriginalPosition;
    }
}