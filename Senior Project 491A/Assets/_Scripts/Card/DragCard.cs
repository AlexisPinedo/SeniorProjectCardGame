using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class DragCard : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    private PlayerCard card;

    public void Awake()
    {
        card = GetComponent<PlayerCard>();
    }

    void OnMouseDown()
    {
        if (card.inShop)
        {
            card.PurchaseCard();
        }
        else
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position); //used to grab the z coordinate of the game object 

            offset = gameObject.transform.position -
                     Camera.main.ScreenToWorldPoint(
                         new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
        
    }

    void OnMouseDrag()
    {
        Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z); //stores position of cursor in screen space
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset; //grabs the position of the mouse cursor and converts to world space

        transform.position = cursorPosition; //updates position of game object
    }
}
