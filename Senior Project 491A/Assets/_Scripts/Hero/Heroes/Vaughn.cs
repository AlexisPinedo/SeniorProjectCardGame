using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class Vaughn : CostRequirementHero
{
    protected override void OnEnable()
    {
        base.OnEnable();
        _heroPowerMessageDisplay = "If you play 3 cards of the same color: Gain 2 currency";
    }

    protected override void HeroPowerEffect()
    {
        NotificationWindowEvent.Instance.EnableNotificationWindow("Condition met gain 2 currency");
        TurnPlayerManager.Instance.TurnPlayer.Currency += 2;
    }
}
