using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurnCardPlayer : MonoBehaviour
{
    public Card cardToPlay;

    public Deck enemyDeck;

    public CreateGrid enemyGrid;

    Vector2 spawnPoint = new Vector2(4, 2);

    int filledCardZones = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayCard();
        }
    }

    void PlayCard()
    {
        var location = enemyGrid.GetNearestPointOnGrid(spawnPoint);

        if (enemyGrid.isPlaceable(location))
        {
            if (location.x == 6f && location.y == 2f)
            {
                spawnPoint.x -= enemyGrid.size;
                Debug.Log("In boss zone");
                return;
            }
            
            cardToPlay = enemyDeck.DrawCard();
            cardToPlay.gameObject.transform.position = location;

            Instantiate(cardToPlay);
            filledCardZones++;
            enemyGrid.SetObjectPlacement(location);

            if (spawnPoint.y == 2f)
            {
                if (spawnPoint.x > 0f)
                    spawnPoint.x -= enemyGrid.size;
                else if (spawnPoint.x <= 0f)
                {
                    spawnPoint.x = 0f;
                    spawnPoint.y -= enemyGrid.size;
                }
            }
            else if(spawnPoint.y == 0f)
            {
                if (spawnPoint.x >= 0f && spawnPoint.x < (enemyGrid.size * enemyGrid.xValUnits) - 2)
                    spawnPoint.x += enemyGrid.size;
                else if (spawnPoint.x >= (enemyGrid.size * enemyGrid.xValUnits) -2 )
                {
                    spawnPoint.x = ((enemyGrid.size * enemyGrid.xValUnits) - 2);
                    spawnPoint.y += enemyGrid.size;
                }
            }
            if (spawnPoint.x == 6f && spawnPoint.y == 2f)
            {
                spawnPoint.x -= enemyGrid.size;
                Debug.Log("In boss zone");
            }
            Debug.Log("Next position to spwan will be: " + spawnPoint);

        }
        else
        {
            Debug.Log("Cannot play card");
            if(filledCardZones == 13)
            {
                Debug.Log("You Lose the board is full");
            }
        }

    }

}
