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
    public Image transparentCover;
    [SerializeField]
    private TextMeshProUGUI notificationText;
    [SerializeField] 
    public Image NotificationView;
    [SerializeField]
    private Button startBattleButton, endTurnButton, okButton;
    //[SerializeField]
    //private Text currency, power;

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
        if(!PhotonNetwork.OfflineMode)
            if (!PhotonNetwork.CurrentRoom.IsOpen)
                ButtonInputManager.Instance.DisableButtonsInList();
        notificationText.text = messageQueue.Dequeue();
        if (notificationText.text.Contains("may"))
            okButton.gameObject.SetActive(true);
        else if (notificationText.text.Contains("selecting"))
            okButton.gameObject.SetActive(false);
        //This was throwing a null ref exception in offline mode 
        //if (!PhotonNetwork.OfflineMode || !PhotonNetwork.CurrentRoom.IsOpen)
        //    photonView.RPC("RemoteEventStateNotficationWindow", RpcTarget.Others, notificationText.text);
        if (!PhotonNetwork.OfflineMode)
            photonView.RPC("RemoteEventStateNotficationWindow", RpcTarget.Others, notificationText.text);
    }

    [PunRPC]
    private void RemoteEventStateNotficationWindow(string text)
    {
        EnableComponents();
        TriggerGameStatePauseEvent();
        ButtonInputManager.Instance.DisableButtonsInList();

        if (text.Contains("may"))
        {
            notificationText.text = (NetworkOwnershipTransferManger.currentPhotonPlayer.NickName + " is selecting 1 card");
            okButton.gameObject.SetActive(false);
        }
        else if (text.Contains("selecting"))
        {
            notificationText.text = ("You may select 1 card");
            okButton.gameObject.SetActive(true);
        }
        else
            notificationText.text = text;

    }

    public void CloseNotificationWindow()
    {
        if (NetworkOwnershipTransferManger.currentPhotonPlayer.IsLocal)
        {
            DisableComponents();
            messageText = "";
            EndGameStatePauseEvent();
            DisableMinionCardContainters();
            ButtonInputManager.Instance.EnableButtonsInList();
            GameEventManager.Instance.EndEvent();
            if (!PhotonNetwork.OfflineMode)
                photonView.RPC("RemoteCloseNotfication", RpcTarget.Others);
        }
        else
        {

        }
    }

    [PunRPC]
    private void RemoteCloseNotfication()
    {
        CloseNotificationWindow();
        DisableComponents();
        messageText = "";
        EndGameStatePauseEvent();
        DisableMinionCardContainters();
        ButtonInputManager.Instance.EnableButtonsInList();
        GameEventManager.Instance.EndEvent();
    }

    private void EnableComponents()
    {
        transparentCover.gameObject.SetActive(true);
        NotificationView.gameObject.SetActive(true);


        if (startBattleButton == null)
            return;

        if (NetworkOwnershipTransferManger.currentPhotonPlayer.IsLocal)
        {
            startBattleButton.gameObject.SetActive(false);
            endTurnButton.gameObject.SetActive(false);
        }
    }
    
    private void DisableComponents()
    {
        transparentCover.gameObject.SetActive(false);
        NotificationView.gameObject.SetActive(false);

        if (startBattleButton == null)
            return;

        if (NetworkOwnershipTransferManger.currentPhotonPlayer.IsLocal)
        {
            startBattleButton.gameObject.SetActive(true);
            endTurnButton.gameObject.SetActive(true);
        }
    }
    
}
