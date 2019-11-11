using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class Vito : TurnStartEffectHero
{
    protected override void OnEnable()
    {
        base.OnEnable();
        _heroPowerMessageDisplay = "Gain 4 power every turn";
        
    }

    protected override void HeroPowerEffect()
    {
        //Debug.Log("Player has Vito selected increasing power by 4 for the turn");
        NotificationWindowEvent.Instance.EnableNotificationWindow("Vito is selected gaining 4 power");
        TurnPlayerManager.Instance.TurnPlayer.Power += 4;
    }
}
