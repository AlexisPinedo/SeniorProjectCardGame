using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The entity that deals cards for the Boss, but that you can't hurt.
/// </summary>
public class BossTurnCardPlayer : MonoBehaviour
{
    //public EnemyCard cardToPlay;

    //public EnemyDeck enemyDeck;

    //public CreateGrid enemyGrid;

    //Vector2 spawnPoint = new Vector2(4, 2);

    //public int filledCardZones = 0;

    //public GameObject parentObject;

    //public bool cardPlaced = false;

    //public void PlayHandler()
    //{
    //    cardPlaced = false;
    //    while (cardPlaced == false && filledCardZones != 13)
    //    {
    //        PlayCard();
    //    }
    //}

    //public void PlayCard()
    //{
    //    var location = enemyGrid.GetNearestPointOnGrid(spawnPoint);

    //    if (enemyGrid.IsPlaceable(location))
    //    {
    //        if (location.x == 6f && location.y == 2f)
    //        {
    //            spawnPoint.x -= enemyGrid.size;
    //            //Debug.Log("In boss zone");
    //            return;
    //        }

    //        cardToPlay = (EnemyCard)enemyDeck.DrawCard();
    //        cardToPlay.gameObject.transform.position = location;

    //        EnemyCard theCardObject = Instantiate(cardToPlay, parentObject.transform);

    //        theCardObject.manager = this;
    //        //theCardObject.bossZones = enemyGrid;

    //        filledCardZones++;
    //        enemyGrid.SetObjectPlacement(location);

    //        //Debug.Log("Next position to spwan will be: " + spawnPoint);
    //        cardPlaced = true;

    //    }
    //    else
    //    {
    //        //Debug.Log("Cannot play card");
    //        if (filledCardZones == 13)
    //        {
    //            //Debug.Log("You Lose the board is full");
    //        }
    //    }

    //    if (spawnPoint.y == 2f)
    //    {
    //        if (spawnPoint.x > 0f)
    //            spawnPoint.x -= enemyGrid.size;
    //        else if (spawnPoint.x <= 0f)
    //        {
    //            spawnPoint.x = 0f;
    //            spawnPoint.y -= enemyGrid.size;
    //        }
    //    }
    //    else if (spawnPoint.y == 0f)
    //    {
    //        if (spawnPoint.x >= 0f && spawnPoint.x < (enemyGrid.size * enemyGrid.xValUnits) - 2)
    //            spawnPoint.x += enemyGrid.size;
    //        else if (spawnPoint.x >= (enemyGrid.size * enemyGrid.xValUnits) - 2)
    //        {
    //            spawnPoint.x = ((enemyGrid.size * enemyGrid.xValUnits) - 2);
    //            spawnPoint.y += enemyGrid.size;
    //        }
    //    }

    //    if (spawnPoint.x == 6f && spawnPoint.y == 2f)
    //    {
    //        spawnPoint.x -= enemyGrid.size;
    //        //Debug.Log("In boss zone");
    //    }
    //}
}
