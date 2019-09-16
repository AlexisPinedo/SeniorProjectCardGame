using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZoomer : MonoBehaviour
{
    public void OnMouseEnter()
    {
        //Debug.Log("enter");
        //transform.localScale += new Vector3(1.1F, 1.1f, 1.1f); //zooms in the object
    }

    public void OnMouseExit()
    {
        //Debug.Log("exit");
        //transform.localScale = new Vector3(1, 1, 1);  //returns the object to its original state
    }
}
