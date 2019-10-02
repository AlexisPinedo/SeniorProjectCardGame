using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Vaughn : CostRequirementHeroe
{
    protected override void HeroPowerEffect()
    {
        UIHandler.Instance.EnableNotificationWindow("Condition met gain 2 currency");
        TurnManager.Instance.turnPlayer.Currency += 2;
    }
}
