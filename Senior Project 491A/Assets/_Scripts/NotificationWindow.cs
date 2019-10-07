using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationWindow : MonoBehaviour
{
    private TextMeshProUGUI notificationText;
    private Button okButton;

    public delegate void _notificationWindowClosed();

    public static event _notificationWindowClosed NotificatoinWindoClosed;

    private static NotificationWindow _instance;

    public static NotificationWindow Instance
    {
        get { return _instance; }
    }

    [SerializeField]
    public Image transparentCover;

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
    }

    public void DisplayMessage(string message)
    {
        notificationText.text = message;
    }

    public void CloseNotificationWindow()
    {
        transparentCover.gameObject.SetActive(false);
        notificationText.text = "";
        this.gameObject.SetActive(false);
        NotificatoinWindoClosed?.Invoke();
    }
}
