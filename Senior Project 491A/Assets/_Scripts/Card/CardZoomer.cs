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
        //Debug.Log("enter");
        transform.localScale += new Vector3(1.5F, 1.5F, 1.5F); //zooms in the object

    }

    private void OnMouseDrag()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void OnMouseExit()
    {
        //Debug.Log("exit");
        transform.localScale = new Vector3(1, 1, 1);  //returns the object to its original state
    }
}