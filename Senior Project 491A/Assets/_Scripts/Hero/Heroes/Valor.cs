using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Valor - if you play 3 cards of the same color. you can add up to 2 cards from the shop to your grave

[CreateAssetMenu]
public class Valor : CostRequirementHero
{
    protected override void HeroPowerEffect()
    {
        UIHandler.Instance.EnableNotificationWindow("Valor Effect Activated");
    }
}
