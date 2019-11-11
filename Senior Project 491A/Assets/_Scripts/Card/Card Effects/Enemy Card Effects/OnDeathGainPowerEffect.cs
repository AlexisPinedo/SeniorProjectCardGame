using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effect/Minion Card Effect/On Death Gain Power")]

public class OnDeathGainPowerEffect : OnDeathCardEffects
{
    [SerializeField] private int powerIncreaseAmount;
    public override void LaunchCardEffect()
    {
        NotificationWindowEvent.Instance.EnableNotificationWindow("Enemy destroyed gain power");
        TurnPlayerManager.Instance.TurnPlayer.Power += powerIncreaseAmount;
    }
}
