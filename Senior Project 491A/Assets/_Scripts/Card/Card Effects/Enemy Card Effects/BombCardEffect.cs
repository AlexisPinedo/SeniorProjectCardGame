using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effect/Minion Card Effect/Bomb Card Effect")]
public class BombCardEffect : PlayMinionCardEffect
{
    public override void LaunchCardEffect()
    {
        base.LaunchCardEffect();
        BombGoal.BombCardsRevealed++;
        Debug.Log("Bomb card revealed current bombs out is " + BombGoal.BombCardsRevealed);
    }
}
