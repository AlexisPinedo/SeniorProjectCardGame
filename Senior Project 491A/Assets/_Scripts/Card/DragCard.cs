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

    private bool offline;

    private PlayerCardDisplay thisCard;

    private PhotonView draggedCard;

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

        //draggedCard = this.GetComponent<PhotonView>();
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
            ShopCardClicked?.Invoke(cardClicked);
        }

        //thisCard = this.gameObject.GetComponent<PlayerCardDisplay>();
        //Debug.Log("Mouse down: " + thisCard.card.CardName);
        //photonView.RPC("RPCOnMouseDown", RpcTarget.Others);
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
        //thisCard = this.gameObject.GetComponent<PlayerCardDisplay>();
        //Debug.Log("Mouse up: " + thisCard.card.CardName);
        //photonView.RPC("RPCOnMouseUp", RpcTarget.Others, thisCard, this.transform.position);

       // draggedCard.RPC("RPCOnMouseUp", RpcTarget.Others, draggedCard.ViewID, this.transform.position);

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
        //transform.position = cursorPosition;

        //thisCard = this.gameObject.GetComponent<PlayerCardDisplay>();
        //Debug.Log("Mouse drag: " + thisCard.card.CardName);

        //photonView.RPC("RPCOnMouseDrag", RpcTarget.Others, thisCard, transform.position);

        draggedCard = this.GetComponent<PhotonView>();

        Debug.Log(draggedCard.ViewID);

        this.photonView.RPC("RPCOnMouseDrag", RpcTarget.Others, draggedCard.ViewID);
    }

    //[PunRPC]
    //private void RPCOnMouseDown(PlayerCardDisplay currentCard, Vector2 position)
    //{
    //    ShopCardClicked?.Invoke(cardClicked);
    //    thisCard = this.gameObject.GetComponent<PlayerCardDisplay>();
    //    Debug.Log("Mouse down: " + thisCard.card.CardName);
    //}

    //[PunRPC]
    //private void RPCOnMouseUp(int cardID, Vector2 position)
    //{
    //    PhotonView foundCard = PhotonView.Find(cardID);
    //    if (foundCard)
    //    {
    //        this.transform.position = position;
    //    }
    //    else
    //    {
    //        Debug.Log("Photon View not found. CardID: " + cardID);
    //    }
    //}

    [PunRPC]
    private void RPCOnMouseDrag(int cardID)
    {

        Debug.Log("Photon View not found. CardID: " + cardID);
        //PhotonView foundCard = PhotonView.Find(cardID);
        //if (foundCard)
        //{
        //    this.transform.position = position;
        //}
        //else
        //{
        //    Debug.Log("Photon View not found. CardID: " + cardID);
        //}
        //currentCard.transform.position = position;
        //Debug.Log("Mouse drag: " + currentCard.card.CardName);
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
