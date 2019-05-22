using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // Player References
    public Player turnPlayer;

    [SerializeField] private int healthValue;

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

    

    void HandleEnemyCardClick(GameObject enemyCard)
    {
        Debug.Log("I have been clicked");
        if (turnPlayer.GetPower() >= healthValue)
        {
            Debug.Log("I can kill the enemy");
            turnPlayer.SubtractPower(healthValue);
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Cannot kill not enough power");
        }
    }

}
