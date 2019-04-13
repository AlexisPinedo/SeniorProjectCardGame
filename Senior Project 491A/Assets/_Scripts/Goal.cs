/*
    Created by: David Taitingfong
    Date:       2019-04-11
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    This class represents a Boss' goal, i.e., what needs to happen in a match
    in order for the boss to win.

    ex: Draw 5 bombs
 */
public class Goal : MonoBehaviour
{
    /* Description of the Goal */
    public string endGoal;

    // Start is called before the first frame update
    void Start()
    {

    }

    /*
        Called when the goal has been achieved
     */
    public void EndGame()
    {
        // TODO: Change from a debug log to an end state
        Debug.Log("Goal was reached. Boss has won. Sorry bud");
    }
}
