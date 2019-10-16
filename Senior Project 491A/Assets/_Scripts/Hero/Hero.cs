using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


/*
 *Heroes that need to be implemented
 * 
 * Vicky - Once per turn you can swap your health and currency
 */

public abstract class Hero : ScriptableObject
{
    [SerializeField]
    public Sprite _heroPortrait;

    public Sprite HeroPortrait
    {
        get => _heroPortrait;
    }

    protected virtual void TriggerHeroPowerEffect()
    {
        if (TurnManager.Instance.turnPlayer.SelectedHero == this)
        {
            HeroPowerEffect();
        }
    }
    
    protected virtual void HeroPowerEffect()
    {
        
    }

}
