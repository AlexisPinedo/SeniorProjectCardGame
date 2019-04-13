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
    // from IEnemy
    public int health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int rewardValue { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    /* The Boss' Goal */
    public Goal goal;

    // TODO: Change to classes
    public string deck;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Set the goal?
    }

    // Update is called once per frame
    void Update()
    {

    }
}
