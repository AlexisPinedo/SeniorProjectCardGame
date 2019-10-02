using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class Vito : TurnStartEffectHero
{
    protected override void HeroPowerEffect()
    {
        //Debug.Log("Player has Vito selected increasing power by 4 for the turn");
        //UIHandler.Instance.EnableNotificationWindow("Vito is selected gaining 4 power");
        TurnManager.Instance.turnPlayer.Power += 4;
    }
}
