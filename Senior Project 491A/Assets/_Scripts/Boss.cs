using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IEnemy
{
    // from IEnemy
    public int health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int rewardValue { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

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
