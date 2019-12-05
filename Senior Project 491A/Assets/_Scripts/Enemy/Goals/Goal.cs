using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal : ScriptableObject
{
    public string goalDescriptionText;

    protected BossCard owner;

    public void SetOwner(BossCard ownerToSet)
    {
        owner = ownerToSet;
        OnGoalEnabled();
    }

    public void RemoveOwner()
    {
        owner = null;
    }

    public virtual void OnGoalEnabled()
    {
        
    }
    
    protected virtual void OnGoalCompletion()
    {
       //NotificationWindowEvent.Instance.EnableNotificationWindow("Goal has been completed enemy won");
        EndGameHandler.TriggerEndGame();
    }
}
