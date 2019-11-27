using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

/// <summary>
/// This class defines all characteristics and functions of the lobby menu.
/// </summary>
public class PhotonQuickManager : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private Text photonStatus, welcomeUser, roomName;

    [SerializeField]
    private GameObject roomLobbyCanvas;

    private static System.Random randomNumber = new System.Random();

    private static string[] randomPlayers = { "Edward", "Alex", "David", "Dominque", "Zack" };

    private void Awake()
    {
        PhotonNetwork.NickName = randomPlayers[randomNumber.Next(0,4)] + " " +randomNumber.Next(100,999).ToString();
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NetworkingClient.EnableLobbyStatistics = true;
        // Settings defined via PhotonServerSettings
        PhotonNetwork.ConnectUsingSettings();
       // DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {

    }

    public override void OnConnectedToMaster()
    {
        photonStatus.text = "Connected to master.";
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Destroy(photonStatus);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Current amount of rooms: "+ PhotonNetwork.CountOfRooms);
        if (PhotonNetwork.CountOfRooms < 1)
        {
            int randomInt = randomNumber.Next();
            RoomOptions options = new RoomOptions
            {
                IsOpen = true,
                IsVisible = true,
                MaxPlayers = 2,
                CustomRoomProperties = new Hashtable() { { "deckRandomValue", randomInt } }
            };
            PhotonNetwork.JoinOrCreateRoom(randomNumber.Next(10000, 99999).ToString(), options, null);
        }
        else
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Room entered....");
        welcomeUser.text = "Welcome, " + PhotonNetwork.LocalPlayer.NickName;
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        roomLobbyCanvas.SetActive(true);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Unable to join a room...");
    }
}
