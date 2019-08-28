using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class History : MonoBehaviour
{
    private List<PlayerCard> playerCardHistory = new List<PlayerCard>();

    public PlayerCard GetLastCardPlayed()
    {
        PlayerCard LastCardPlayed;
        if (playerCardHistory.Count == 0)
            return null;

        //LastCardPlayed = playerCardHistory.
        return LastCardPlayed;
    }
}
