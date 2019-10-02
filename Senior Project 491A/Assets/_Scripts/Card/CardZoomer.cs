using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZoomer : MonoBehaviour
{
    private object myGameObject;

    public Vector2 OriginalPosition;
    

    private void Awake()
    {
        OriginalPosition = this.transform.position;
    }

    public void OnMouseEnter()
    {
        //shop card
        if (this.transform.parent.gameObject.GetComponent<HandContainer>() == null) {
            Debug.Log("enter");
            transform.localScale += new Vector3(0.5F, 0.5F, 0.5F); //zooms in the object
            Vector3 newPosition = new Vector3(0, -1.5F, 0);
            transform.position += newPosition;
        }
        //player card
        else{
            Debug.Log("enter");
            transform.localScale += new Vector3(0.5F, 0.5F, 0.5F); //zooms in the object
            Vector3 newPosition = new Vector3(0, 1.5f, 0);
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
        //TODO: Make a conditional statement
        //where if a card is being dragged, other cards should not zoom when they are touched
    }

    public void OnMouseExit()
    {
        //shop card
        if (this.transform.parent.gameObject.GetComponent<HandContainer>() == null) {
            Debug.Log("exit");
            transform.localScale = new Vector3(1, 1, 1);  //returns the object to its original state
            transform.position = OriginalPosition;
        }
        //player card
        else{
            Debug.Log("exit");
            transform.localScale = new Vector3(1, 1, 1);  //returns the object to its original state
            transform.position = OriginalPosition;
        }
    }
}