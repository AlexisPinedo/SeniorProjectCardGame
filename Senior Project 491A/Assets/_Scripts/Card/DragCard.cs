using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class DragCard : MonoBehaviourPunCallbacks
{
    private Vector3 offset;
    private Vector3 screenPoint;
    public Vector2 OriginalPosition;

    public delegate void _ShopCardClicked(PlayerCardHolder cardClicked);

    public static event _ShopCardClicked ShopCardClicked;

    private bool offline;

    private void Awake()
    {
        OriginalPosition = this.transform.position;
        offline = PhotonNetworkManager.IsOffline;
        Debug.Log(this.name + " from DragCard is owned by " + this.photonView.OwnerActorNr);


        if (!offline)
        {
            if (this.photonView.Owner != PhotonNetwork.MasterClient)
            {
                Debug.Log("From DragCard.cs, transfering card ownership to Master Client");
                this.photonView.TransferOwnership(PhotonNetwork.MasterClient);
            }
        }
    }

    public void OnMouseDown()
    {
        if (offline || photonView.IsMine)
        {
            if (this.transform.parent.gameObject.GetComponent<HandContainer>() == null)
            {
                //Debug.Log("Card is in Shop");
                PlayerCardHolder cardClicked = this.gameObject.GetComponent<PlayerCardHolder>();
                ShopCardClicked?.Invoke(cardClicked);

                if(!offline)
                    photonView.RPC("RPCOnMouseDown", RpcTarget.Others, cardClicked);
            }

            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position); //used to grab the z coordinate of the game object 

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(
                         new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    public void OnMouseUp()
    {
        if (offline || photonView.IsMine)
        {
            if (this.gameObject != null && PlayZone.cardInPlayZone == false)
            {
                this.transform.position = OriginalPosition;
                if (!offline)
                    photonView.RPC("RPCOnMouseUp", RpcTarget.Others, OriginalPosition);
            }
        }
    }

    public void OnMouseDrag()
    {
        if (offline || photonView.IsMine)
        {
            if (this.transform.parent.gameObject.GetComponent<HandContainer>() == null)
            {
                return;
            }

            //Debug.Log("Attempting to drag and the object is draggable");

            UnityEngine.Vector2 cursorScreenPoint = new UnityEngine.Vector2(Input.mousePosition.x, Input.mousePosition.y); //stores position of cursor in screen space
            UnityEngine.Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset; //grabs the position of the mouse cursor and converts to world space

            transform.position = cursorPosition; //updates position of game object
            if (!offline)
                photonView.RPC("RPCOnMouseDrag", RpcTarget.Others, cursorPosition);
        }
    }

    [PunRPC]
    private void RPCOnMouseDown(PlayerCardHolder cardClicked)
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
