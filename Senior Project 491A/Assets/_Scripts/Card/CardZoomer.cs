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
        offline = PhotonNetworkManager.IsOffline;

    }

    public void OnMouseEnter()
    {
        if (offline || photonView.IsMine)
        {
            //Debug.Log("enter");
            transform.localScale += new Vector3(1.5F, 1.5F, 1.5F); //zooms in the object
            photonView.RPC("RPCOnMouseEnter", RpcTarget.Others, transform.localScale);
        }
    }

    private void OnMouseDrag()
    {
        if (offline || photonView.IsMine)
        {
            transform.localScale = new Vector3(1, 1, 1);
            photonView.RPC("RPCOnMouseDrag", RpcTarget.Others, transform.localScale);
        }
    }

    public void OnMouseExit()
    {
        if (offline || photonView.IsMine)
        {
            //Debug.Log("exit");
            transform.localScale = new Vector3(1, 1, 1);  //returns the object to its original state
            photonView.RPC("RPCOnMouseExit", RpcTarget.Others, transform.localScale);
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