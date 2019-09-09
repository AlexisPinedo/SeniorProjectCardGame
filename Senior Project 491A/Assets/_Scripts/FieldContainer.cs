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
        throw new NotImplementedException();
    }

    private void OnDisable()
    {
        throw new NotImplementedException();
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

    void CreateInstanceOfCard(Vector2 freeLocation)
    {
        EnemyCard cardDrawn = null;
        cardDrawn = (EnemyCard)enemyDeck.cardsInDeck.Pop();

        holder.card = cardDrawn;
        EnemyCardHolder cardHolder = Instantiate(holder, freeLocation, Quaternion.identity, this.transform);
        containerGrid.cardLocationReference.Add(new Vector2(cardHolder.gameObject.transform.position.x, 
            cardHolder.gameObject.transform.position.y), cardHolder);
    }

    void AddFreeCardLocation(EnemyCardHolder cardDestroyed)
    {
        
    }
    
    
}
