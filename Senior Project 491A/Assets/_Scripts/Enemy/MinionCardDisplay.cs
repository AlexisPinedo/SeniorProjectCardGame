using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;

/// <summary>
/// Holds visual information specific to minion cards. Extends EnemyCardDisplay.
/// Class will load in text, sprites, card art, and values into the card display
/// this loads depending on the card attached to it. 
/// </summary>
//[ExecuteInEditMode]
public class MinionCardDisplay : EnemyCardDisplay<MinionCard>
{
    //private byte currentCardIdenrifier = (byte)'E';

    //Delegate event to handle anything that cares if the card has been destroyed
    public static event Action<MinionCardDisplay> CardDestroyed;

    //Delegate event to handle anything that cares if the card has been clicked
    public static event Action<MinionCardDisplay> MinionCardClicked;

    public Action MinionCardSummoned;

    public Action MinionCardDestroyed; 
    
    public Vector2 OriginalPosition;

    void Awake()
    {
        base.Awake();
        OriginalPosition = this.transform.position;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        TurnPhaseManager.BattlePhaseStarted += EnableBoxCollider;
        TurnPhaseManager.BattlePhaseEnded += DisableBoxCollider;
        Event_Base.DisableMinionCards += DisableBoxCollider;
        MinionCardSummoned?.Invoke();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        TurnPhaseManager.BattlePhaseStarted -= EnableBoxCollider;
        TurnPhaseManager.BattlePhaseEnded -= DisableBoxCollider;
        Event_Base.DisableMinionCards += DisableBoxCollider;
    }

    //This method will invoke the CardDestroyed event 
    protected override void OnDestroy()
    {
        Debug.Log("minion card was destroyed");
        base.OnDestroy();
        CardDestroyed?.Invoke(this);
    }

    private void OnMouseEnter()
    {
        //Debug.Log("enter");
        transform.localScale = new Vector2(1.5F, 1.5F); //zooms in the object
        Vector2 newPosition = new Vector2(0, -1);
        transform.position = new Vector2(newPosition.x + OriginalPosition.x, newPosition.y + OriginalPosition.y);
    }
    
    //This method will invoke the MinionCardClicked event
    protected override void OnMouseDown()
    {
        Debug.Log("Minion card clicked");
        MinionCardClicked?.Invoke(this);
        if(!PhotonNetwork.OfflineMode)
            photonView.RPC("RPCAttackMinion", RpcTarget.Others, this.photonView.ViewID);
    }

    private void OnMouseExit()
    {
        transform.localScale = new Vector2(1, 1);  //returns the object to its original state
        if (!DragCard.cardHeld)
            transform.position = OriginalPosition;
    }

    [PunRPC]
    private void RPCAttackMinion(int cardID)
    {
        PhotonView foundCard = PhotonView.Find(cardID);
        if (foundCard)
        {
            MinionCardDisplay attackedMinion = foundCard.GetComponent<MinionCardDisplay>();
            MinionCardClicked?.Invoke(attackedMinion);
            Debug.Log("Minion Attacked");
        }
        else
        {
            Debug.Log("Photon View not found. CardID: " + cardID);
        }
    }


}
