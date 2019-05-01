using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCard : Card
{
    [SerializeField]
    private int rewardValue, healthValue;


    private void Awake()
    {
        bossZones = this.GetComponent<CreateGrid>();
        manager = this.GetComponent<BossTurnCardPlayer>();
        Debug.Log("This method ran");
    }

    private void OnDestroy()
    {
        bossZones.SetObjectPlacement(this.transform.position, false);
        manager.filledCardZones--;
        Debug.Log("Set location to false");
    }

    private void CheckIfDead()
    {

    }
}
