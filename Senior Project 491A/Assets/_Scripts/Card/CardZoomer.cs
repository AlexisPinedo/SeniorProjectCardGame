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
        transform.localScale += new Vector3(1.5F, 1.5F, 1.5F); //zooms in the object

        if (offline || photonView.IsMine)
        {
            //Debug.Log("enter");
            //photonView.RPC("RPCOnMouseEnter", RpcTarget.Others, transform.localScale);
        }
    }

    private void OnMouseDrag()
    {
        transform.localScale = new Vector3(1, 1, 1);

        if (offline || photonView.IsMine)
        {
            //photonView.RPC("RPCOnMouseDrag", RpcTarget.Others, transform.localScale);
        }
    }

    public void OnMouseExit()
    {
        transform.localScale = new Vector3(1, 1, 1);  //returns the object to its original state

        if (offline || photonView.IsMine)
        {
            //Debug.Log("exit");
            //photonView.RPC("RPCOnMouseExit", RpcTarget.Others, transform.localScale);
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