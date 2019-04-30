using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graveyard : MonoBehaviour
{
    private List<PlayerCard> graveYard = new List<PlayerCard>();

    public List<PlayerCard> GetGraveYard()
    {
        return graveYard;
    }

    public void AddToGrave(PlayerCard card)
    {
        graveYard.Add(card);
    }

    public void RemoveCard(PlayerCard card)
    {
        if (graveYard.Contains(card))
            graveYard.Remove(card);
    }
}
