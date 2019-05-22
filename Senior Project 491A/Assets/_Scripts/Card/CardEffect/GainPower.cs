using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New OnPurchaseEffect", menuName = "CardEffects/OnPurchase/GainPower", order = 0)]
public class GainPower : OnPurchaseEffectBase
{
    [SerializeField] private int powerAmount = 1;


    protected override void DoTrigger()
    {
        Debug.Log("Triggering Effect");
        owner.cardAttack += powerAmount;
    }
}
