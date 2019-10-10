using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ShuffleDeck : MonoBehaviourPunCallbacks
{
    public int randomNumber;

    private static ShuffleDeck _instance;

    public static ShuffleDeck Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != this && _instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        randomNumber = (int)PhotonNetwork.CurrentRoom.CustomProperties["deckRandomValue"];
        Debug.Log("RandomSyncedValue: " + randomNumber);
    }
    
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
