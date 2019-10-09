using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This method will contain the reward pile
/// this will probably be changes to an integer value stored on the player scriptable object
/// </summary>
public class Reward : ScriptableObject
{
    private List<PlayerCard> rewardPile = new List<PlayerCard>();
    private int rewardPoints;

    public void getPoints()
    {
        //foreach (Card rewardCard in rewardPile)
            //rewardPoints = rewardPoints + rewardCard.cardCurrency;
    }

}
