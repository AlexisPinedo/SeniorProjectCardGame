using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

/// <summary>
/// This class defines all characteristics and functions of the lobby menu.
/// </summary>
public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private InputField nameInp;

    [SerializeField]
    private Text photonStatus, welcomeUser, roomName;

    [SerializeField]
    private GameObject mainLobbyCanvas, heroPickerPopup;

    [SerializeField]
    private GameObject createRoomButton, tryAgainButton;

    public static Heroes playerOneHero, playerTwoHero;

    private bool Connected;

    public override void OnEnable()
    {
        base.OnEnable();

        if (PhotonNetwork.IsConnected)
        {
            /**
             * Connected from previous game.
             * Grab player nickname and disconnect them.
             * Attempt to rejoin the lobby with nickname from previous connection.
             */

            nameInp.text = PhotonNetwork.LocalPlayer.NickName;
            PhotonNetwork.Disconnect();
            StartCoroutine(AttemptingConnection());
            photonStatus.gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(AttemptingConnection());
            photonStatus.gameObject.SetActive(true);
        }
    }

    #region PUN Networking
    IEnumerator AttemptingConnection()
    {
        photonStatus.text = "Joining the lobby ...";

        //Disable while firebase is not implemented on screen 
        PhotonNetwork.NickName = nameInp.text;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NetworkingClient.EnableLobbyStatistics = true;
        PhotonNetwork.ConnectUsingSettings();

        yield return new WaitForSeconds(4);

        if (!Connected)
        {
            photonStatus.text = "Failed to connect ...";
            tryAgainButton.SetActive(true);
        }
    }

    public void OnClick_TryConnectionAgain()
    {
        StartCoroutine(AttemptingConnection());
    }

    public override void OnConnectedToMaster()
    {
        Connected = true;
        if (photonStatus.gameObject.activeSelf)
            photonStatus.gameObject.SetActive(false);
        if (tryAgainButton.activeSelf)
            tryAgainButton.gameObject.SetActive(false);
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    /// <summary>
    /// PhotonNetwork specific. Method called after successfully joining a PUN lobby.
    /// </summary>
    public override void OnJoinedLobby()
    {
        welcomeUser.text = "Welcome, " + PhotonNetwork.LocalPlayer.NickName;

        if (!NotificationWindowEvent.Instance.NotificationView.gameObject.activeSelf)
        {
            createRoomButton.SetActive(true);
            mainLobbyCanvas.SetActive(true);
        }
    }

    /// <summary>
    /// PhotonNetwork specific. Method called after successfully joining a PUN room.
    /// </summary>
    public override void OnJoinedRoom()
    {
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        mainLobbyCanvas.SetActive(false);
        heroPickerPopup.SetActive(true);
    }

    /// <summary>
    /// PhotonNetwork specific. Method called upon failure to join a PUN room.
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("<Color=Red><a>Join Room Failed</a></Color>", this);
    }
    #endregion
}
