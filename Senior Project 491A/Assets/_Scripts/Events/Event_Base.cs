using System;

using UnityEngine;

public abstract class Event_Base : MonoBehaviour
{
    public static event Action GameStatePausingEventTriggered;
    public static event Action GameStatePausingEventEnded;
    public static event Action DisableMinionCards;
    
    public virtual void EventState()
    {
        
    }

    protected void AddStateToQueue()
    {
        GameEventManager.Instance.AddStateToQueue(this);
    }

    protected void TriggerGameStatePauseEvent()
    {
        GameStatePausingEventTriggered?.Invoke();
    }

    protected void EndGameStatePauseEvent()
    {
        GameStatePausingEventEnded?.Invoke();
    }

    protected void DisableMinionCardContainters()
    {
        DisableMinionCards?.Invoke();
    }
    
}
