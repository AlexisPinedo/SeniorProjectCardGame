using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Goal/Bomb Goal")]
public class BombGoal : Goal
{
    [SerializeField] private MinionCard bombGoal;
    [SerializeField] private int requiredCountOfCards = 5;
    public int bombCardsRevealed = 0;
    public override void OnGoalEnabled()
    {
        owner.AddCardToBossDeck(bombGoal, 5);
    }

    public void CheckGoalCompletion()
    {
        if (bombCardsRevealed == requiredCountOfCards)
        {
            OnGoalCompletion();
        }
    }
}

