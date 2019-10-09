using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnStartEffectHero : Hero
{
    protected virtual void OnEnable()
    {
        //Debug.Log("Turn starting history enabled");
        History.CardHistoryComponentsUpdated += TriggerHeroPowerEffect;
    }

    protected void OnDisable()
    {
        //Debug.Log("Turn starting history disabled");
        History.CardHistoryComponentsUpdated -= TriggerHeroPowerEffect;
    }
    

}
