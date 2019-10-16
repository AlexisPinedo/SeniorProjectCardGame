using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class NotificationWindowEvent : Event_Base
{
    private TextMeshProUGUI notificationText;
    private Button okButton;

    public delegate void _notificationWindowOpened();

    public static event _notificationWindowOpened NotificationWindowOpened;

    public delegate void _notificationWindowClosed();

    public static event _notificationWindowClosed NotificatoinWindoClosed;

    private static NotificationWindowEvent _instance;
    public static NotificationWindowEvent Instance
    {
        get { return _instance; }
    }

    [HideInInspector]
    public Image transparentCover;

    private string messageText = "";
    
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

        notificationText = GetComponentInChildren<TextMeshProUGUI>();
        okButton = GetComponent<Button>();
        transparentCover = GetComponentInChildren<Image>();
    }

    private void OnDisable()
    {
        NotificatoinWindoClosed?.Invoke();
        
        transparentCover.gameObject.SetActive(false);
        notificationText.text = "";
    }
    
    public void EnableNotificationWindow(string message)
    {
        DisplayMessage(message);
        GameEventManager.Instance.AddStateToQueue(this);
    }

    public override void EventState()
    {
        Debug.Log("In display notification window event");

        notificationText.text = messageText;
        NotificationWindowOpened?.Invoke();
    }


    public void DisplayMessage(string message)
    {
        messageText = message;
    }

    public void CloseNotificationWindow()
    {
        this.gameObject.SetActive(false);
        messageText = "";
        GameEventManager.Instance.EndEvent();
    }
    
}
