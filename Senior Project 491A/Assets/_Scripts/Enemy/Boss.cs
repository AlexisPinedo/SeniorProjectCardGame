/*
    Created by: David Taitingfong
    Date:       2019-04-11
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    This class represents a match's Boss
 */
public class Boss : MonoBehaviour, IEnemy
{
    /* Boss' health and reward value */
    private int _health;
    private int _rewardValue;

    // from IEnemy
    public int health
    {
        get { return _health; }
        set { _health = value; }
    }
    public int rewardValue
    {
        get { return _rewardValue;}
        set { _rewardValue = value;}
    }

    /* The Boss' Goal */
    public Goal goal;

    /* The Boss' Deck */
    public Deck bossDeck;

    public CreateGrid EnemyGrid;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Set the goal?
        // TODO: Populate the Boss' deck?

        this.transform.position = EnemyGrid.GetNearestPointOnGrid(new Vector2(6, 2));

    }

    // Update is called once per frame
    void Update()
    {

    }
}
