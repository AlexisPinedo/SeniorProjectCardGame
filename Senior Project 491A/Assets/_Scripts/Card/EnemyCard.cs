﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCard : Card
{
    [SerializeField]
    private int rewardValue, healthValue;

    public TurnManager turnPlayer;


    //Enemy Card components
    public BossTurnCardPlayer manager;
    public CreateGrid bossZones;

    private void Awake()
    {
        bossZones = this.GetComponent<CreateGrid>();
        manager = this.GetComponent<BossTurnCardPlayer>();
        turnPlayer = GameObject.FindObjectOfType<TurnManager>();
        Debug.Log("This method ran");
    }

    private void OnDestroy()
    {
        bossZones.SetObjectPlacement(this.transform.position, false);
        manager.filledCardZones--;
        Debug.Log("Set location to false");
    }

    public void OnMouseDown()
    {
        Debug.Log("I have been clicked");
        if (turnPlayer.turnPlayer.GetPower() >= healthValue)
        {
            Debug.Log("I can kill the enemy");
            turnPlayer.turnPlayer.SubtractPower(healthValue);
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Cannot kill not enough power");
        }
    }

}
