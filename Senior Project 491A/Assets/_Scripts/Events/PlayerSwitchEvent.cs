using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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
        AddStateToQueue();
    }
    
    public override void EventState()
    {
        TurnPlayerManager.Instance.ChangeActivePlayer();
        GameEventManager.Instance.EndEvent();
        if(!PhotonNetwork.OfflineMode)
            photonView.RPC("RemoteEventStatePlayerSwitch", RpcTarget.Others);
    }

    [PunRPC]
    public void RemoteEventStatePlayerSwitch()
    {
        TurnPlayerManager.Instance.ChangeActivePlayer();
        GameEventManager.Instance.EndEvent();
    }
}
