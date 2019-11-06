﻿using System;
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
        if(TurnManager.Instance.turnPlayer.SelectedHero != this)
            return;

        HandContainer playerContainer = TurnManager.Instance.turnPlayerGameObject.GetComponentInChildren<HandContainer>();

        //playerContainer.containerGrid.xValUnits += 1;
        playerContainer.containerGrid.xValUnits = 6;
        playerContainer.DrawCard();
        
    }

}
