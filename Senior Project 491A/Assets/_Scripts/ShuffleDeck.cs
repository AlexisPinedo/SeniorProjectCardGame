using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShuffleDeck 
{
    public static Stack<Card> Shuffle(Deck deckToShuffle)
    {
        //System.Random random = new System.Random(RandomNumberNetworkGenerator.Instance.randomNumber);

        System.Random random = new System.Random();
        
        Debug.Log("Shuffling with new seed: " + random);

        var deckList = deckToShuffle.cardsInDeck.ToArray();
        int n = deckList.Length;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Card value = deckList[k];
            deckList[k] = deckList[n];
            deckList[n] = value;
        }
        
        deckToShuffle.cardsInDeck = new Stack<Card>(deckList);

        return deckToShuffle.cardsInDeck;
    }
}
