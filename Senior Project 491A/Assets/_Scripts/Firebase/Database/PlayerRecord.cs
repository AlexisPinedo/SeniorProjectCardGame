using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecord
{
    public int wins;
    public int losses;

    public PlayerRecord(int wins, int losses)
    {
        this.wins = wins;
        this.losses = losses;
    }

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string,object> result = new Dictionary<string, object>
        {
            ["wins"] = wins,
            ["losses"] = losses
        };

        return result;
    }
}
