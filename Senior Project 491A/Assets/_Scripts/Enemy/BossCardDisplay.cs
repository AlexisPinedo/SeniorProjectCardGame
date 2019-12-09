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

    [SerializeField] private SelectedBoss BossToLoad;
    
    protected override void Awake()
    {
        card = BossToLoad.SelectedBossCard;
        base.Awake();
        enabled = true;
        card.EnableGoal();
    }

    //this handles the boss card being clicked 
    protected override void OnMouseDown()
    {
        Debug.Log("boss has been clicked");
        BossCardClicked?.Invoke(this);
        if(!PhotonNetwork.OfflineMode)
            photonView.RPC("RPCAttackBoss", RpcTarget.Others, this.photonView.ViewID);
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
