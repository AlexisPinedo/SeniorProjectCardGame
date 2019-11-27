using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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
        AddStateToQueue();
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
        photonView.RPC("RemoteEventState", RpcTarget.Others, notificationText.text);
    }

    [PunRPC]
    private void RemoteEventState(string text)
    {
        EnableComponents();
        TriggerGameStatePauseEvent();
        ButtonInputManager.Instance.DisableButtonsInList();
        notificationText.text = text;
    }

    public void CloseNotificationWindow()
    {
        Debug.Log("Photon View Owner:" + photonView.Owner.NickName);
        Debug.Log("Current photon player: " + NetworkOwnershipTransferManger.currentPhotonPlayer.NickName);


        DisableComponents();
        messageText = "";
        EndGameStatePauseEvent();
        DisableMinionCardContainters();
        GameEventManager.Instance.EndEvent();


        if (photonView.IsMine)
        {
            ButtonInputManager.Instance.EnableButtonsInList();
            photonView.RPC("RemoteCloseNotfication", RpcTarget.Others);
        }
        else
        {
            ButtonInputManager.Instance.DisableButtonsInList();
        }

        //if (NetworkOwnershipTransferManger.currentPhotonPlayer.IsLocal)
        //{
        //    DisableComponents();
        //    messageText = "";
        //    EndGameStatePauseEvent();
        //    DisableMinionCardContainters();
        //    ButtonInputManager.Instance.EnableButtonsInList();
        //    GameEventManager.Instance.EndEvent();
        //    photonView.RPC("RemoteCloseNotfication", RpcTarget.Others);
        //}
    }

    [PunRPC]
    private void RemoteCloseNotfication()
    {
        CloseNotificationWindow();
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
