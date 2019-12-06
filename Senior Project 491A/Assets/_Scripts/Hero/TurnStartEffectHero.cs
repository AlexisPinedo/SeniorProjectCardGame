using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnStartEffectHero : Hero
{
    protected virtual void OnEnable()
    {
        TurnPhaseManager.PlayerTurnStarted += TriggerHeroPowerEffect;

        //Swap to player turn started...
        //Vito was gaining power only during enemy phase and would revert to 0 on player turn start
        //History.CardHistoryComponentsUpdated += TriggerHeroPowerEffect;
    }

    protected void OnDisable()
    {
        TurnPhaseManager.PlayerTurnStarted -= TriggerHeroPowerEffect;
        //History.CardHistoryComponentsUpdated -= TriggerHeroPowerEffect;
    }
    

}
