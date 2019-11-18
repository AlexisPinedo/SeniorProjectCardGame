using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Holds visual information specific to boss cards. Extends EnemyCardDisplay.
/// Class will load in text, sprites, card art, and values into the card display
/// this loads depending on the card attached to it. 
/// </summary>
//[ExecuteInEditMode]
public class BossCardDisplay : EnemyCardDisplay<BossCard>
{
    //We create a delegate event for the boss card to handle what happens when clicked
    public delegate void _BossCardClicked(BossCardDisplay cardClicked);

    public static event _BossCardClicked BossCardClicked;
    
    public Vector2 OriginalPosition;

    protected override void Awake()
    {
        base.Awake();
        this.enabled = true;
        OriginalPosition = this.transform.position;
    }

    private void OnMouseEnter()
    {
        //Debug.Log("enter");
        transform.localScale = new Vector2(1.5F, 1.5F); //zooms in the object
        Vector2 newPosition = new Vector2(0, -1);
        transform.position = new Vector2(newPosition.x + OriginalPosition.x, newPosition.y + OriginalPosition.y);
    }

    //this handles the boss card being clicked 
    protected override void OnMouseDown()
    {
        Debug.Log("boss has been clicked");
        BossCardClicked?.Invoke(this);
        this.photonView.RPC("RPCAttackBoss", RpcTarget.Others, this.photonView.ViewID);
    }
    
    private void OnMouseExit()
    {
        transform.localScale = new Vector2(1, 1);  //returns the object to its original state
        if (!DragCard.cardHeld)
            transform.position = OriginalPosition;
    }

    [PunRPC]
    private void RPCAttackBoss(int cardID)
    {
        PhotonView foundCard = PhotonView.Find(cardID);
        if (foundCard)
        {
            BossCardDisplay purchasedCard = foundCard.GetComponent<BossCardDisplay>();
            BossCardClicked?.Invoke(this);
            Debug.Log("boss has been clicked");
        }
        else
        {
            Debug.Log("Photon View not found. CardID: " + cardID);
        }
    }
}
