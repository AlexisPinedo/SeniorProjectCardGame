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
    public static event Action <PlayerCardDisplay> CardDragged;
    public static event Action CardReleased; 

    private PlayerCardDisplay thisCard;

    private PhotonView RPCCardSelected;

    private void Awake()
    {
        //Set the original position of the card to its location in space to use a reference
        OriginalPosition = this.transform.position;
    }



    /// <summary>
    /// This method is ivoked when the mouse button is clicked
    /// </summary>
    public void OnMouseDown()
    {
        
        if (photonView.IsMine)
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

                cardClicked.transform.position = OriginalPosition;
                
                ShopCardClicked?.Invoke(cardClicked);

                //photon view of our current card
                RPCCardSelected = this.GetComponent<PhotonView>();
                this.photonView.RPC("RPCOnMouseDown", RpcTarget.Others, RPCCardSelected.ViewID);
            }
            else
            {
                CardDragged?.Invoke(GetComponent<PlayerCardDisplay>());
            }
        }
    }

    /// <summary>
    /// this method is called when the mouse button is released
    /// </summary>
    public void OnMouseUp()
    {
        if (photonView.IsMine)
        {
            CardReleased?.Invoke();
            //if there is a gameobject and the card is not in the play zone we will return the card to the original position
            if (this.gameObject != null && PlayZone.cardInPlayZone == false)
            {
                this.transform.position = OriginalPosition;
            }

            //photon view of our current card
            RPCCardSelected = this.GetComponent<PhotonView>();

            this.photonView.RPC("RPCOnMouseUp", RpcTarget.Others, RPCCardSelected.ViewID, this.transform.position);
        }

    }

    /// <summary>
    /// This method is called when the card is being dragged
    /// </summary>
    public void OnMouseDrag()
    {
        if (photonView.IsMine)
        {
            //if the card display has no hand container it means that the card is in the shop
            //if that is the case we do not want to drag the card. 
            if (this.transform.parent.gameObject.GetComponent<HandContainer>() == null)
            {
                return;
            }

            //Debug.Log("Attempting to drag and the object is draggable");

            //stores position of cursor in screen space
            Vector2 cursorScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //grabs the position of the mouse cursor and converts to world space
            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;

            //updates position of game object
            transform.position = cursorPosition;

            //current card
            thisCard = this.gameObject.GetComponent<PlayerCardDisplay>();

            //photon view of our current card
            RPCCardSelected = this.GetComponent<PhotonView>();

            //RPC call the current card photon ID and the changed position
            this.photonView.RPC("RPCOnMouseDrag", RpcTarget.Others, RPCCardSelected.ViewID, transform.position);
        }

    }

    [PunRPC]
    private void RPCOnMouseDown(int cardID)
    {
        PhotonView foundCard = PhotonView.Find(cardID);
        if (foundCard)
        {
            PlayerCardDisplay purchasedCard = foundCard.GetComponent<PlayerCardDisplay>();
            ShopCardClicked?.Invoke(purchasedCard);
        }
        else
        {
            Debug.Log("Photon View not found. CardID: " + cardID);
        }
    }

    [PunRPC]
    private void RPCOnMouseUp(int cardID, Vector3 position)
    {
        PhotonView foundCard = PhotonView.Find(cardID);
        if (foundCard)
        {
            this.transform.position = position;
        }
        else
        {
            Debug.Log("Photon View not found. CardID: " + cardID);
        }
    }

    [PunRPC]
    private void RPCOnMouseDrag(int cardID, Vector3 position)
    {
        //find the phototon view associted with the given ID
        PhotonView foundCard = PhotonView.Find(cardID);
        if (foundCard)
        {
            //update the position of the card
            foundCard.transform.position = position;
        }
        else
        {
            Debug.Log("Photon View not found. CardID: " + cardID);
        }
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
