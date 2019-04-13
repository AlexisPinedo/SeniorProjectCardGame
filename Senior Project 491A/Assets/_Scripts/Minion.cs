/*
    Created by: David Taitingfong
    Date:       2019-04-11
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class represents a Boss's Minion

    TODO: Extend Card class
 */
public class Minion : MonoBehaviour, IEnemy
{
    // from IEnemy
    public int health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int rewardValue { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
