using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : ScriptableObject
{
    private List<Card> rewardPile = new List<Card>();
    private int rewardPoints;

    public void getPoints()
    {
        foreach (Card rewardCard in rewardPile)
            rewardPoints = rewardPoints + rewardCard.cardCurrency;
    }

}
