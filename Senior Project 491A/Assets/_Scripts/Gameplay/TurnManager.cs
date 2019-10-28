using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviourPunCallbacks
{
    // Player References
    public Player turnPlayer;

    public static Photon.Realtime.Player photonPlayer1, photonPlayer2, currentPhotonPlayer;

    public GameObject turnPlayerGameObject;

    [SerializeField]
    private GameObject player1GameObject;

    [SerializeField]
    private GameObject player2GameObject;

    [SerializeField]
    private HandContainer p1Handcontainer, p2HandContainer;

    [SerializeField]
    private ShopContainer shopContainer;

    [SerializeField]
    private FieldContainer minionContainer;

    [SerializeField]
    private BossCardDisplay bossCard;

    [SerializeField]
    private PlayZone playZone;

    [SerializeField]
    private GameObject cavnas;

    private PhotonView p1HandcontainerPV, p2HandContainerPV, shopContainerPV, minionContainerPV, playZonePV, canvasPV, bossCardPV;

    public Player player1;

    public Player player2;

    public delegate void _PlayerSwitched();
    public static event _PlayerSwitched PlayerSwitched;

    public delegate void _goingToSwitchPlayer();
    public static event _goingToSwitchPlayer GoingToSwitchPlayer;

    public delegate void _effectSwitchedPlayer();
    public static event _effectSwitchedPlayer EffectSwitchedPlayer;

    public static List<int> photonViewIDs = new List<int>();

    private static TurnManager _instance;

    public static TurnManager Instance
    {
        get { return _instance; }
    }

    private void OnEnable()
    {
        UIHandler.EndTurnClicked += ChangeActivePlayer;
    }

    private void OnDisable()
    {
        UIHandler.EndTurnClicked -= ChangeActivePlayer;
    }

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

        player2GameObject.SetActive(false);
        turnPlayer = player1;
        turnPlayerGameObject = player1GameObject;

        //grab photon views from game objects
        p1HandcontainerPV = p1Handcontainer.GetComponent<PhotonView>();
        p2HandContainerPV = p2HandContainer.GetComponent<PhotonView>();
        shopContainerPV = shopContainer.GetComponent<PhotonView>();
        minionContainerPV = minionContainer.GetComponent<PhotonView>();
        bossCardPV = bossCard.GetComponent<PhotonView>();
        playZonePV = playZone.GetComponent<PhotonView>();
        canvasPV = cavnas.GetComponent<PhotonView>();

        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Photon is online...");

            //assign player 1 and player 2 for referencee
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (player.IsMasterClient)
                    photonPlayer1 = player;
                else
                    photonPlayer2 = player;
            }

            //current player
            currentPhotonPlayer = photonPlayer1;

            Debug.Log("Photon Player 1: " + photonPlayer1.NickName);
            Debug.Log("Photon Player 2: " + photonPlayer2.NickName);

            p1HandcontainerPV.TransferOwnership(photonPlayer1);
            p2HandContainerPV.TransferOwnership(photonPlayer2);
            shopContainerPV.TransferOwnership(photonPlayer1);
            minionContainerPV.TransferOwnership(photonPlayer1);
            bossCardPV.TransferOwnership(photonPlayer1);
            playZonePV.TransferOwnership(photonPlayer1);
            canvasPV.TransferOwnership(photonPlayer1);
        }
        else
        {
            //enabling offline mode allows all photon code to be ignored
            PhotonNetwork.OfflineMode = true;
            currentPhotonPlayer = PhotonNetwork.LocalPlayer;
            Debug.Log("Photon is offline...");
        }


    }

    private void Start()
    {
        //PlayerSwitched?.Invoke();
    }

    public void QuickChangeActivePlayer()
    {
        SwapPlayers();
        EffectSwitchedPlayer?.Invoke();
    }

    public void ChangeActivePlayer()
    {
        if (player1GameObject != null && player2GameObject != null)
        {
            turnPlayer.Currency = 0;
            turnPlayer.Power = 0;
            
            GoingToSwitchPlayer?.Invoke();
            
            SwapPlayers();

            PlayerSwitched?.Invoke();

            TextUpdate.Instance.UpdateCurrency();
            TextUpdate.Instance.UpdatePower();
        }
    }

    private void SwapPlayers()
    {
        // Switch to Player Two
        if (player1GameObject.activeSelf)
        {
            player1GameObject.SetActive(false);
            player2GameObject.SetActive(true);
            player2GameObject.GetComponentInChildren<HandContainer>().enabled = true;
            turnPlayer = player2;

            Debug.Log("SwapPlayers() to player 2");

            turnPlayerGameObject = player2GameObject;
        }
        // Switch to Player One
        else if (player2GameObject.activeSelf)
        {
            player2GameObject.SetActive(false);
            player1GameObject.SetActive(true);
            turnPlayer = player1;

            Debug.Log("SwapPlayers() to player 1");

            turnPlayerGameObject = player1GameObject;
        }

        TransferObjects();
    }

    public void TransferObjects()
    {
        if (PhotonNetwork.OfflineMode)
            return;

        // transfer photon view ownership to player 1
        if (turnPlayer == player1)
            currentPhotonPlayer = photonPlayer1;
        // transfer photon view ownership to player 2
        else if (turnPlayer == player2)
            currentPhotonPlayer = photonPlayer2;
        else
            Debug.Log("Current player instance not found...");

        Debug.Log("Current player to transfer ownership: " + currentPhotonPlayer.NickName);

        shopContainerPV.TransferOwnership(currentPhotonPlayer);
        playZonePV.TransferOwnership(currentPhotonPlayer);
        canvasPV.TransferOwnership(currentPhotonPlayer);
        bossCardPV.TransferOwnership(currentPhotonPlayer);
        //grab all shop cards and transfer to current player
        PhotonView[] shopCards = shopContainerPV.GetComponentsInChildren<PhotonView>();
        foreach (PhotonView shopCard in shopCards)
            shopCard.TransferOwnership(currentPhotonPlayer);
        //grab all minion cards and transfer to current player
        PhotonView[] minionCards = minionContainerPV.GetComponentsInChildren<PhotonView>();
        foreach (PhotonView minionCard in minionCards)
            minionCard.TransferOwnership(currentPhotonPlayer);
    }
}
