using System;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// This class handles the dragging of cards. When a card is clicked it will follow the mouse and drag with it. 
/// </summary>
public class DragCard : MonoBehaviourPun
{
    private Vector2 offset;
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

    /// <summary>
    /// This method is ivoked when the mouse button is clicked
    /// </summary>
    public void OnMouseDown()
    {
        //Debug.Log("card has been clicked");
        //photon view of our current card
        if (photonView.IsMine)
        {
            //Here we add the offset from the card and the mouse
            offset = transform.position - Camera.main.ScreenToWorldPoint(
                         new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            //Debug.Log("Running Handling card clicked method");

            HandleCardClicked(offset);
            if(!PhotonNetwork.OfflineMode) 
                photonView.RPC("CardDraggedClicked", RpcTarget.Others, offset);
        }
    }

    [PunRPC]
    private void CardDraggedClicked(Vector2 position)
    {
        HandleCardClicked(position);
    }
    
    /// <summary>
    /// this method is called when the mouse button is released
    /// </summary>
    public void OnMouseUp()
    {
        HandleCardRelease();
        if(!PhotonNetwork.OfflineMode)
            if (photonView.IsMine)
                photonView.RPC("CardDraggedReleased", RpcTarget.Others);
    }

    [PunRPC]
    private void CardDraggedReleased()
    {
        HandleCardRelease();
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
            Vector2 cursorPosition = (Vector2)Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;

            MoveCardWithCursor(cursorPosition);
            if(!PhotonNetwork.OfflineMode)
                photonView.RPC("DraggingCard", RpcTarget.Others, cursorPosition);
        }
    }

    [PunRPC]
    private void DraggingCard(Vector2 position)
    {
        MoveCardWithCursor(position);
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
    
    private void MoveCardWithCursor(Vector2 position)
    {
        //if the card display has no hand container it means that the card is in the shop
        //if that is the case we do not want to drag the card. 
        if (transform.parent.gameObject.GetComponent<HandContainer>() == null)
        {
            return;
        }
        //updates position of game object
        //transform.position = position;
        if(photonView.IsMine)
            transform.position = Vector2.Lerp(transform.position, position, .25f);
        else
        {
            transform.position = Vector2.Lerp(transform.position, position, .025f);
        }
    }
    
    private void HandleCardClicked(Vector2 RPCoffset)
    {
        cardHeld = true;

        //Here we add the offset from the card and the mouse
        offset = RPCoffset;

        //if the card display has no hand container it means that the card is in the shop
        //We can use this to our advantage by adding shop functionality here

        //Debug.Log("Card is in Shop");
        PlayerCardDisplay cardClicked = this.gameObject.GetComponent<PlayerCardDisplay>();

        if (transform.parent.gameObject.GetComponent<HandContainer>() == null)
        {
            cardClicked.transform.position = OriginalPosition;

            ShopCardClicked?.Invoke(cardClicked);

            cardHeld = false;
        }
        else
        {
            CardDragged?.Invoke(cardClicked);

            //Debug.Log("particle effect call...");
        }
    }
}