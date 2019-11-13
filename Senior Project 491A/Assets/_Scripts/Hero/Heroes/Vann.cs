using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class Vann : TurnStartEffectHero
{
    protected override void OnEnable()
    {
        base.OnEnable();
        _heroPowerMessageDisplay = "Draw 6 cards every turn";

    }

    protected override void HeroPowerEffect()
    {
        if(TurnPlayerManager.Instance.TurnPlayer.SelectedHero != this)
            return;

        HandContainer playerContainer = TurnPlayerManager.Instance.TurnPlayerGameObject.GetComponentInChildren<HandContainer>();

        //playerContainer.containerCardGrid.xValUnits += 1;
        playerContainer.containerCardGrid.xValUnits = 6;
        playerContainer.DrawCard();
        
    }

}
