using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathCardEffects : CardEffect
{
    public override void CardPlacedIntoPlay()
    {
        if(owner.CardPlayed != null)
            owner.CardDestoyed += LaunchCardEffect;
    }

    public override void CardRemovedFromPlay()
    {
        if(owner.CardPlayed != null)
            owner.CardDestoyed -= LaunchCardEffect;
    }
}
