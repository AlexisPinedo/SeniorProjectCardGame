/*
    Created by: David Taitingfong
    Date:       2019-04-11
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEditor;
using UnityEngine;


/// <summary>
/// This class contains the data components for card that are boss cards
/// This object is what is used to to create different bosses
/// Defines a boss Card as a scriptable object inherits from card
/// </summary>

//This is used to create boss card in the project section you can then attatch the components for that boss
[CreateAssetMenu(menuName = "Card/BossCard Card")]
public class BossCard : EnemyCard
{
    [SerializeField]
    private Goal goal;
    [SerializeField]
    private EnemyDeck bossDeck;
    
    private void Awake()
    {
        if (goal != null)
        {
            goal.SetOwner(this);
        }
    }

    public void EnableGoal()
    {
        if(goal != null)
            goal.OnGoalEnabled();
    }

    public void AddCardToBossDeck(MinionCard cardToAdd, int cardCopies = 1)
    {
        bossDeck.AddSingleCardToDeck(cardToAdd, cardCopies);
    }
}
