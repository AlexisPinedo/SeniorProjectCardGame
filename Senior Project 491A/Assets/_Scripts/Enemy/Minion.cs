/*
    Created by: David Taitingfong
    Date:       2019-04-11
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class represents a Boss's Minion

    TODO: Extend Card class
 */
public class Minion : EnemyCard
{
    /* Minion's health and reward values */
    private int _health;
    //private int _rewardValue;
    public CardEffect effect;


    // from IEnemy
    public int health
    {
            get { return _health; }
            set {
                if (value < 1) {
                    _health = 5;  // Default health
                } else {
                    _health = value;
                }
            }
    }

    public int rewardValue
    {
            get { return _rewardValue;}
            set {
                if (value < 1) {
                    _rewardValue = 2;  // Default reward value for Minions
                } else {
                    _rewardValue = value;
                }
            }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        manager.filledCardZones--;
        Debug.Log("Set location to false");
    }
}
