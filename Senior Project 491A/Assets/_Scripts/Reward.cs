using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : ScriptableObject
{
    static List<Card> rewardPile = new List<Card>();
    public int rewardPoints;

    public void getPoints()
    {
        foreach (Card rewardCard in rewardPile)
            rewardPoints = rewardPoints + rewardCard.cardCurrency;
    }

}
