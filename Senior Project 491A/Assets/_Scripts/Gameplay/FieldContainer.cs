using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class FieldContainer : Container
{
    public MinionCardDisplay minionDisplay;
    public EnemyDeck enemyDeck;

    public delegate void _enemyCardPlayed(EnemyCard cardPlayed);

    public static event _enemyCardPlayed EnemyCardPlayed;

    private static FieldContainer _instance;

    public static FieldContainer Instance
    {
        get => _instance;
    }

    private void Awake()
    {
        if (_instance != this && _instance == null)
            _instance = this;
        else
        {
            Destroy(this.gameObject);
        }
        enemyDeck.cardsInDeck = ShuffleDeck.Shuffle(enemyDeck);
    }

    private void OnEnable()
    {
        MinionCardDisplay.CardDestroyed += AddFreeCardLocation;

        //UIHandler.EndTurnClicked += DisplayACard;
    }

    private void OnDisable()
    {
        MinionCardDisplay.CardDestroyed -= AddFreeCardLocation;
        //UIHandler.EndTurnClicked -= DisplayACard;
    }

    public void DisplayACard()
    {
        if(enemyDeck.cardsInDeck.Count <= 0)
        {
            //Debug.Log("Enemy deck is " + enemyDeck.cardsInDeck.Count);
            //Debug.Log("Enemy Deck is empty");
            return;
        }
        
        if (containerCardGrid.freeLocations.Count == 0)
        {
            EndGameHandler.TriggerEndGame();
            return;
        }

        Vector2 freeLocation = containerCardGrid.freeLocations.Pop();
        //Debug.Log(containerCardGrid.GetNearestPointOnGrid(new Vector2(0, 3.5f)));
        if (freeLocation == containerCardGrid.GetNearestPointOnGrid(new Vector2(8, 2)))
        {
            //Debug.Log("Card is in boss zone");
            DisplayACard();
            return;
        }
        
        if (containerCardGrid.cardLocationReference.ContainsKey(freeLocation))
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
        MinionCard cardDrawn = null;
        enemyDeck.cardsInDeck = ShuffleDeck.Shuffle(enemyDeck);
        cardDrawn = enemyDeck.cardsInDeck.Pop();

        minionDisplay.card = cardDrawn;
        MinionCardDisplay cardDisplay = Instantiate(minionDisplay, freeLocation, Quaternion.identity, this.transform);

        NetworkIDAssigner.AssignID(cardDisplay.gameObject.GetPhotonView());

        cardDisplay.enabled = true;
        
        if (!containerCardGrid.cardLocationReference.ContainsKey(freeLocation))
        {
            containerCardGrid.cardLocationReference.Add(new Vector2(cardDisplay.gameObject.transform.position.x, 
                cardDisplay.gameObject.transform.position.y), cardDisplay);
        }
        else
        {
            containerCardGrid.cardLocationReference[freeLocation] = cardDisplay;
        }
        EnemyCardPlayed?.Invoke(cardDisplay.card);
    }

    void AddFreeCardLocation(MinionCardDisplay cardDestroyed)
    {
        Vector2 cardLocation = cardDestroyed.gameObject.transform.position;
        //Debug.Log("Card destroyed adding free location");
        containerCardGrid.freeLocations.Push(cardLocation);
        //containerCardGrid.cardLocationReference[cardLocation] = null;
    }
}
