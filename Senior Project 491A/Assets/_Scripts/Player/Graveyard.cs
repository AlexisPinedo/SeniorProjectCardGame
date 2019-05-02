using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graveyard : MonoBehaviour
{
    private List<PlayerCard> graveyard = new List<PlayerCard>();

    public List<PlayerCard> getGraveyard()
    {
        return graveyard;
    }

    public void addToGrave(PlayerCard card)
    {
        graveyard.Add(card);
    }

    public void removeCard(PlayerCard card)
    {
        if (graveyard.Contains(card))
            graveyard.Remove(card);
    }
}
