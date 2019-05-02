using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graveyard : MonoBehaviour
{
    private List<Card> graveYard = new List<Card>();

    public List<Card> getGraveYard()
    {
        return graveYard;
    }

    public void addToGrave(Card card)
    {
        graveYard.Add(card);
    }

    public void removeCard(Card card)
    {
        if (graveYard.Contains(card))
            graveYard.Remove(card);
    }
}
