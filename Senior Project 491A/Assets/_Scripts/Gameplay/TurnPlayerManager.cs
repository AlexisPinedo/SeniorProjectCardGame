using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPlayerManager : MonoBehaviourPunCallbacks
{
    // Player References
    [SerializeField]
    private Player player1, player2,turnPlayer;

    public Player Player1
    {
        get { return player1; }
    }

    public Player Player2
    {
        get { return player2; }
    }
    
    public Player TurnPlayer
    {
        get { return turnPlayer; }
    }
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
        
        HandleInitialTurn();
    }

    private void HandleInitialTurn()
    {
        turnPlayer = player1;
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

}
