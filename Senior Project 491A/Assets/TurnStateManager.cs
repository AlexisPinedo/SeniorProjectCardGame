using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnStateManager : MonoBehaviour
{
    private bool battePhase, playPhase = false;

    public static event Action PlayerTurnStarted; 
    public static event Action PlayerTurnEnded;
    public static event Action PlayPhaseStarted;
    public static event Action PlayPhaseEnded;
    public static event Action BattlePhaseStarted;
    public static event Action BattlePhaseEnded;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
    private void StartPlayerTurn()
    {
        HandlePlayerTurnState();
    }
    private void StartBattlePhase()
    {
        playPhase = false;
        battePhase = true;
    }

    private void EndBattlePhase()
    {
        playPhase = false;
        battePhase = false;
    }
    private void StartPlayPhase()
    {
        playPhase = true;
        battePhase = false;
    }
    private void EndPlayPhase()
    {
        battePhase = false;
        playPhase = false;
    }
    

    IEnumerator HandlePlayerTurnState()
    {
        PlayerTurnStarted?.Invoke();
        Debug.Log("Beginning Shop Play phase");
        PlayPhaseStarted?.Invoke();
        while (playPhase)
        {
            yield return null;
        }
        PlayPhaseEnded?.Invoke();
        Debug.Log("Beginning Battle Phase");
        BattlePhaseStarted?.Invoke();
        while (battePhase)
        {
            yield return null;
        }
        BattlePhaseEnded?.Invoke();
        PlayerTurnEnded?.Invoke();
    }
}
