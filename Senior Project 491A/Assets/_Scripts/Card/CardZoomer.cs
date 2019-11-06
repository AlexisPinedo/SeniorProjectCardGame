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

    private void HandleMounseEnteringCard()
    {
        if (transform.parent.gameObject.GetComponent<HandContainer>() == null)
        {
            //Debug.Log("enter");
            transform.localScale = new Vector2(1.5F, 1.5F); //zooms in the object
            Vector3 newPosition = new Vector2(0, -1);
            transform.position = new Vector2(newPosition.x + OriginalPosition.x, newPosition.y + OriginalPosition.y);
        }
        //player card
        else
        {
            //Debug.Log("enter");
            transform.localScale = new Vector2(1.5F, 1.5F); //zooms in the object
            Vector3 newPosition = new Vector2(0, 1);
            transform.position = new Vector2(newPosition.x + OriginalPosition.x, newPosition.y + OriginalPosition.y);

        }
    }
//    private void HandleMounseEnteringCard(CardZoomer cardZoomedIn)
//    {
//        if (cardZoomedIn.transform.parent.gameObject.GetComponent<HandContainer>() == null)
//        {
//            //Debug.Log("enter");
//            cardZoomedIn.transform.localScale = new Vector2(1.5F, 1.5F); //zooms in the object
//            Vector3 newPosition = new Vector2(0, -1);
//            cardZoomedIn.transform.position = new Vector2(newPosition.x + OriginalPosition.x, newPosition.y + OriginalPosition.y);
//        }
//        //player card
//        else
//        {
//            //Debug.Log("enter");
//            cardZoomedIn.transform.localScale = new Vector2(1.5F, 1.5F); //zooms in the object
//            Vector3 newPosition = new Vector2(0, 1);
//            cardZoomedIn.transform.position = new Vector2(newPosition.x + OriginalPosition.x, newPosition.y + OriginalPosition.y);
//
//        }
//    }

    public void OnMouseEnter()
    {
        HandleMounseEnteringCard();
        if(photonView.IsMine)
            photonView.RPC("RPCOnMouseEnter", RpcTarget.Others);
    }
    
    [PunRPC]
    private void RPCOnMouseEnter()
    {
        HandleMounseEnteringCard();
    }

//    [PunRPC]
//    private void RPCOnMouseEnter(int cardID)
//    {
//        PhotonView foundCard = PhotonView.Find(cardID);
//        if (foundCard)
//        {
//            CardZoomer cardZoomed = foundCard.GetComponent<CardZoomer>();
//            HandleMounseEnteringCard(cardZoomed);
//        }
//        else
//        {
//            Debug.Log("Photon View not found. CardID: " + cardID);
//        }
//    }

    private void OnMouseDrag()
    {
        transform.localScale = new Vector3(1, 1, 1);

        photonView.RPC("RPCOnMouseDrag", RpcTarget.Others, photonView.ViewID);
    }

    [PunRPC]
    private void RPCOnMouseDrag(int cardID, Transform trans)
    {
        PhotonView foundCard = PhotonView.Find(cardID);
        if (foundCard)
        {
            //update the position of the card
            foundCard.transform.localScale = new Vector3(1, 1, 1);;
        }
        else
        {
            Debug.Log("Photon View not found. CardID: " + cardID);
        }
    }

    public void OnMouseExit()
    {
        //shop card
        //Debug.Log("exit");
        transform.localScale = new Vector3(1, 1, 1);  //returns the object to its original state
        transform.position = OriginalPosition;

        photonView.RPC("RPCOnMouseExit", RpcTarget.Others, photonView.ViewID);
    }

    [PunRPC]
    private void RPCOnMouseExit(int cardID)
    {
        PhotonView foundCard = PhotonView.Find(cardID);
        if (foundCard)
        {
            foundCard.transform.localScale = new Vector3(1, 1, 1);  //returns the object to its original state
            foundCard.transform.position = OriginalPosition;
        }
        else
        {
            Debug.Log("Photon View not found. CardID: " + cardID);
        }
    }

    public void OnMouseDown()
    {
        //shop card
        //if (this.transform.parent.gameObject.GetComponent<HandContainer>() == null)
        //{
            transform.position = OriginalPosition;
        //}

        //player card
        //else
        //{
        //    transform.position = OriginalPosition;
        //}

        photonView.RPC("RPCOnMouseDown", RpcTarget.Others, photonView.ViewID);
    }

    [PunRPC]
    private void RPCOnMouseDown(int cardID)
    {
        PhotonView foundCard = PhotonView.Find(cardID);
        if (foundCard)
        {
            //update the position of the card
            foundCard.transform.position = OriginalPosition;
        }
        else
        {
            Debug.Log("Photon View not found. CardID: " + cardID);
        }
    }
}