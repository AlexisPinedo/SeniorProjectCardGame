using System;
using UnityEngine;

[CreateAssetMenu]
public class SelectedBoss : ScriptableObject
{
    [SerializeField]
    private BossCard selectedBossCard;

    public BossCard SelectedBossCard
    {
        get => selectedBossCard;
        set => selectedBossCard = value;
    }
}
