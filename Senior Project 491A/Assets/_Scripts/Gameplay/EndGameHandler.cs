using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public class EndGameHandler : MonoBehaviour
{
    public static Action GameEnded;

    [SerializeField] private GameObject EndGameWindowPanel;

    [SerializeField] private GameObject EndButtonWindowButton;

    [SerializeField] private TextMeshProUGUI EndGameText;

    private void Awake()
    {
        EndGameText = EndGameWindowPanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        GameEnded += DisplayEndGameWindow;
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    private void OnDisable()
    {
        GameEnded -= DisplayEndGameWindow;
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    private void SetEndGameText(string text)
    {
        EndGameText.text = text;
    }

    public static void TriggerEndGame()
    {
        GameEnded?.Invoke();
    }

    private void DisplayEndGameWindow()
    {
        EndGameWindowPanel.SetActive(true);
        EndButtonWindowButton.SetActive(true);
    }

    public void ReturnToMainMenu()
    {

        //if (!PhotonNetwork.OfflineMode)
        //{
        //    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient };
        //    SendOptions sendOptions = new SendOptions { Reliability = true };
        //    PhotonNetwork.RaiseEvent(NetworkOwnershipTransferManger.endGameEvent, null, raiseEventOptions, sendOptions);
        //}
        //else
        //SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
        PhotonNetwork.LoadLevel(0);
    }

    private void OnEvent(EventData photonEvent)
    {
        //byte recievedCode = photonEvent.Code;
        //if (recievedCode == NetworkOwnershipTransferManger.endGameEvent)
        //    SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
    }

}
