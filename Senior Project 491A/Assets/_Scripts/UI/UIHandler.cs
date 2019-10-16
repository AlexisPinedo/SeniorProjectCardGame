using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UIHandler : MonoBehaviour
{
    public delegate void settingsButtonAction();
    public static event settingsButtonAction SettingsClicked;

    public delegate void StartBattleButtonAction();
    public static event StartBattleButtonAction StartClicked;

    public delegate void GraveyardButtonAction();
    public static event GraveyardButtonAction GraveyardClicked;

    public delegate void HeroPowerButtonAction();
    public static event HeroPowerButtonAction HeroPowerClicked;

    public delegate void EndTurnButtonAction();
    public static event EndTurnButtonAction EndTurnClicked;
    
    private static UIHandler _instance;

    public static UIHandler Instance
    {
        get { return _instance; }
    }

    [SerializeField]
    private NotificationWindowEvent windowEventReference;

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
        StartClicked?.Invoke();
    }

    public void GraveyardButtonOnClick()
    {
        GraveyardClicked?.Invoke();
    }

    public void HeroPowerButtonOnClick()
    {
        HeroPowerClicked?.Invoke();
    }

    public void EndTurnButtonOnClick()
    {
        EndTurnClicked?.Invoke();
    }

    public void EnableNotificationWindow(string message)
    {
        windowEventReference.gameObject.SetActive(true);
        windowEventReference.EnableNotificationWindow(message);
    }

}
