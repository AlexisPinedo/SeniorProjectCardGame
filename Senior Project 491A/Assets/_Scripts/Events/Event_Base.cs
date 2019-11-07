using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event_Base : MonoBehaviour
{
    public delegate void _gameStatePausingEventTriggered();
    public static event _gameStatePausingEventTriggered GameStatePausingEventTriggered;
    
    public delegate void _gameStatePausingEventEnded();
    public static event _gameStatePausingEventEnded GameStatePausingEventEnded;
    public virtual void EventState()
    {
        
    }

    protected void TriggerGameStatePauseEvent()
    {
        GameStatePausingEventTriggered?.Invoke();
    }

    protected void EndGameStatePauseEvent()
    {
        GameStatePausingEventEnded?.Invoke();
    }
    
}
