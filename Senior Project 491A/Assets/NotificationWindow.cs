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
    
    private static NotificationWindow _instance;

    public static NotificationWindow Instance
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

        notificationText = GetComponentInChildren<TextMeshProUGUI>();
        okButton = GetComponent<Button>();
    }

    public void DisplayMessage(string message)
    {
        Time.timeScale = 0;
        notificationText.text = message;
    }

    public void CloseNotificationWindow()
    {
        Time.timeScale = 1;
        notificationText.text = "";
        this.gameObject.SetActive(false);
    }
}
