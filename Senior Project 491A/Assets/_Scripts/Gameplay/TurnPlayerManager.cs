using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager game object for deciding which Player has the active turn.
/// </summary>
public class TurnPlayerManager : MonoBehaviourPunCallbacks
{
    // Heroes!
    [SerializeField] private Valor valor;
    [SerializeField] private Vann van;
    [SerializeField] private Vaughn vaughn;
    [SerializeField] private Veda veda;
    [SerializeField] private Vicky vicky;
    [SerializeField] private Vito vito;

    // Player References
    [SerializeField]
    private Player player1, player2, turnPlayer;
    public Player Player1 { get { return player1; } }
    public Player Player2 { get { return player2; } }
    /// <summary>
    /// The Player who is actively taking their turn.
    /// </summary>
    public Player TurnPlayer { get { return turnPlayer; } }

    [SerializeField]
    private GameObject turnPlayerGameObject;
    public GameObject TurnPlayerGameObject
    {
        get { return turnPlayerGameObject; }
    }

    [SerializeField]
    private GameObject player1GameObject, player2GameObject;
    public static event Action PlayerSwitched;
    public static event Action GameStarted;

    private static TurnPlayerManager _instance;
    public static TurnPlayerManager Instance
    {
        get { return _instance; }
    }

    private void OnEnable()
    {
        TurnPhaseManager.PrePlayerPhase += ChangeActivePlayer;
    }

    private void OnDisable()
    {
        TurnPhaseManager.PrePlayerPhase -= ChangeActivePlayer;
    }

    void Awake()
    {
        if (_instance == null && _instance != this)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Heroes p1Hero;
        if (!PhotonNetwork.OfflineMode)
        {
            Heroes p2Hero;

            if (PhotonNetwork.CurrentRoom.CustomProperties["playerOneHero"] != null)
            {
                p1Hero = (Heroes)PhotonNetwork.CurrentRoom.CustomProperties["playerOneHero"];
                player1.SelectedHero = GetHero(p1Hero);
            }
            if (PhotonNetwork.CurrentRoom.CustomProperties["playerTwoHero"] != null)
            {
                p2Hero = (Heroes)PhotonNetwork.CurrentRoom.CustomProperties["playerTwoHero"];
                player2.SelectedHero = GetHero(p2Hero);
            }
        }
        else
        {
            p1Hero = (Heroes)HeroPicker.offlineSelectedHero;
            player1.SelectedHero = GetHero(p1Hero);
        }

        HandleInitialTurn();
    }

    private void HandleInitialTurn()
    {
        turnPlayer = player1;

        if (player2GameObject != null)
            player2GameObject.SetActive(false);

        turnPlayerGameObject = player1GameObject;
        GameStarted?.Invoke();
    }

    public void ChangeActivePlayer()
    {
        if (player1GameObject != null && player2GameObject != null)
        {
            SwapPlayers();
            PlayerSwitched?.Invoke();
            if (TurnPhaseManager.CurrentPhase == Phases.StartPhase)
            {
                turnPlayer.Currency = 0;
                turnPlayer.Power = 0;
                TextUpdate.Instance.UpdateCurrency();
                TextUpdate.Instance.UpdatePower();
            }
        }
        else if (player1GameObject != null)
        {
            if (TurnPhaseManager.CurrentPhase == Phases.StartPhase)
            {
                turnPlayer.Currency = 0;
                turnPlayer.Power = 0;
                TextUpdate.Instance.UpdateCurrency();
                TextUpdate.Instance.UpdatePower();
            }
        }
    }

    private void SwapPlayers()
    {
        // Switch to Player Two
        if (player1GameObject.activeSelf)
        {
            player1GameObject.SetActive(false);
            player2GameObject.SetActive(true);
            turnPlayer = player2;
            NetworkOwnershipTransferManger.currentPhotonPlayer = NetworkOwnershipTransferManger.photonPlayer2;
            NetworkOwnershipTransferManger.pendingPhotonPlayer = NetworkOwnershipTransferManger.photonPlayer1;
            turnPlayerGameObject = player2GameObject;
        }
        // Switch to Player One
        else if (player2GameObject.activeSelf)
        {
            player2GameObject.SetActive(false);
            player1GameObject.SetActive(true);
            turnPlayer = player1;
            NetworkOwnershipTransferManger.currentPhotonPlayer = NetworkOwnershipTransferManger.photonPlayer1;
            NetworkOwnershipTransferManger.pendingPhotonPlayer = NetworkOwnershipTransferManger.photonPlayer2;
            turnPlayerGameObject = player1GameObject;
        }
    }

    private Hero GetHero(Heroes heroPicked)
    {
        Hero selectedHero = null;

        switch (heroPicked)
        {
            case Heroes.Valor:
                selectedHero = valor;
                break;
            case Heroes.Vann:
                selectedHero = van;
                break;
            case Heroes.Vaughn:
                selectedHero = vaughn;
                break;
            case Heroes.Veda:
                selectedHero = veda;
                break;
            case Heroes.Vicky:
                selectedHero = vicky;
                break;
            case Heroes.Vito:
                selectedHero = vito;
                break;
            default:
                selectedHero = null;
                break;
        }

        return selectedHero;
    }
}
