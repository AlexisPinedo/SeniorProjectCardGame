using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CardZoomer : MonoBehaviourPunCallbacks, IPunObservable
{
    private object myGameObject;

    public Vector2 OriginalPosition;

    private void Awake()
    {
        OriginalPosition = this.transform.position;
    }

    public void OnMouseEnter()
    {
        if (photonView.IsMine)
        {
            //Debug.Log("enter");
            transform.localScale += new Vector3(1.5F, 1.5F, 1.5F); //zooms in the object
        }
    }

    private void OnMouseDrag()
    {
        if (photonView.IsMine)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void OnMouseExit()
    {
        if (photonView.IsMine)
        {
            //Debug.Log("exit");
            transform.localScale = new Vector3(1, 1, 1);  //returns the object to its original state
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.localScale);
        }
        if (stream.IsReading)
        {
            transform.localScale = (Vector3)stream.ReceiveNext();
        }
    }
}