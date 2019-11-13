using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//These will always be player cards
public abstract class OnPlayEffects : CardEffect
{
    public override void CardPlacedIntoPlay()
    {
        if(owner.CardPlayed != null)
            owner.CardPlayed += LaunchCardEffect;
    }

    public override void CardRemovedFromPlay()
    {
        if(owner.CardPlayed != null)
            owner.CardPlayed -= LaunchCardEffect;
    }

    public override void LaunchCardEffect()
    {
        //invoke delegate
    }
}
