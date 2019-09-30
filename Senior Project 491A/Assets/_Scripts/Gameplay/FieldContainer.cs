using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldContainer : Container
{
    public EnemyCardHolder holder;
    public EnemyDeck enemyDeck;

    private void OnEnable()
    {
        MinionCardHolder.CardDestroyed += AddFreeCardLocation;
        UIHandler.EndTurnClicked += DisplayACard;
    }

    private void OnDisable()
    {
        MinionCardHolder.CardDestroyed -= AddFreeCardLocation;
        UIHandler.EndTurnClicked -= DisplayACard;
    }

    public void DisplayACard()
    {
        if(enemyDeck.cardsInDeck.Count <= 0)
        {
            //Debug.Log("Enemy deck is " + enemyDeck.cardsInDeck.Count);
            //Debug.Log("Enemy Deck is empty");
            return;
        }
        
        if (containerGrid.freeLocations.Count == 0)
        {
            //Debug.Log("Field Zone is full");
            return;
        }

        Vector2 freeLocation = containerGrid.freeLocations.Pop();
        //Debug.Log(containerGrid.GetNearestPointOnGrid(new Vector2(0, 3.5f)));
        if (freeLocation == containerGrid.GetNearestPointOnGrid(new Vector2(8, 2)))
        {
            //Debug.Log("Card is in boss zone");
            DisplayACard();
            return;
        }
        
        if (containerGrid.cardLocationReference.ContainsKey(freeLocation))
        {
            CreateInstanceOfCard(freeLocation);
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
        //Debug.Log("Card destroyed adding free location");
        containerGrid.freeLocations.Push(cardLocation);
        
        //containerGrid.cardLocationReference[cardLocation] = null;
        
    }
    
    
}
