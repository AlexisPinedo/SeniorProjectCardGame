using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// This class handles the dragging of cards. When a card is clicked it will follow the mouse and drag with it. 
/// </summary>
public class DragCard : MonoBehaviourPunCallbacks
{
    private Vector3 offset;
    private Vector3 screenPoint;
    public Vector2 OriginalPosition;
    
    //This class is attached to all player cards. This can be cards owned by the player or by the shop
    //We use this delegate event to handle the clicking of a card in the shop 
    public delegate void _ShopCardClicked(PlayerCardDisplay cardClicked);

    public static event _ShopCardClicked ShopCardClicked;
    public static event Action CardDragged;
    public static event Action CardReleased; 

    private bool offline;
    
    private void Awake()
    {
        //Set the original position of the card to its location in space to use a reference
        OriginalPosition = this.transform.position;

//        offline = PhotonNetworkManager.IsOffline;
//        Debug.Log(this.name + " from DragCard is owned by " + this.photonView.OwnerActorNr);
//
//
//        if (!offline)
//        {
//            if (this.photonView.Owner != PhotonNetwork.MasterClient)
//            {
//                Debug.Log("From DragCard.cs, transfering card ownership to Master Client");
//                this.photonView.TransferOwnership(PhotonNetwork.MasterClient);
//            }
//        }
    }

    /// <summary>
    /// This method is ivoked when the mouse button is clicked
    /// </summary>
    public void OnMouseDown()
    {
        //used to grab the z coordinate of the game object 
        //We need to conver the position to world space so it works with nested objects
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        //Here we add the offset from the card and the mouse
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(
                     new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        
        //if the card display has no hand container it means that the card is in the shop
        //We can use this to our advantage by adding shop functionality here
        if (this.transform.parent.gameObject.GetComponent<HandContainer>() == null)
        {
            //Debug.Log("Card is in Shop");
            PlayerCardDisplay cardClicked = this.gameObject.GetComponent<PlayerCardDisplay>();
            this.transform.position = OriginalPosition;
            ShopCardClicked?.Invoke(cardClicked);
        }
        if (offline || photonView.IsMine)
        {
            //if(!offline)
                //photonView.RPC("RPCOnMouseDown", RpcTarget.Others, cardClicked);
        }
    }

    /// <summary>
    /// this method is called when the mouse button is released
    /// </summary>
    public void OnMouseUp()
    {
        //if there is a gameobject and the card is not in the play zone we will return the card to the original position
        if (this.gameObject != null && PlayZone.cardInPlayZone == false)
        {
            this.transform.position = OriginalPosition;
        }

        if (offline || photonView.IsMine)
        {
            if (!offline)
                    photonView.RPC("RPCOnMouseUp", RpcTarget.Others, OriginalPosition);
        }
    }

    /// <summary>
    /// This method is called when the card is being dragged
    /// </summary>
    public void OnMouseDrag()
    {
        //if the card display has no hand container it means that the card is in the shop
        //if that is the case we do not want to drag the card. 
        if (this.transform.parent.gameObject.GetComponent<HandContainer>() == null)
        {
            return;
        }
        
        
        //Debug.Log("Attempting to drag and the object is draggable");
        
        //stores position of cursor in screen space
        UnityEngine.Vector2 cursorScreenPoint = new UnityEngine.Vector2(Input.mousePosition.x, Input.mousePosition.y); 
        
        //grabs the position of the mouse cursor and converts to world space
        UnityEngine.Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset; 
        
        //updates position of game object
        transform.position = cursorPosition;
        
        if (offline || photonView.IsMine)
        {
          
            if (!offline)
                photonView.RPC("RPCOnMouseDrag", RpcTarget.Others, cursorPosition);
        }
    }

    [PunRPC]
    private void RPCOnMouseDown(PlayerCardDisplay cardClicked)
    {
        ShopCardClicked?.Invoke(cardClicked);
    }

    [PunRPC]
    private void RPCOnMouseUp(Vector2 position)
    {
        transform.position = position;
    }

    [PunRPC]
    private void RPCOnMouseDrag(Vector2 position)
    {
        transform.position = position;
    }
}


//public class DragCard : MonoBehaviour
    //{
        //public delegate void _onCardPurschased(PlayerCard cardBought);
        //public static event _onCardPurschased CardPurchased;

        //private Vector3 screenPoint;
        //private Vector3 offset;

        //private PlayerCard card;

        //public bool draggable = true;

        //public void Awake()
        //{
        //    card = GetComponent<PlayerCard>();
        //}

        //void OnMouseDown()
        //{
        //    if (card.inShop)
        //    {
        //        draggable = true;
        //        //if (card.PurchaseCard())
        //        //{
        //        //    if (CardPurchased != null)
        //        //    {
        //        //        CardPurchased.Invoke(card);
        //        //    }
        //        //}
        //    }
        //    else
        //    {
        //        draggable = true;
        //        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position); //used to grab the z coordinate of the game object 

        //        offset = gameObject.transform.position -
        //                 Camera.main.ScreenToWorldPoint(
        //                     new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        //    }
        //}

        //void OnMouseDrag()
        //{
        //    if (draggable)
        //    {
        //        UnityEngine.Vector2 cursorScreenPoint = new UnityEngine.Vector2(Input.mousePosition.x, Input.mousePosition.y); //stores position of cursor in screen space
        //        UnityEngine.Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset; //grabs the position of the mouse cursor and converts to world space

        //        transform.position = cursorPosition; //updates position of game object
        //    }
        //}
    //}
