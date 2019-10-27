using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayMinonCardEffect : OnPlayEffects
{
    [SerializeField] private int summonCount;
    public override void LaunchCardEffect()
    {
        for (int i = 0; i < summonCount; i++)
        {
            FieldContainer.Instance.DisplayACard();
        }
    }
}
