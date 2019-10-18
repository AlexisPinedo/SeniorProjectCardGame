using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

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

    private byte endTurnIdentifier = (byte)'E';

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

    public void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    public void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
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

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others,
        };

        SendOptions sendOptions = new SendOptions
        {
            Reliability = true
        };

        PhotonNetwork.RaiseEvent(endTurnIdentifier, null, raiseEventOptions, sendOptions);
    }

    public void OnEvent(EventData photonEvent)
    {
        byte recievedCode = photonEvent.Code;
        if (recievedCode == endTurnIdentifier)
        {
            EndTurnClicked?.Invoke();
        }
        else
        {
            //Debug.Log("Event code not found");
        }
    }

}
