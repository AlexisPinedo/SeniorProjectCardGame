using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnPurchaseEffectBase : ScriptableObject
{
    public PlayerCard owner;
    public void Trigger(PlayerCard _owner)
    {
        owner = _owner;
        DoTrigger();
    }
    protected abstract void DoTrigger();
}
