using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effect/Player Card Effect/Destroy Minion")]
public class DestroyMinion : OnPlayEffects
{
    public override void LaunchCardEffect()
    {
        base.LaunchCardEffect();
        MinionSelectionEvent.Instance.EnableMinonSelectionEvent();
    }
}
