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

    public delegate void _TurnCounterUpdated();

    public static event _TurnCounterUpdated TurnCounterUpdated;

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

    private void OnEnable()
    {
        PlayZone.CardPlayed += AddCardToHistory;
        TurnManager.PlayerSwitched += HandleTurnEnding;
    }

    private void OnDisable()
    {
        PlayZone.CardPlayed -= AddCardToHistory;
        TurnManager.PlayerSwitched -= HandleTurnEnding;
    }

    public void AddCardToHistory(PlayerCard cardToAdd)
    {
        Debug.Log("Card Added");
        playerCardHistory.Add(cardToAdd);
        EntireCardHistory.Add(cardToAdd);
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
        TurnCounterUpdated?.Invoke();
    }

    //public void ClearHistory()
    //{
    //    playerCardHistory.Clear();
    //}
    ///*
    // * This method will return the last element in the list if it is not empty
    // */
    //public  PlayerCard GetLastCardPlayed()
    //{

    //    if (playerCardHistory.Count == 0)
    //    {
    //        Debug.Log("list is Empty");
    //        return null;
    //    }

    //    LastCardPlayed = playerCardHistory[playerCardHistory.Count - 1];

    //    return LastCardPlayed;
    //}

    ///*
    // * This method will validate is the last card played with the parameter
    // * if both cards share the same card type it will return true
    // */
    // public  bool ValidateSameType(PlayerCard ConditionType)
    //{
    //    if (playerCardHistory.Count == 0)
    //    {
    //        Debug.Log("list is Empty");
    //        return false;
    //    }

    //    LastCardPlayed = playerCardHistory[playerCardHistory.Count - 1];
    //    if (LastCardPlayed.cardType == ConditionType.cardType)
    //        return true;
    //    else
    //        return false;
    //}

    ///*
    // * This method will validate the last three cards played and check their attributes
    // * if the are all the same card type it will return true
    // */
    // public  bool ValidateLastThreeCardsPlayed(PlayerCard ConditionType)
    //{
    //    if (playerCardHistory.Count <= 2)
    //    {
    //        Debug.Log("list is not big enough");
    //        return false;
    //    }

    //    LastCardPlayed = playerCardHistory[playerCardHistory.Count - 1];
    //    PlayerCard CardPlayedBeforeLast = playerCardHistory[playerCardHistory.Count - 2];
    //    if (ConditionType.cardType == LastCardPlayed.cardType && ConditionType.cardType == CardPlayedBeforeLast.cardType)
    //        return true;
    //    else
    //        return false;

    //}

}
