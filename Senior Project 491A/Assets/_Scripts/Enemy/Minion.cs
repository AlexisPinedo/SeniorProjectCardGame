﻿/*
    Created by: David Taitingfong
    Date:       2019-04-11
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A lackey for the Boss.
/// </summary>
public class Minion : EnemyCard
{
    /// <summary>
    /// The Minion's health, hidden public prying eyes.
    /// </summary>
    private int _health;

    //private int _rewardValue;
    public CardEffect effect;

    /// <summary>
    /// The Minion's Health. Defaults to 5 if value being set is too low.
    /// </summary>
    public int Health
    {
        get { return _health; }
        set
        {
            if (value < 1)
            {
                _health = 5;  // Default health
            }
            else
            {
                _health = value;
            }
        }
    }

    /// <summary>
    /// The reward value for defeating this Minion. Defaults to 2 if the value being set is too low.
    /// </summary>
    public int RewardValue
    {
        get { return _rewardValue; }
        set
        {
            if (value < 1)
            {
                _rewardValue = 2;  // Default reward value for Minions
            }
            else
            {
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
