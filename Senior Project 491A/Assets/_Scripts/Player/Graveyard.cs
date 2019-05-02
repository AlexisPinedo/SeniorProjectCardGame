using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graveyard : MonoBehaviour
{
    private List<Card> graveyard = new List<Card>();

    public List<Card> getGraveyard()
    {
        return graveyard;
    }

    public void addToGrave(Card card)
    {
        graveyard.Add(card);
    }

    public void removeCard(Card card)
    {
        if (graveyard.Contains(card))
            graveyard.Remove(card);
    }
}
