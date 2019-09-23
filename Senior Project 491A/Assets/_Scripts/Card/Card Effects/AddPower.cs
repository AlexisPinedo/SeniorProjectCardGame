using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AddPower : CardEffect
{
    [SerializeField] private int powerAdditionAmount;

    void AddPowerByEffect()
    {
        Debug.Log("Adding Power via card effect");
        TurnManager.Instance.turnPlayer.Power += powerAdditionAmount;
    }

    public override void LaunchCardEffect()
    {
        AddPowerByEffect();
    }
}
