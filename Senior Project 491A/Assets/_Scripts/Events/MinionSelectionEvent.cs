using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MinionSelectionEvent : Event_Base
{
    private static MinionSelectionEvent _instance;

    public static MinionSelectionEvent Instance
    {
        get { return _instance; }
    }
    
    public void EnableMinonSelectionEvent()
    {
        GameEventManager.Instance.AddStateToQueue(this);
    }
    
    private void Awake()
    {
        if (_instance == null && _instance != this)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    private IEnumerator MinionSelectionState()
    {
        Debug.Log("In minion selection Event");

        yield return new WaitForSeconds(1.0f);
        
        Debug.Log("exiting minion selection Event");
    }
}
