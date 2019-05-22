using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCard : Card
{
    public delegate void _EnemyDestroyed(Vector2 cardPosition, bool cardRemoved);
    public static event _EnemyDestroyed EnemyDestroyed;

    //Create delegate event like the one above for the enemycardclicked
    // Delegate will be type void and take a gameobject as a parameter

    [SerializeField]
    private int rewardValue, healthValue;

    //Should the enemy card know about the turn player
    public TurnManager turnPlayer;
    /*Should not be here. What we can do instead: 
    create a delegate event that triggers when this object is destroyed
    and when the object is clicked on then have the bossturncardplayer and turnplayer handle the data
    */
    public BossTurnCardPlayer manager;

    private void Awake()
    {
        manager = this.GetComponent<BossTurnCardPlayer>();
        turnPlayer = GameObject.FindObjectOfType<TurnManager>();
        Debug.Log("This method ran");
    }

    private void OnDestroy()
    {
        if (EnemyDestroyed != null)
            EnemyDestroyed.Invoke(this.transform.position, false);
        manager.filledCardZones--;
        Debug.Log("Set location to false");
    }

    public void OnMouseDown()
    {
        //invoke your event like in the ondestroy method and pass in this.gameObject

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
