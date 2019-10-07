using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZoomer : MonoBehaviour
{
    public Vector2 OriginalPosition;

    private BoxCollider2D cardBoxCollider;

    private void Awake()
    {
        OriginalPosition = this.transform.position;
        cardBoxCollider = GetComponent<BoxCollider2D>();
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

    public void OnMouseDrag()
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

//    public void OnMouseDown()
//    {
//        //shop card
//        if (this.transform.parent.gameObject.GetComponent<HandContainer>() == null) {
//            transform.position = OriginalPosition;
//        }
//        //player card
//        else{
//            transform.localScale = OriginalPosition;
//        }
//    }
}