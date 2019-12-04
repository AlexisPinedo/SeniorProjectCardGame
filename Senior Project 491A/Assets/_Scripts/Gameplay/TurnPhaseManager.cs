using System;
using System.Collections;
using UnityEngine;

public class TurnPhaseManager : MonoBehaviour
{
    private static TurnPhaseManager _instance;

    public static TurnPhaseManager Instance
    {
        get => _instance;
    }

    private bool battePhaseOccuring, playPhaseOccuring, endPhaseTriggered = false;

    private static Phases currentPhase;

    public static Phases CurrentPhase
    {
        get => currentPhase;
    }

    public static event Action PlayerTurnStarted;
    public static event Action EndingPlayerTurn; 
    public static event Action PlayerTurnEnded;
    public static event Action PlayPhaseStarted;
    public static event Action PlayPhaseEnded;
    public static event Action BattlePhaseStarted;
    public static event Action BattlePhaseEnded;
    public static event Action PrePlayerPhase;
  

    private void Awake()
    {
        if (_instance == null == _instance != this)
            _instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        TurnPlayerManager.GameStarted += StartPlayerTurn;
        UIHandler.StartBattleClicked += StartBattlePhase;
        UIHandler.EndTurnClicked += EndPlayerTurn;
        BossTurnManager.BossTurnEnded += StartPlayerTurn;
    }

    private void OnDisable()
    {
        TurnPlayerManager.GameStarted -= StartPlayerTurn;
        UIHandler.StartBattleClicked -= StartBattlePhase;
        UIHandler.EndTurnClicked -= EndPlayerTurn;
        BossTurnManager.BossTurnEnded -= StartPlayerTurn;

    }
    private void StartPlayerTurn()
    {
        currentPhase = Phases.StartPhase;
        PrePlayerPhase?.Invoke();
        battePhaseOccuring = playPhaseOccuring = endPhaseTriggered = false;
        StartCoroutine(HandlePlayerTurnState());
    }

    private void EndPlayerTurn()
    {
        currentPhase = Phases.EndPhase;
        playPhaseOccuring = false;
        battePhaseOccuring = false;
        endPhaseTriggered = true;
    }
    
    private void StartBattlePhase()
    {
        if (endPhaseTriggered != true)
        {
            currentPhase = Phases.BattlePhase;
            playPhaseOccuring = false;
            battePhaseOccuring = true;
        }
    }

    private void EndBattlePhase()
    {
        currentPhase = Phases.NoPhase;
        playPhaseOccuring = false;
        battePhaseOccuring = false;
    }
    private void StartPlayPhase()
    {
        if (endPhaseTriggered != true)
        {
            currentPhase = Phases.PlayPhase;
            playPhaseOccuring = true;
            battePhaseOccuring = false;
        }
    }
    private void EndPlayPhase()
    {
        currentPhase = Phases.EndPhase;
        battePhaseOccuring = false;
        playPhaseOccuring = false;
    }

    IEnumerator HandlePlayerTurnState()
    {
        //Debug.Log("Starting Turn");
        PlayerTurnStarted?.Invoke();
        
        //Debug.Log("Beginning Shop Play phase");
        StartPlayPhase();
        PlayPhaseStarted?.Invoke();
        while (playPhaseOccuring)
        {
            yield return null;
        }
        EndPlayPhase();
        PlayPhaseEnded?.Invoke();
        
        //Debug.Log("Beginning Battle Phase");
        StartBattlePhase();
        BattlePhaseStarted?.Invoke();
        while (battePhaseOccuring)
        {
            yield return null;
        }
        EndBattlePhase();
        BattlePhaseEnded?.Invoke();
        
        Debug.Log("Sending Ending Player Turn Event");
        EndingPlayerTurn?.Invoke();
        //Debug.Log("Ending Turn");
        EndPlayPhase();
        Debug.Log("Sending end turn event");
        PlayerTurnEnded?.Invoke();
    }
}
