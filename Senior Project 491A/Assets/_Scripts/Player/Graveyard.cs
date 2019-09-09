using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Graveyard : ScriptableObject
{
    public List<PlayerCard> graveyard = new List<PlayerCard>();
}




//public class Graveyard : MonoBehaviour
//{
    //public List<PlayerCard> graveyard = new List<PlayerCard>();

    //public List<PlayerCard> GetGraveyard()
    //{
    //    return graveyard;
    //}

    //public void AddToGrave(PlayerCard card)
    //{
    //    if (card != null)
    //    {
    //        graveyard.Add(card);
    //    }
    //}

    //public void RemoveFromGrave(PlayerCard card)
    //{
    //    if (card != null && graveyard.Contains(card))
    //    {
    //        graveyard.Remove(card);
    //    }
    //}

    //public void MoveToDeck(PlayerDeck pd)
    //{
    //    while (graveyard.Count != 0)
    //    {
    //        pd.AddCard(graveyard[0]);
    //        Debug.Log("In Graveyard: Adding " + graveyard[0] + " to the deck");
    //        graveyard.Remove(graveyard[0]);
    //    }
    //    pd.Shuffle();
    //}
//}
