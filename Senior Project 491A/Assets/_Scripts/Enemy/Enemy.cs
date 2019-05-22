using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Created by: David Taitingfong
    Date:       2019-04-11
 */


/*
    This interface describes attributes for all enemies, e.g.,
    Minions and Bosses
 */
public class Enemy : MonoBehaviour
{
    public int health { get; set; }
    public int rewardValue { get; set; }
}
