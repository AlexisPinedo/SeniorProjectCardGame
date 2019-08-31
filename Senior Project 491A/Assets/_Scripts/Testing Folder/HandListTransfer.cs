using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandListTransfer : MonoBehaviour
{
    [SerializeField]
    public List<PlayerCard> Hand = new List<PlayerCard>();

    public static HandListTransfer instance;

    private void Awake()
    {
        instance = this;
    }

    public void DrawCardFromDeck()
    {
        for(int i = 0; i < 5; i++)
        {
            if (DeckListTransfer.instance.Deck.Count <= 0)
            {
                Debug.Log("Deck is empty cant draw");
                break;

            }
            PlayerCard card = DeckListTransfer.instance.Deck.Pop();
            Hand.Add(Instantiate(card));
            
        }
    }
}
