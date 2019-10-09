/*
    Created by: David Taitingfong
    Date:       2019-04-11
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains the data components for card that are Minion cards
/// This object is what is used to to create different Minions
/// Defines a Minion Card as a scriptable object inherits from card
/// </summary>
//This is used to create Minion card in the project section you can then attach the components for that boss
[CreateAssetMenu(menuName = "Card/Minion Card")]
public class Minion : EnemyCard
{
    
}












/// <summary>
/// A lackey for the Boss.
/// </summary>
//public class Minion : EnemyCard
//{
//    /// <summary>
//    /// The Minion's health, hidden public prying eyes.
//    /// </summary>
//    private int _health;
//
//    //private int _rewardValue;
//
//    /// <summary>
//    /// The Minion's Health. Defaults to 5 if value being set is too low.
//    /// </summary>
//    public int Health
//    {
//        get { return _health; }
//        set
//        {
//            if (value < 1)
//            {
//                _health = 5;  // Default health
//            }
//            else
//            {
//                _health = value;
//            }
//        }
//    }
//
//    /// <summary>
//    /// The reward value for defeating this Minion. Defaults to 2 if the value being set is too low.
//    /// </summary>
//    public int RewardValue
//    {
//        get { return _rewardValue; }
//        set
//        {
//            if (value < 1)
//            {
//                _rewardValue = 2;  // Default reward value for Minions
//            }
//            else
//            {
//                _rewardValue = value;
//            }
//        }
//    }
//
//    public CardEffect effect;
//
//    // Start is called before the first frame update
//    void Start()
//    {
//
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//
//    }
//}
