using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGoal : Goal
{
    public int bombCounter = 0;

    public int bombsToShuffle;

    public EnemyCard bombCard;

    public EnemyDeck enemyDeck;


    private void Awake()
    {
        //for(int i = 0; i < bombsToShuffle; i++)
        //{
        //    Debug.Log("Adding bomb card");
        //    enemyDeck.AddCard(bombCard);
        //}
        //enemyDeck.Shuffle();
    }

    private void OnEnable()
    {
        Deck.CardDrawn += HandleCardDrawn;
    }

    private void OnDisable()
    {
        Deck.CardDrawn -= HandleCardDrawn;
    }

    void HandleCardDrawn(Card cardDrawn)
    {
        if (cardDrawn.Equals(bombCard))
        {
            Debug.Log("Bomb revealved");
            bombCounter++;
            if(bombCounter == 5)
            {
                Debug.Log("You Lose!");
            }
        }
    }


}
