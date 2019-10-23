using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class NotificationWindowEvent : Event_Base
{
    private static NotificationWindowEvent _instance;
    public static NotificationWindowEvent Instance
    {
        get { return _instance; }
    }

    [SerializeField]
    private Image transparentCover;
    [SerializeField]
    private TextMeshProUGUI notificationText;
    [SerializeField] 
    private Image NotificationView; 

    private string messageText = "";
    
    private Queue<string> messageQueue = new Queue<string>();
    
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

    private void OnDisable()
    {
        transparentCover.gameObject.SetActive(false);
        notificationText.text = "";
    }
    
    public void EnableNotificationWindow(string message)
    {
        AddMessageToMessageQueue(message);
        TriggerGameStatePauseEvent();
        GameEventManager.Instance.AddStateToQueue(this);
    }

    public void AddMessageToMessageQueue(string message)
    {
        messageQueue.Enqueue(message);
    }

    public override void EventState()
    {
        EnableComponents();
        Debug.Log("In display notification window event");
        ButtonInputManager.Instance.DisableButtonsInList();
        notificationText.text = messageQueue.Dequeue();
    }

    public void CloseNotificationWindow()
    {
        DisableComponents();
        messageText = "";
        EndGameStatePauseEvent();
        ButtonInputManager.Instance.EnableButtonsInList();
        GameEventManager.Instance.EndEvent();
    }

    private void EnableComponents()
    {
        transparentCover.gameObject.SetActive(true);
        NotificationView.gameObject.SetActive(true);
    }
    
    private void DisableComponents()
    {
        transparentCover.gameObject.SetActive(false);
        NotificationView.gameObject.SetActive(false);
    }
    
}
