using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckListTransfer : MonoBehaviour
{
    [SerializeField]
    public Stack<PlayerCard> Deck = new Stack<PlayerCard>();

    public PlayerCard cardToAdd;

    public static DeckListTransfer instance;

    void Awake()
    {
        instance = this;
        Deck.Push(cardToAdd);
    }
}
