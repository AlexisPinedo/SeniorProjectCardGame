using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class History : MonoBehaviour
{
    private static History _instance;

    public static History Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField]
    private List<PlayerCard> playerCardHistory = new List<PlayerCard>();

    public List<PlayerCard> PlayerCardHistory
    {
        get { return playerCardHistory; }
    }

    [SerializeField]
    private List<PlayerCard> EntireCardHistory = new List<PlayerCard>();

    [SerializeField] private int turnCount = 0;

    public int TurnCount => turnCount;

    public delegate void _cardHistoryComponentsUpdated();

    public static event _cardHistoryComponentsUpdated CardHistoryComponentsUpdated;

    public delegate void _cardAddedToHistory();
    
    public static event _cardAddedToHistory CardAddedToHistory;

    //private PlayerCard LastCardPlayed;

    private void Awake()
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

    private void Start()
    {
        HandleTurnEnding();
    }

    private void OnEnable()
    {
        PlayZone.CardPlayed += AddCardToHistory;
        TurnPhaseManager.PlayerTurnEnded += HandleTurnEnding;
    }

    private void OnDisable()
    {
        PlayZone.CardPlayed -= AddCardToHistory;
        TurnPhaseManager.PlayerTurnEnded -= HandleTurnEnding;
    }

    public void AddCardToHistory(PlayerCard cardToAdd)
    {
        //Debug.Log("Card Added");
        playerCardHistory.Add(cardToAdd);
        EntireCardHistory.Add(cardToAdd);
        
        CardAddedToHistory?.Invoke();
    }

    private void HandleTurnEnding()
    {
        ClearPlayerHistory();
        IncrementTurnCounter();
    }

    public void ClearPlayerHistory()
    {
        playerCardHistory.Clear();
    }

    public void IncrementTurnCounter()
    {
        turnCount++;
        CardHistoryComponentsUpdated?.Invoke();
    }
}
