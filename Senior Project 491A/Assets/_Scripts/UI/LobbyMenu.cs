using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// This class defines all characteristics and functions of the lobby menu.
/// </summary>
public class LobbyMenu : MonoBehaviourPunCallbacks
{
    #region UI references
    [SerializeField]
    private GameObject nameScreen, connectScreen;

    [SerializeField]
    private GameObject createNameBtn;

    [SerializeField]
    private GameObject joinRoomBtn, joinRoomBtnBack, joinRoomBtnAccept, joinInpObj;

    [SerializeField]
    private GameObject createRoomBtn, createRoomBtnBack, createRoomBtnAccept, createInpObj;

    [SerializeField]
    private InputField nameInp, createRoomInp, joinRoomInp;
    #endregion

    #region Private Fields
    /// <summary>
    /// Minimum number of characters required for a Player's nickname.
    /// </summary>
    private readonly int minNameLen = 5;

    /// <summary>
    /// Minimum number of characters required for a Room name.
    /// </summary>
    private readonly int minRoomNameLen = 4;
    #endregion

    private void Awake()
    {
        // Connect to Photon server
        PhotonNetwork.ConnectUsingSettings();
    }

    #region Pun Overrides
    /// <summary>
    /// Called after connection to server is established.
    /// </summary>
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    /// <summary>
    /// Called after connection to master is established.
    /// </summary>
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby: " + PhotonNetwork.CurrentLobby.ToString());
        nameScreen.SetActive(true);
        createNameBtn.SetActive(false);
    }

    /// <summary>
    /// Called after a room is joined.
    /// </summary>
    public override void OnJoinedRoom()
    {
        // Go to game - in our case, the MVP scene *for now*
        PhotonNetwork.LoadLevel(1); // 1 = build index!
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("<Color=Red><a>Join Room Failed</a></Color>", this);
        OnClick_JoinRoomBack();
    }
    #endregion

    #region UI Methods
    /// <summary>
    /// On-click method for nickname creation.
    /// </summary>
    public void OnClick_CreateNameBtn()
    {
        if (nameInp.text.Length >= minNameLen)
        {
            // Set Player's nickname
            PhotonNetwork.NickName = nameInp.text;
            Debug.Log("PUN nickname: " + nameInp.text);

            nameScreen.SetActive(false);
            connectScreen.SetActive(true);
        }
    }

    /// <summary>
    /// Shows the "Accept" button only if the minimum number of characters is
    /// entered for a nickname.
    /// </summary>
    public void OnNameField_Changed()
    {
        if (nameInp.text.Length >= minNameLen)
        {
            createNameBtn.SetActive(true);
        }
        else
        {
            createNameBtn.SetActive(false);
        }
    }

    /// <summary>
    /// Hides the Join Room button and displays the back and accept buttons, and the input field.
    /// </summary>
    public void OnClick_JoinRoom()
    {
        joinRoomBtn.SetActive(false);
        joinInpObj.SetActive(true);
        joinRoomBtnAccept.SetActive(true);
        joinRoomBtnBack.SetActive(true);
    }

    /// <summary>
    /// Hides the input field, back and accept buttons, and shows the Join Room button.
    /// </summary>
    public void OnClick_JoinRoomBack()
    {
        joinRoomBtn.SetActive(true);
        joinInpObj.SetActive(false);
        joinRoomBtnBack.SetActive(false);
        joinRoomBtnAccept.SetActive(false);
    }

    /// <summary>
    /// On-click method for the Join Room's Accept button.
    /// </summary>
    public void OnClick_JoinRoomAccept()
    {
        // TODO: Find a more UI/UX centric way to convey this message
        // (i.e., room names need a minimum length?)
        if (joinRoomInp.text.Length >= minRoomNameLen)
        {
            Debug.Log("Joining room: " + joinRoomInp.text);
            PhotonNetwork.JoinRoom(joinRoomInp.text, null);
        }
        else
        {
            joinRoomInp.placeholder.GetComponent<Text>().text = "Nope";
        }
    }

    /// <summary>
    /// On-click method for the Create Room button.
    /// 
    /// NOT IMPLEMENTED YET.
    /// </summary>
    public void OnClick_CreateRoom()
    {
        createRoomBtn.SetActive(false);
        createInpObj.SetActive(true);
        createRoomBtnAccept.SetActive(true);
        createRoomBtnBack.SetActive(true);
    }

    public void OnClick_CreateRoomAccept()
    {
        if (createRoomInp.text.Length >= minRoomNameLen)
        {
            RoomOptions roomOpt = new RoomOptions
            {
                MaxPlayers = 2
            };

            PhotonNetwork.CreateRoom(createRoomInp.text, new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default, null);
            Debug.Log("New room created called " + createRoomInp.text);
        }
        else
        {
            Debug.LogError("Room name needs more characters");
        }
    }

    public void OnClick_CreateRoomBack()
    {
        createRoomBtn.SetActive(true);
        createInpObj.SetActive(false);
        createRoomBtnAccept.SetActive(false);
        createRoomBtnBack.SetActive(false);
    }
    #endregion
}
