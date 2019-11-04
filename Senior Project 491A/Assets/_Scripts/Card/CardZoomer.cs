using System;
using System.Collections;
using System.Collections.Generic;
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
        //cardcollider.size & cardcollider.offset
        //shop card
        if (this.transform.parent.gameObject.GetComponent<HandContainer>() == null)
        {
            //Debug.Log("enter");
            transform.localScale += new Vector3(0.5F, 0.5F, 0.5F); //zooms in the object
            Vector3 newPosition = new Vector3(0, -1, 0);
            transform.position += newPosition;
        }
        //player card
        else
        {
            //Debug.Log("enter");
            transform.localScale += new Vector3(0.5F, 0.5F, 0.5F); //zooms in the object
            Vector3 newPosition = new Vector3(0, 1, 0);
            transform.position += newPosition;
        }
        photonView.RPC("RPCOnMouseEnter", RpcTarget.Others, photonView.ViewID, transform.position);
    }

    [PunRPC]
    private void RPCOnMouseEnter(int cardID, Vector3 RPCPosition)
    {
        PhotonView foundCard = PhotonView.Find(cardID);
        if (foundCard)
        {
            if (foundCard.transform.parent.gameObject.GetComponent<HandContainer>() == null)
            {
                foundCard.transform.localScale += new Vector3(0.5F, 0.5F, 0.5F);
                foundCard.transform.position += RPCPosition;
            }
            else
            {
                foundCard.transform.localScale += new Vector3(0.5F, 0.5F, 0.5F);
                foundCard.transform.position += RPCPosition;
            }
        }
        else
            Debug.Log("Photon View not found. CardID: " + cardID);
    }

    private void OnMouseDrag()
    {
        if (this.transform.parent.gameObject.GetComponent<HandContainer>() == null)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
        photonView.RPC("RPCOnMouseDrag", RpcTarget.Others, photonView.ViewID, transform.localScale);
    }

    [PunRPC]
    private void RPCOnMouseDrag(int cardID, Vector3 RPCScale)
    {
        PhotonView foundCard = PhotonView.Find(cardID);
        if (foundCard)
        {
            if (foundCard.transform.parent.gameObject.GetComponent<HandContainer>() == null)
                foundCard.transform.localScale = RPCScale;
            else
                foundCard.transform.localScale = RPCScale;
        }
        else
            Debug.Log("Photon View not found. CardID: " + cardID);
    }

    public void OnMouseExit()
    {
        transform.localScale = new Vector3(1, 1, 1);  //returns the object to its original state
        transform.position = OriginalPosition;
        photonView.RPC("RPCOnMouseExit", RpcTarget.Others, photonView.ViewID, transform.position);
    }

    [PunRPC]
    private void RPCOnMouseExit(int cardID, Vector3 RPCPosition)
    {
        PhotonView foundCard = PhotonView.Find(cardID);
        if (foundCard)
        {
            //update the position of the card
            foundCard.transform.localScale = new Vector3(1, 1, 1);
            foundCard.transform.position = RPCPosition;
        }
        else
            Debug.Log("Photon View not found. CardID: " + cardID);
    }

    public void OnMouseDown()
    {
        if (this.transform.parent.gameObject.GetComponent<HandContainer>() == null)
            transform.position = OriginalPosition;
        else
            transform.position = OriginalPosition;

        photonView.RPC("RPCOnMouseDown", RpcTarget.Others, photonView.ViewID, transform.position);
    }

    [PunRPC]
    private void RPCOnMouseDown(int cardID, Vector3 RPCPosition)
    {
        PhotonView foundCard = PhotonView.Find(cardID);
        if (foundCard)
        {
            if (foundCard.transform.parent.gameObject.GetComponent<HandContainer>() == null)
                foundCard.transform.position = RPCPosition;
            else
                foundCard.transform.position = RPCPosition;
        }
        else
            Debug.Log("Photon View not found. CardID: " + cardID);
    }
}