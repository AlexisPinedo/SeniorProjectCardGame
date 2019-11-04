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
        
    }

    public void OnMouseExit()
    {
        //shop card
        //Debug.Log("exit");
        transform.localScale = new Vector3(1, 1, 1);  //returns the object to its original state
        transform.position = OriginalPosition;
    }
    
    public void OnMouseDown()
    {
        //shop card
        if (this.transform.parent.gameObject.GetComponent<HandContainer>() == null) {
            transform.position = OriginalPosition;
        }
        //player card
        else{
            transform.position = OriginalPosition;
        }
    }
}