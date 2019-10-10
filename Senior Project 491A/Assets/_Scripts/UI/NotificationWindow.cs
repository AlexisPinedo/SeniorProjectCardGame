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

    public delegate void _notificationWindowOpened();

    public static event _notificationWindowOpened NotificationWindowOpened;

    public delegate void _notificationWindowClosed();

    public static event _notificationWindowClosed NotificatoinWindoClosed;

    private static NotificationWindow _instance;

    public static NotificationWindow Instance
    {
        get { return _instance; }
    }

    [HideInInspector]
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
        transparentCover = GetComponentInChildren<Image>();
        
    }

    private void OnEnable()
    {
        //Debug.Log("Going to invoke event");
        NotificationWindowOpened?.Invoke();
    }

    private void OnDisable()
    {
        NotificatoinWindoClosed?.Invoke();
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
    }
    

}
