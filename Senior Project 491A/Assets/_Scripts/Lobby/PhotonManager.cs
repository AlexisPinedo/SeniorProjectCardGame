using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

/// <summary>
/// This class defines all characteristics and functions of the lobby menu.
/// </summary>
public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private InputField nameInp, roomInput;

    [SerializeField]
    private Text photonStatus, welcomeUser, roomName, heroSelectedText;

    [SerializeField]
    private GameObject mainLobbyCanvas, roomLobbyCanvas, heroPickerPopup;

    [SerializeField]
    private GameObject createRoomPanel, backButton, createRoomButton, gameLogo;

    [SerializeField]
    private Button photonGenerateRoomButton;

    /// <summary>
    /// Reference for the Player's Hero
    /// </summary>
    Heroes playerOneHero, playerTwoHero;

    private static System.Random randNum = new System.Random();

    private readonly int minRoomNameLen = 4;

    /// <summary>
    /// Currently empty.
    /// </summary>
    private void Awake()
    {
    }

    public override void OnEnable()
    {
        base.OnEnable();

        photonStatus.text = "Establishing conenction with server";

        if (nameInp.text == null || nameInp.text == "")
        {
            nameInp.text = "Debugging Offline";
        }
        PhotonNetwork.NickName = nameInp.text;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NetworkingClient.EnableLobbyStatistics = true;

        // Settings defined via PhotonServerSettings
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    /// Currently empty
    /// </summary>
    void Update()
    {
    }

    #region PUN Networking
    public override void OnConnectedToMaster()
    {
        photonStatus.text = "Connected to master.";

        PhotonNetwork.JoinLobby(TypedLobby.Default);
        photonStatus.text = "";
    }

    /// <summary>
    /// PhotonNetwork specific. Method called after successfully joining a PUN lobby.
    /// </summary>
    public override void OnJoinedLobby()
    {
        Debug.Log("\t" + PhotonNetwork.LocalPlayer.NickName + " has joined the lobby");
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

        Debug.Log("\t" + PhotonNetwork.LocalPlayer.NickName + " has joined the room.\n\tSelecting a hero now!");

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
            CustomRoomProperties = new Hashtable() { { "deckRandomValue", randomInt } }
        };

        PhotonNetwork.JoinOrCreateRoom(roomInput.text, options, null);
    }

    /// <summary>
    /// Detects if the room attempting to be created has the sufficient number of characters.
    /// </summary>
    public void OnRoomNameField_Changed()
    {
        if (roomInput.text.Length >= minRoomNameLen)
        {
            photonGenerateRoomButton.interactable = true;
        }
        else
        {
            photonGenerateRoomButton.interactable = false;
        }
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
        gameLogo.SetActive(false);
        mainLobbyCanvas.SetActive(false);
        heroPickerPopup.SetActive(true);
    }

    /// <summary>
    /// Sets the text for the hero based upon which icon was clicked.
    /// </summary>
    /// <param name="heroName"></param>
    public void OnClick_HeroClicked(string heroName)
    {
        heroSelectedText.text = heroName;
    }

    /// <summary>
    /// Confirms the Hero selection for each player.
    /// </summary>
    public void OnClick_ConfirmHeroSelection()
    {
        if (heroSelectedText.text != "")
        {
            Debug.Log("\tCustom props: " + PhotonNetwork.CurrentRoom.CustomProperties);
            Hashtable roomProps = PhotonNetwork.CurrentRoom.CustomProperties;

            int heroNum = -1;

            switch (heroSelectedText.text)
            {
                case "Valor":
                    heroNum = 0;
                    break;
                case "Vann":
                    heroNum = 1;
                    break;
                case "Vaughn":
                    heroNum = 2;
                    break;
                case "Veda":
                    heroNum = 3;
                    break;
                case "Vicky":
                    heroNum = 4;
                    break;
                case "Vito":
                    heroNum = 5;
                    break;
                default:
                    heroNum = -1;
                    break;
            }

            if (PhotonNetwork.IsMasterClient)
            {
                playerOneHero = (Heroes)heroNum;
                roomProps.Add("playerOneHero", playerOneHero);
                Debug.Log("Player 1 selected " + playerOneHero);
            }
            else
            {
                playerTwoHero = (Heroes)heroNum;
                roomProps.Add("playerTwoHero", playerTwoHero);
                Debug.Log("Player 2 selected " + playerTwoHero);
            }

            PhotonNetwork.CurrentRoom.SetCustomProperties(roomProps);

            heroPickerPopup.SetActive(false);

        }
        else
        {
            heroSelectedText.text = "Hero Selected: Pick A Hero";
        }

        roomLobbyCanvas.SetActive(true);
    }
}
