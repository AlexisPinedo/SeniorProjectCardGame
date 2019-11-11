using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UIElements;

public class PlayZone : MonoBehaviourPunCallbacks
{
    /* Triggers when a PlayerCard is dragged into the Play Zone */

    private static PlayZone _instance;

    public static PlayZone Instance
    {
        get => _instance;
    }

    public static bool cardInPlayZone = false;

    public static PlayerCardDisplay cardInZone;

    public delegate void _CardPlayed(PlayerCard cardPlayed);

    public static event _CardPlayed CardPlayed;
    public static event Action HasPlayed;

    private PhotonView RPCCardSelected;

    [SerializeField] private Transform enlargementZone;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (photonView.IsMine)
        //{
            if (other.transform.parent.gameObject.GetComponent<HandContainer>() == null)
            {
                return;
            }

            //Debug.Log("Card has entered");
            cardInPlayZone = true;
            if(other.gameObject.GetComponent<PlayerCardDisplay>() != null)
                cardInZone = other.gameObject.GetComponent<PlayerCardDisplay>();

            RPCCardSelected = cardInZone.GetComponent<PhotonView>();
            RPCCardSelected.RPC("RPCOnTriggerEnter2D", RpcTarget.Others, RPCCardSelected.ViewID);
        //}
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //if (photonView.IsMine)
        //{
            //Debug.Log("Card has left");
            cardInPlayZone = false;
            cardInZone = null;

            this.photonView.RPC("RPCOnTriggerExit2D", RpcTarget.Others);
       // }
    }

    private void Update()
    {
        if(TurnManager.currentPhotonPlayer != PhotonNetwork.LocalPlayer)
            return;
        
        if (cardInPlayZone)
        {
            if (!Input.GetMouseButton(0))
            {
                if (PhotonNetwork.OfflineMode)
                {
                    HandleCardPlayed();
                }
                else
                {
                    this.photonView.RPC("RPCPlayZoneUpdate", RpcTarget.All);
                }
            }
        }
    }
    

    private void HandleCardPlayed()
    {
        // Card stuff
        Hand tpHand = TurnManager.Instance.turnPlayer.hand;
        PlayerCardDisplay cardDisplay = cardInZone;
        PlayerCard cardPlayed = cardDisplay.card;

        StartCoroutine(TransformCard(cardInZone, enlargementZone.position));
        //Destroy(cardInZone.gameObject);
        
        TurnManager.Instance.turnPlayer.Power += cardPlayed.CardAttack;
        TurnManager.Instance.turnPlayer.Currency += cardPlayed.CardCurrency;

        if (!cardPlayed.CardName.Equals("Phantom"))
        {
            tpHand.hand.Remove(cardPlayed);
            TurnManager.Instance.turnPlayer.graveyard.graveyard.Add(cardPlayed);
        }

        CardPlayed?.Invoke(cardPlayed);
        HasPlayed?.Invoke();
        cardInPlayZone = false;
        cardInZone = null;
    }

    IEnumerator TransformCard(PlayerCardDisplay cardInPlay, Vector3 destination)
    {
        cardInPlay.transform.localScale = new Vector3(0, 0, 0);
        cardInPlay.transform.position = destination;
        
        float currentLerpTime = 0;
        float lerpTime = 0.3f;

        //Vector3 startPos = cardInPlay.transform.position;
        Vector3 startSize = cardInPlay.transform.localScale;
        Vector3 endSize = cardInPlay.transform.localScale + new Vector3(1.5f, 1.5f, 1.5f);


        while (cardInPlay.transform.localScale != endSize)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float Perc = currentLerpTime / lerpTime;

            //cardInPlay.transform.position = Vector3.Lerp(startPos, destination, Perc);
            cardInPlay.transform.localScale = Vector3.Lerp(startSize, endSize, Perc);

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1.0f);

        Destroy(cardInPlay.gameObject);

        //cardDisplay.GetComponent<CardZoomer>().OriginalPosition = cardDestination;
        //cardDisplay.GetComponent<DragCard>().OriginalPosition = cardDestination;

    }

    [PunRPC]
    private void RPCOnTriggerEnter2D(int cardID)
    {
        PhotonView foundCard = PhotonView.Find(cardID);
        if (foundCard)
        {
            cardInPlayZone = true;
            if (foundCard.GetComponent<PlayerCardDisplay>() != null)
            {
                cardInZone = foundCard.GetComponent<PlayerCardDisplay>();
            }
            else
            {
                Debug.Log("Card container was null");
            }
        }
        else
        {
            Debug.Log("Photon View not found. CardID: " + cardID);
        }
    }

    [PunRPC]
    private void RPCOnTriggerExit2D()
    {

        cardInPlayZone = false;
        cardInZone = null;

    }

    [PunRPC]
    private void RPCPlayZoneUpdate()
    {
        HandleCardPlayed();
        //HasPlayed?.Invoke();
    }

}
