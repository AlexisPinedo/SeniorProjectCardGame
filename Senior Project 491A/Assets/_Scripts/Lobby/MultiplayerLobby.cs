using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

/// <summary>
/// Multiplayer lobby handler with references to other relevant canvases.
/// </summary>
public class MultiplayerLobby : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject multiplayerLobby, mainMenuScreen, photonManager;

    [SerializeField]
    private GameObject createRoomPanel, createRoomButton;

    [SerializeField]
    private Button photonGenerateRoomButton;

    [SerializeField]
    private InputField roomInput;

    private readonly int minRoomNameLen = 4;

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

    #region OnClick Methods

    /// <summary>
    /// Generates a new Photon Room and sends the user to the Hero selection screen.
    /// </summary>
    public void OnClick_GeneratePhotonRoom()
    {
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
        roomInput.text = "";
        createRoomButton.SetActive(true);
        createRoomPanel.SetActive(false);
    }

    public void OnClick_SignOut()
    {

        //PhotonNetwork.LeaveLobby();
        photonManager.SetActive(false);
        multiplayerLobby.SetActive(false);
        PhotonNetwork.Disconnect();
        MainMenu.multiplayerMode = false;
        mainMenuScreen.SetActive(true);


        //PhotonNetwork.OfflineMode = true;

        OnClick_CreateRoomBack();
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
        roomInput.text = "";
        createRoomButton.SetActive(true);
        createRoomPanel.SetActive(false);
    }
    #endregion
}
