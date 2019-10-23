/*
    Created by: David Taitingfong
    Date:       2019-04-11
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ALSO OBSOLETE CLASS THAT IS WIP
/// </summary>

/*
    This class represents a Boss' goal, i.e., what needs to happen in a match
    in order for the boss to win.

    ex: Draw 5 bombs
 */
public abstract class Goal : MonoBehaviour
{
    public string goalDescriptionText;
    
    protected void OnGoalCompletion()
    {
        NotificationWindowEvent.Instance.EnableNotificationWindow("Goal has been completed enemy won");
        EndGameHandler.TriggerEndGame();
    }
}
