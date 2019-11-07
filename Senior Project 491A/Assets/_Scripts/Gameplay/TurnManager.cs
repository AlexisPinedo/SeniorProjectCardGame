using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviourPunCallbacks
{
    // Player References
    public Player player1, player2, turnPlayer;

    public static Photon.Realtime.Player photonPlayer1, photonPlayer2, currentPhotonPlayer;

    public GameObject turnPlayerGameObject;

    [SerializeField]
    private GameObject player1GameObject, player2GameObject;

    [SerializeField]
    private ShopContainer shopCards;

    [SerializeField]
    private FieldContainer minionCards;

    [SerializeField]
    private BossCardDisplay bossCard;

    public delegate void _PlayerSwitched();
    public static event _PlayerSwitched PlayerSwitched;

    public delegate void _goingToSwitchPlayer();
    public static event _goingToSwitchPlayer GoingToSwitchPlayer;

    public delegate void _effectSwitchedPlayer();
    public static event _effectSwitchedPlayer EffectSwitchedPlayer;

    public static List<int> photonViewIDs = new List<int>();

    public static byte endTurnEvent = (byte)'E';

    public static byte startBattleEvent = (byte)'S';

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

            currentPhotonPlayer = photonPlayer1;

            Debug.Log("Photon Player 1: " + photonPlayer1.NickName);
            Debug.Log("Photon Player 2: " + photonPlayer2.NickName);
        }
        else
        {
            //enabling offline mode allows all photon code to be ignored
            PhotonNetwork.OfflineMode = true;
            currentPhotonPlayer = photonPlayer1 = photonPlayer2 = PhotonNetwork.LocalPlayer;
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
            currentPhotonPlayer = photonPlayer2;
            turnPlayerGameObject = player2GameObject;
        }
        // Switch to Player One
        else if (player2GameObject.activeSelf)
        {
            player2GameObject.SetActive(false);
            player1GameObject.SetActive(true);
            turnPlayer = player1;
            currentPhotonPlayer = photonPlayer1;
            turnPlayerGameObject = player1GameObject;
        }

        if (!PhotonNetwork.OfflineMode)
            TransferObjects();
    }

    public void TransferObjects()
    {
        bossCard.photonView.TransferOwnership(currentPhotonPlayer);

        PhotonView[] minionTransfer = minionCards.GetComponentsInChildren<PhotonView>();
        for (int i = 0; i < minionTransfer.Length; i++)
            minionTransfer[i].TransferOwnership(currentPhotonPlayer);

        PhotonView[] shopTransfer = shopCards.GetComponentsInChildren<PhotonView>();
        for (int i = 0; i < shopTransfer.Length; i++)
            shopTransfer[i].TransferOwnership(currentPhotonPlayer);

        //Alternate appraoches for transfer

        //MinionCardDisplay[] views = FindObjectsOfType<MinionCardDisplay>();
        //foreach (MinionCardDisplay card in views)
        //    card.photonView.TransferOwnership(currentPhotonPlayer);

        //foreach (PhotonView currentCard in minionTransfer)
        //    currentCard.TransferOwnership(currentPhotonPlayer);

        //foreach (PhotonView currentCard in shopTransfer)
        //    currentCard.TransferOwnership(currentPhotonPlayer);
    }
}
