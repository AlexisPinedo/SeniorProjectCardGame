using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardListTransfer : MonoBehaviour
{
    [SerializeField]
    public List<PlayerCard> Grave = new List<PlayerCard>();

    public static GraveyardListTransfer instance;

    private void Awake()
    {
        instance = this;
    }

    public void MoveGraveToDeck()
    {
        foreach (PlayerCard card in Grave)
        {
            DeckListTransfer.instance.Deck.Push(card);
        }
    }

}
