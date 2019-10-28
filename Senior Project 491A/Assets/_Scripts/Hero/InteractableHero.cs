using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableHero : Hero
{
    protected bool isInteractable = true;

    public void SetIsInteractableTrue()
    {
        isInteractable = true;
    }

    public void TriggerHeroPower()
    {
        HeroPowerEffect();
    }
    
}
