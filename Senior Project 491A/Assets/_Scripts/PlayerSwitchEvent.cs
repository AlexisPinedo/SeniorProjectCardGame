using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchEvent : Event_Base
{
    private static PlayerSwitchEvent _instance;
    public static PlayerSwitchEvent Instance
    {
        get { return _instance; }
    }
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void EnablePlayerSwitchEvent()
    {
        GameEventManager.Instance.AddStateToQueue(this);
    }
    public override void EventState()
    {
        Debug.Log("In Player swap event");
        TurnManager.Instance.QuickChangeActivePlayer();
        GameEventManager.Instance.EndEvent();
    }
}
