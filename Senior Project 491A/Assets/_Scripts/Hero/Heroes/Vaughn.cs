using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class Vaughn : CostRequirementHero
{
    protected override void HeroPowerEffect()
    {
        NotificationWindowEvent.Instance.EnableNotificationWindow("Condition met gain 2 currency");
        TurnManager.Instance.turnPlayer.Currency += 2;
    }
}
