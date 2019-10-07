using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UIHandler : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public GameObject canvas, startBattleButton, endTurnButton;

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
    
        
    public delegate void _notificationWindowEnabled();

    public static event _notificationWindowEnabled NotificationWindowEnabled;
    

    private static UIHandler _instance;

    public static UIHandler Instance
    {
        get { return _instance; }
    }

    [SerializeField]
    private NotificationWindow windowReference;

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
        
        if (photonView.IsMine)
        {
            //startBattleButton.SetActive(true);
            //endTurnButton.SetActive(true);
        }
        else
        {
            //startBattleButton.SetActive(true);
            //endTurnButton.SetActive(true);
        }


        if (!PhotonNetworkManager.IsOffline)
        {
            if (!photonView.IsMine)
            {
                startBattleButton.SetActive(false);
                endTurnButton.SetActive(false);
            }
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
        windowReference.gameObject.SetActive(true);
        NotificationWindow.Instance.DisplayMessage(message);
        NotificationWindow.Instance.transparentCover.gameObject.SetActive(true);
        NotificationWindowEnabled?.Invoke();
    }

}
