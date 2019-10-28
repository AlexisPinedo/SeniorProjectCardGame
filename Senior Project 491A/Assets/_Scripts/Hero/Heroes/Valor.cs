using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Valor - if you play 3 cards of the same color. you can add up to 2 cards from the shop to your grave

//[CreateAssetMenu]
public class Valor : CostRequirementHero
{
    private int cardSelectionLimit = 2;
    

    //Veda - If you play 3 cards of the same color, both players may add 1 card from the shop to the graveyard

    protected override void OnEnable()
    {
        base.OnEnable();
        _heroPowerMessageDisplay =
            "If you play 3 cards of the same color: Add 2 cards of the same color to your grave";
    }

    protected override void HeroPowerEffect()
    {
        NotificationWindowEvent.Instance.EnableNotificationWindow("Cost met select 2 cards of the same color");
        
        FreeShopSelectionEvent.Instance.EnableShopSelectionState(cardSelectionLimit, History.Instance.PlayerCardHistory.Last().CardType);
    }


}
