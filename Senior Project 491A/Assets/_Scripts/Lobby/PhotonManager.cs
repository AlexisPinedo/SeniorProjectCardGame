using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections;

/// <summary>
/// This class defines all characteristics and functions of the lobby menu.
/// </summary>
public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private InputField nameInp, roomInput;

    [SerializeField]
    private Text photonStatus, welcomeUser, roomName;

    [SerializeField]
    private GameObject mainLobbyCanvas, roomLobbyCanvas, heroPickerPopup;

    [SerializeField]
    private GameObject createRoomPanel, backButton, createRoomButton, gameLogo, tryAgainButton;

    [SerializeField]
    private Button photonGenerateRoomButton;

    /// <summary>
    /// Reference for the Player's Hero
    /// </summary>
    public static Heroes playerOneHero, playerTwoHero;

    private static System.Random randNum = new System.Random();

    private readonly int minRoomNameLen = 4;

    private bool Connected = false;

    public override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(AttemptingConnection());
    }

    IEnumerator AttemptingConnection()
    {
        photonStatus.text = "Attempting conenction ...";

        //Disable while firebase is not implemented on screen 
        //PhotonNetwork.NickName = AuthManager.sharedInstance.GetCurrentUser().UserId;
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

    #region PUN Networking
    public override void OnConnectedToMaster()
    {
        Connected = true;
        photonStatus.text = "Connected ...";
        Destroy(photonStatus.gameObject);
        Destroy(tryAgainButton.gameObject);
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    /// <summary>
    /// PhotonNetwork specific. Method called after successfully joining a PUN lobby.
    /// </summary>
    public override void OnJoinedLobby()
    {
        welcomeUser.text = "Welcome, " + PhotonNetwork.LocalPlayer.NickName;
        mainLobbyCanvas.SetActive(true);
        createRoomButton.SetActive(true);
    }

    /// <summary>
    /// PhotonNetwork specific. Method called after successfully joining a PUN room.
    /// </summary>
    public override void OnJoinedRoom()
    {
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        mainLobbyCanvas.SetActive(false);
        SelectHero();
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

    /// <summary>
    /// Generates a new Photon Room and sends the user to the Hero selection screen.
    /// </summary>
    public void GeneratePhotonRoom()
    {
        createRoomButton.SetActive(false);
        createRoomPanel.SetActive(false);

        System.Random randomNumber = new System.Random();
        int randomInt = randomNumber.Next();

        RoomOptions options = new RoomOptions
        {
            IsOpen = true,
            IsVisible = true,
            MaxPlayers = 2,
            CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "deckRandomValue", randomInt } }
        };

        PhotonNetwork.JoinOrCreateRoom(roomInput.text, options, null);
    }

    /// <summary>
    /// Detects if the room attempting to be created has the sufficient number of characters.
    /// </summary>
    public void OnRoomNameField_Changed()
    {
        if (roomInput.text.Length >= minRoomNameLen)
            photonGenerateRoomButton.interactable = true;
        else
            photonGenerateRoomButton.interactable = false;
    }

    /// <summary>
    /// Hides the button and displays three objects: a back button, an input field (the room to be created), and the accept button.
    /// </summary>
    public void OnClick_CreateRoom()
    {
        createRoomButton.SetActive(false);
        createRoomPanel.SetActive(true);
    }

    /// <summary>
    /// Hides the create room panel and shows the create room button.
    /// </summary>
    public void OnClick_CreateRoomBack()
    {
        createRoomButton.SetActive(true);
        createRoomPanel.SetActive(false);
    }

    /// <summary>
    /// Brings the player to the canvas to select a Hero.
    /// </summary>
    public void SelectHero()
    {
        mainLobbyCanvas.SetActive(false);
        heroPickerPopup.SetActive(true);
    }

    public void OnClick_TryConnectionAgain()
    {
        StartCoroutine(AttemptingConnection());
    }
}
