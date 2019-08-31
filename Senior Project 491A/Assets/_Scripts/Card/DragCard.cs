using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class DragCard : MonoBehaviour
{
    public delegate void _onCardPurschased(PlayerCard cardBought);
    public static event _onCardPurschased CardPurchased;

    private Vector3 screenPoint;
    private Vector3 offset;

    private PlayerCard card;

    public bool draggable = true;

    public void Awake()
    {
        card = GetComponent<PlayerCard>();
    }

    void OnMouseDown()
    {
        if (card.inShop)
        {
            draggable = true;
            //if (card.PurchaseCard())
            //{
            //    if (CardPurchased != null)
            //    {
            //        CardPurchased.Invoke(card);
            //    }
            //}
        }
        else
        {
            draggable = true;
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position); //used to grab the z coordinate of the game object 

            offset = gameObject.transform.position -
                     Camera.main.ScreenToWorldPoint(
                         new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    void OnMouseDrag()
    {
        if (draggable)
        {
            UnityEngine.Vector2 cursorScreenPoint = new UnityEngine.Vector2(Input.mousePosition.x, Input.mousePosition.y); //stores position of cursor in screen space
            UnityEngine.Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset; //grabs the position of the mouse cursor and converts to world space

            transform.position = cursorPosition; //updates position of game object
        }
    }
}
