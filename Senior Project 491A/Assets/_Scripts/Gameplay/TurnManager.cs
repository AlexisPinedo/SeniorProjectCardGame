using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // Player References
    public static Player turnPlayer;

    //create a method that handles the card that was clicked
    // this method needs to have the same signature as your delegate
    // subscribe to onenable and ondisable methods
    void OnEnable()
    {
        EnemyCard.EnemyClicked += HandleEnemyCardClick;
    }

    void OnDisable()
    {
        EnemyCard.EnemyClicked -= HandleEnemyCardClick;
    }

    void HandleEnemyCardClick(EnemyCard enemyCard)
    {
        Debug.Log("I have been clicked");
        if (turnPlayer.GetPower() >= enemyCard.healthValue)
        {
            Debug.Log("I can kill the enemy");
            turnPlayer.SubtractPower(enemyCard.healthValue);
            Destroy(enemyCard.gameObject);
        }
        else
        {
            Debug.Log("Cannot kill not enough power");
        }
    }

}
