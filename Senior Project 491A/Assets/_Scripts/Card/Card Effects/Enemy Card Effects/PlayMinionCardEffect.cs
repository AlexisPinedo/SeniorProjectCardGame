using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effect/Minion Card Effect/Play Minion")]
public class PlayMinionCardEffect : OnPlayEffects
{
    [SerializeField] private int summonCount;
    
    public override void LaunchCardEffect()
    {
        for (int i = 0; i < summonCount; i++)
        {
            FieldContainer.Instance.DisplayACard();
        }
    }

    IEnumerator DestroyBomb()
    {
        yield return new WaitForSeconds(1);
    }
}


