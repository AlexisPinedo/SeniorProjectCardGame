using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCardManager : MonoBehaviour
{
    //private static EnemyCardManager _instance;
    //public static EnemyCardManager Instance { get { return _instance; } }

    //private void Awake()
    //{
    //    if (_instance != null && _instance != this)
    //        Destroy(this.gameObject);
    //    else
    //        _instance = this;
    //}

    ////create a method that handles the card that was clicked
    //// this method needs to have the same signature as your delegate
    //// subscribe to onenable and ondisable methods
    //void OnEnable()
    //{
    //    EnemyCard.EnemyClicked += HandleEnemyCardClick;
    //}

    //void OnDisable()
    //{
    //    EnemyCard.EnemyClicked -= HandleEnemyCardClick;
    //}

    //void HandleEnemyCardClick(EnemyCard enemyCard)
    //{
    //    Debug.Log("I have been clicked");
    //    if (TurnManager.Instance.turnPlayer.GetPower() >= enemyCard.HealthValue)
    //    {
    //        Debug.Log("I can kill the enemy");
    //        TurnManager.Instance.turnPlayer.SubtractPower(enemyCard.HealthValue);
    //        Destroy(enemyCard.gameObject);
    //    }
    //    else
    //    {
    //        Debug.Log("Cannot kill not enough power");
    //    }
    //}
}
