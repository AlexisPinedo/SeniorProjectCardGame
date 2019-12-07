using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Goal/Bomb Goal")]
public class BombGoal : Goal
{
    [SerializeField] private MinionCard bombGoal;
    private static int requiredCountOfCards = 5;
    private static int bombCardsRevealed = 0;

    public static int BombCardsRevealed
    {
        get
        {
            return bombCardsRevealed;
        }
        set
        {
            bombCardsRevealed = value;
            _instance.CheckGoalCompletion();
        }
    }

    private void OnEnable()
    {
        _instance = this;
    }

    private static BombGoal _instance;

    public static BombGoal Instance
    {
        get { return _instance; }
    }

    public override void OnGoalEnabled()
    {
        owner.AddCardToBossDeck(bombGoal, 5);
    }

    public void CheckGoalCompletion()
    {
        if (bombCardsRevealed == requiredCountOfCards)
        {
            Debug.Log("Goal has been met");
            OnGoalCompletion();
        }
    }
}

