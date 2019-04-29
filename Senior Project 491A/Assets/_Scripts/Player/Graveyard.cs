using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graveyard : MonoBehaviour
{
    private List<Card> graveYard = new List<Card>();

    public List<Card> GetGraveYard()
    {
        return graveYard;
    }

    public void AddToGrave(Card card)
    {
        graveYard.Add(card);
    }

    public void RemoveCard(Card card)
    {
        if (graveYard.Contains(card))
            graveYard.Remove(card);
    }
}
