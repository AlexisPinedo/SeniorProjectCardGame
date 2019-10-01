using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Vito : Hero
{
    protected override void HeroPowerEffect()
    {
        if (TurnManager.Instance.turnPlayer.SelectedHero == this)
        {
            Debug.Log("Player has Vito selected increasing power by 4 for the turn");
            UIHandler.Instance.EnableNotificationWindow("Vito is selected gaining 4 power");
            TurnManager.Instance.turnPlayer.Power += 4;
        }
    }
}
