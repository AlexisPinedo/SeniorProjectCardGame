using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// This class handles the dragging of cards. When a card is clicked it will follow the mouse and drag with it. 
/// </summary>
public class DragCard : MonoBehaviourPun
{
    private Vector3 offset;
    private Vector3 screenPoint;
    public Vector2 OriginalPosition;
    public static bool cardHeld = false;

    //This class is attached to all player cards. This can be cards owned by the player or by the shop
    //We use this delegate event to handle the clicking of a card in the shop 
    public delegate void _ShopCardClicked(PlayerCardDisplay cardClicked);

    public static event _ShopCardClicked ShopCardClicked;
    public static event Action <PlayerCardDisplay> CardDragged;
    public static event Action CardReleased; 
    
    private void Awake()
    {
        //Set the original position of the card to its location in space to use a reference
        OriginalPosition = this.transform.position;
    }


    private void HandleCardClicked(Vector3 RPCoffset)
    {
        cardHeld = true;
        //used to grab the z coordinate of the game object 
        //We need to conver the position to world space so it works with nested objects
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        //Here we add the offset from the card and the mouse
        offset = RPCoffset;

        //if the card display has no hand container it means that the card is in the shop
        //We can use this to our advantage by adding shop functionality here

        //Debug.Log("Card is in Shop");
        PlayerCardDisplay cardClicked = this.gameObject.GetComponent<PlayerCardDisplay>();

        if (this.transform.parent.gameObject.GetComponent<HandContainer>() == null)
        {
            cardClicked.transform.position = OriginalPosition;

            ShopCardClicked?.Invoke(cardClicked);
        }
        else
        {
            CardDragged?.Invoke(cardClicked);
        }
    }

    /// <summary>
    /// This method is ivoked when the mouse button is clicked
    /// </summary>
    public void OnMouseDown()
    {
        //photon view of our current card
        if (photonView.IsMine)
        {
            //used to grab the z coordinate of the game object 
            //We need to conver the position to world space so it works with nested objects
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            //Here we add the offset from the card and the mouse
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(
                         new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

            HandleCardClicked(offset);
            photonView.RPC("RPCOnMouseDown", RpcTarget.Others, offset);
        }
    }

    [PunRPC]
    private void RPCOnMouseDown(Vector3 position)
    {
        HandleCardClicked(position);
    }

    private void HandleCardRelease()
    {
        cardHeld = false;
        CardReleased?.Invoke();
         //if there is a gameobject and the card is not in the play zone we will return the card to the original position
         if (gameObject != null && PlayZone.cardInPlayZone == false)
         {
             transform.position = OriginalPosition;
         }
    }

    /// <summary>
    /// this method is called when the mouse button is released
    /// </summary>
    public void OnMouseUp()
    {
        HandleCardRelease();
        if (photonView.IsMine)
            photonView.RPC("RPCOnMouseUp", RpcTarget.Others);
    }

    [PunRPC]
    private void RPCOnMouseUp()
    {
        HandleCardRelease();
    }

    private void MoveCardWithCursor(Vector3 position)
    {
        //if the card display has no hand container it means that the card is in the shop
        //if that is the case we do not want to drag the card. 
        if (transform.parent.gameObject.GetComponent<HandContainer>() == null)
        {
            return;
        }
        //updates position of game object
        transform.position = position;
    }
    
    /// <summary>
    /// This method is called when the card is being dragged
    /// </summary>
    public void OnMouseDrag()
    {
        if (photonView.IsMine)
        {
            //stores position of cursor in screen space
            Vector2 cursorScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //grabs the position of the mouse cursor and converts to world space
            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;

            MoveCardWithCursor(cursorPosition);
            photonView.RPC("RPCOnMouseDrag", RpcTarget.Others, cursorPosition);
        }
    }

    [PunRPC]
    private void RPCOnMouseDrag(Vector3 position)
    {
        MoveCardWithCursor(position);
    }
}