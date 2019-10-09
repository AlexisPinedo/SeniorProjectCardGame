using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CardZoomer : MonoBehaviourPunCallbacks
{
    private object myGameObject;

    public Vector2 OriginalPosition;

    private bool offline;

    private void Awake()
    {
        OriginalPosition = this.transform.position;
        //offline = PhotonNetworkManager.IsOffline;

    }

    public void OnMouseEnter()
    {
        //cardcollider.size & cardcollider.offset
        //shop card
        if (this.transform.parent.gameObject.GetComponent<HandContainer>() == null) {
            //Debug.Log("enter");
            transform.localScale += new Vector3(0.5F, 0.5F, 0.5F); //zooms in the object
            Vector3 newPosition = new Vector3(0, -1, 0);
            transform.position += newPosition;
        }
        //player card
        else{
            //Debug.Log("enter");
            transform.localScale += new Vector3(0.5F, 0.5F, 0.5F); //zooms in the object
            Vector3 newPosition = new Vector3(0, 1, 0);
            transform.position += newPosition;
        }
        if (offline || photonView.IsMine)
        {
            //Debug.Log("enter");
            //photonView.RPC("RPCOnMouseEnter", RpcTarget.Others, transform.localScale);
        }
    }

    private void OnMouseDrag()
    {
        //shop card
        if (this.transform.parent.gameObject.GetComponent<HandContainer>() == null) {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //player card
        else{
            transform.localScale = new Vector3(1, 1, 1);
        }
        
        if (offline || photonView.IsMine)
        {
            //photonView.RPC("RPCOnMouseDrag", RpcTarget.Others, transform.localScale);
        }
    }

    public void OnMouseExit()
    {
        //shop card
        //Debug.Log("exit");
        transform.localScale = new Vector3(1, 1, 1);  //returns the object to its original state
        transform.position = OriginalPosition;
        
        if (offline || photonView.IsMine)
        {
            //Debug.Log("exit");
            //photonView.RPC("RPCOnMouseExit", RpcTarget.Others, transform.localScale);
        }
    }
    
    public void OnMouseDown()
    {
        //shop card
        if (this.transform.parent.gameObject.GetComponent<HandContainer>() == null) {
            transform.position = OriginalPosition;
        }
        //player card
        else{
            transform.localScale = OriginalPosition;
        }
    }

    [PunRPC]
    private void RPCOnMouseEnter(Vector3 position)
    {
        transform.localScale = position;
    }

    [PunRPC]
    private void RPCOnMouseDrag(Vector3 position)
    {
        transform.localScale = position;
    }

    [PunRPC]
    private void RPCOnMouseExit(Vector3 position)
    {
        transform.localScale = position;
    }
}