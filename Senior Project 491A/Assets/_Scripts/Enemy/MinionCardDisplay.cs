using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

/// <summary>
/// Holds visual information specific to minion cards. Extends EnemyCardDisplay.
/// Class will load in text, sprites, card art, and values into the card display
/// this loads depending on the card attached to it. 
/// </summary>
//[ExecuteInEditMode]
public class MinionCardDisplay : EnemyCardDisplay
{
    private byte currentCardIdenrifier = (byte)'E';

    //Delegate event to handle anything that cares if the card has been destroyed
    public delegate void _cardDestroyed(EnemyCardDisplay destroytedCard);
    public static event _cardDestroyed CardDestroyed;
    
    //Delegate event to handle anything that cares if the card has been clicked
    public delegate void _MinionCardClicked(MinionCardDisplay cardClicked);

    public static event _MinionCardClicked MinionCardClicked;

    void Awake()
    {
        base.Awake();
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.AllocateViewID(photonView))
            {
                object[] data = { photonView.ViewID };

                RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                {
                    Receivers = ReceiverGroup.Others,
                    CachingOption = EventCaching.AddToRoomCache
                };

                SendOptions sendOptions = new SendOptions
                {
                    Reliability = true
                };

//                Debug.Log("MinionCard assigned ViewID: " + photonView.ViewID);
                PhotonNetwork.RaiseEvent(currentCardIdenrifier, data, raiseEventOptions, sendOptions);

                if (!PhotonNetwork.OfflineMode)
                {
                    if (!TurnManager.currentPhotonPlayer.IsMasterClient)
                    {
                        //Debug.Log("Master Client has assigned a PhotonView ID and is transfering ownership to other player...");
                        photonView.TransferOwnership(TurnManager.currentPhotonPlayer);
                    }
                }
            }
            else
            {
                Debug.Log("Unable to allocate ID");
            }
        }
    }
    public void OnEvent(EventData photonEvent)
    {

        byte recievedCode = photonEvent.Code;
        if (recievedCode == currentCardIdenrifier)
        {
            object[] data = (object[])photonEvent.CustomData;
            int recievedPhotonID = (int)data[0];

            if (!TurnManager.photonViewIDs.Contains(recievedPhotonID))
            {
                photonView.ViewID = recievedPhotonID;
                TurnManager.photonViewIDs.Add(recievedPhotonID);

                Debug.Log("MinionCard RPC to assign PhotonView ID: " + photonView.ViewID);
                PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
            }
        }
        else
        {
            //Debug.Log("Event code not found");
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    public void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }


    //This method will invoke the CardDestroyed event 
    protected override void OnDestroy()
    {
        Debug.Log("minion card was destroyed");
        base.OnDestroy();
        if(CardDestroyed != null)
            CardDestroyed.Invoke(this);
    }

    //This method will invoke the MinionCardClicked event
    protected override void OnMouseDown()
    {
        MinionCardClicked?.Invoke(this);
    }
}
