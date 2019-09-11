using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldContainer : Container
{
    public EnemyCardHolder holder;
    public EnemyDeck enemyDeck;

    private void Start()
    {
        DisplayACard();
    }

    private void OnEnable()
    {
        MinionCardHolder.CardDestroyed += AddFreeCardLocation;
    }

    private void OnDisable()
    {
        MinionCardHolder.CardDestroyed -= AddFreeCardLocation;
    }

    public void DisplayACard()
    {
        if(enemyDeck.cardsInDeck.Count <= 0)
        {
            //Debug.Log("Enemy deck is " + enemyDeck.cardsInDeck.Count);
            return;
        }

        Vector2 freeLocation = containerGrid.freeLocations.Dequeue();
        if (containerGrid.cardLocationReference.ContainsKey(freeLocation))
        {
            //Handle code for checking if the spot is free
            if (containerGrid.cardLocationReference[freeLocation] == null)
            {
                CreateInstanceOfCard(freeLocation);
            }
            else
            {
                DisplayACard();
            }
        }
        else
        {
            CreateInstanceOfCard(freeLocation);
        }
    }

    public void CreateInstanceOfCard(Vector2 freeLocation)
    {
        EnemyCard cardDrawn = null;
        cardDrawn = (EnemyCard)enemyDeck.cardsInDeck.Pop();

        holder.card = cardDrawn;
        EnemyCardHolder cardHolder = Instantiate(holder, freeLocation, Quaternion.identity, this.transform);
        if (!containerGrid.cardLocationReference.ContainsKey(freeLocation))
        {
            containerGrid.cardLocationReference.Add(new Vector2(cardHolder.gameObject.transform.position.x, 
                cardHolder.gameObject.transform.position.y), cardHolder);
        }
        else
        {
            containerGrid.cardLocationReference[freeLocation] = cardHolder;
        }
        

    }

    void AddFreeCardLocation(EnemyCardHolder cardDestroyed)
    {
        Vector2 cardLocation = cardDestroyed.gameObject.transform.position;
        Debug.Log("Card destroyed adding free location");
        containerGrid.freeLocations.Enqueue(cardLocation);
        //containerGrid.cardLocationReference[cardLocation] = null;
    }
    
    
}
