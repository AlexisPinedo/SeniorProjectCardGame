using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class OnDeathGainPowerEffect : OnDeathCardEffects
{
    [SerializeField] private int powerIncreaseAmount;
    public override void LaunchCardEffect()
    {
        NotificationWindowEvent.Instance.EnableNotificationWindow("Enemy destroyed gain power");
        TurnManager.Instance.turnPlayer.Power += powerIncreaseAmount;
    }
}
