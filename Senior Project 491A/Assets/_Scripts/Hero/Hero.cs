using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 *Heroes that need to be implemented
 * Valor - if you play 3 cards of the same color. you can add up to 2 cards from the shop to your grave
 * Vaughn - if you play 3 cards of the same color, gain 2 currency
 * Veda - If you play 3 cards of the same color, both players may add 1 card from the shop to the graveyard
 * Vicky - Once per turn you can swap your health and currency
 * Vann - Draw an extra card every turn
 */

public abstract class Hero : ScriptableObject
{
    [SerializeField]
    private Sprite heroPortrait;
    protected virtual void OnEnable()
    {
        TurnManager.PlayerSwitched += HeroPowerEffect;
    }

    protected void OnDisable()
    {
        TurnManager.PlayerSwitched -= HeroPowerEffect;
    }


    protected virtual void HeroPowerEffect()
    {
        
    }
}
