using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public static event Action SettingsClicked;
    public static event Action StartBattleClicked;
    public static event Action GraveyardClicked;
    public static event Action HeroPowerClicked;
    public static event Action EndTurnClicked;
    
    private static UIHandler _instance;

    public static UIHandler Instance
    {
        get { return _instance; }
    }

    [SerializeField]
    private NotificationWindowEvent windowReference;

    private void Awake()
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

    public void SettingsButtonOnClick()
    {
        SettingsClicked?.Invoke();
    }

    public void StartBattleButtonOnClick()
    {
        StartBattleClicked?.Invoke();
        NetworkUIEventRaiser.Instance.SendBattleButtonClickEvent();
    }

    public void RaiseEventStartBattleButtonOnClick()
    {
        StartBattleClicked?.Invoke();
    }

    public void GraveyardButtonOnClick()
    {
        GraveyardClicked?.Invoke();
    }

    public void HeroPowerButtonOnClick()
    {
        if (NetworkOwnershipTransferManger.currentPhotonPlayer.IsLocal)
            HeroPowerClicked?.Invoke();
        else
        {
            Debug.Log("Unable to click hero not ur turn");
        }
    }

    public void EndTurnButtonOnClick()
    {
        PlayZoneAnimationManager.SharedInstance.DestroyCards();
        EndTurnClicked?.Invoke();
        NetworkUIEventRaiser.Instance.SendEndTurnClickEvent();
    }

    public void RasieEventEndTurnButtonOnClick()
    {
        PlayZoneAnimationManager.SharedInstance.DestroyCards();
        EndTurnClicked?.Invoke();
    }

}
