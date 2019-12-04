
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

/// <summary>
/// This class defines all characteristics and functions of the lobby menu.
/// </summary>
public class RoomLobby : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject mainLobbyCanvas, roomLobbyCanvas;

    [SerializeField]
    private GameObject valorIconP1, vannIconP1, vaughnIconP1, vedaIconP1, vickyIconP1, vitoIconP1, unknownIconP1;

    [SerializeField]
    private GameObject valorIconP2, vannIconP2, vaughnIconP2, vedaIconP2, vickyIconP2, vitoIconP2, unknownIconP2;

    [SerializeField]
    private Text buttonStatusText, heroP1Text, heroP2Text, nickNameP1Text, nickNameP2Text, roomStatus;

    [SerializeField]
    private Button startMatchButton;

    int playerOne, playerTwo = -1;

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (roomLobbyCanvas.activeSelf == false)
        {
            Debug.Log("Canvas is active");
            startMatchButton.interactable = false;
            startMatchButton.gameObject.SetActive(true);
        }
        else
        {
            GetCurrentRoomPlayers();
        }

        if (propertiesThatChanged.ContainsKey("playerOneHero"))
        {
            Debug.Log("Properties updated with playerOneHero ");
            playerOne = (int)PhotonNetwork.CurrentRoom.CustomProperties["playerOneHero"];
            AssignPlayerOne(true);
        }

        if (propertiesThatChanged.ContainsKey("playerTwoHero"))
        {
            Debug.Log("Properties updated with playerTwoHero ");
            playerTwo = (int)PhotonNetwork.CurrentRoom.CustomProperties["playerTwoHero"];
            AssignPlayerTwo(true);

            if (PhotonNetwork.IsMasterClient)
            {
                roomStatus.text = "Match is ready to begin!";
                buttonStatusText.text = "Start Match";
                startMatchButton.interactable = true;
            }
            else
            {
                if (!PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("playerOneHero"))
                    roomStatus.text = nickNameP1Text.text + " is selecting a hero...";
                else
                    roomStatus.text = nickNameP1Text.text + " is preparing to start the match!";
            }
        }
    }

    /// <summary>
    /// Assigns the Hero to Player One that they selected.
    /// </summary>
    /// <param name="Switch"></param>
    private void AssignPlayerOne(bool Switch)
    {
        unknownIconP1.gameObject.SetActive(!Switch);

        if (playerOne == 0)
        {
            valorIconP1.gameObject.SetActive(Switch);
            heroP1Text.text = "Valor";
        }
        else if (playerOne == 1)
        {
            vannIconP1.gameObject.SetActive(Switch);
            heroP1Text.text = "Vann";
        }
        else if (playerOne == 2)
        {
            vaughnIconP1.gameObject.SetActive(Switch);
            heroP1Text.text = "Vaughn";
        }
        else if (playerOne == 3)
        {
            vedaIconP1.gameObject.SetActive(Switch);
            heroP1Text.text = "Veda";
        }
        else if (playerOne == 4)
        {
            vickyIconP1.gameObject.SetActive(Switch);
            heroP1Text.text = "Vicky";
        }
        else if (playerOne == 5)
        {
            vitoIconP1.gameObject.SetActive(Switch);
            heroP1Text.text = "Vito";
        }
    }

    /// <summary>
    /// Assigns the Hero to Player Two that they selected.
    /// </summary>
    /// <param name="Switch"></param>
    private void AssignPlayerTwo(bool Switch)
    {
        unknownIconP2.gameObject.SetActive(!Switch);

        if (playerTwo == 0)
        {
            valorIconP2.gameObject.SetActive(Switch);
            heroP2Text.text = "Valor";
        }
        else if (playerTwo == 1)
        {
            vannIconP2.gameObject.SetActive(Switch);
            heroP2Text.text = "Vann";
        }
        else if (playerTwo == 2)
        {
            vaughnIconP2.gameObject.SetActive(Switch);
            heroP2Text.text = "Vaughn";
        }
        else if (playerTwo == 3)
        {
            vedaIconP2.gameObject.SetActive(Switch);
            heroP2Text.text = "Veda";
        }
        else if (playerTwo == 4)
        {
            vickyIconP2.gameObject.SetActive(Switch);
            heroP2Text.text = "Vicky";
        }
        else if (playerTwo == 5)
        {
            vitoIconP2.gameObject.SetActive(Switch);
            heroP2Text.text = "Vito";
        }
    }

    /// <summary>
    /// Gets (???) the Players active in the current Photon Room.
    /// </summary>
    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;

        Debug.Log("Getting players...");

        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if (player.IsMasterClient)
            {
                nickNameP1Text.text = player.NickName;
                Debug.Log("Assigned player 1 nickname...");
            }
            else
            {
                nickNameP2Text.text = player.NickName;
                Debug.Log("Assigned player 2 nickname...");
            }
        }
    }

    /// <summary>
    /// Handles actions taken when a Player has entered a Room.
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (newPlayer.IsMasterClient)
        {
            nickNameP1Text.text = newPlayer.NickName;
        }
        else
        {
            nickNameP2Text.text = newPlayer.NickName;
            roomStatus.text = newPlayer.NickName + " is selecting a hero...";
        }
    }

    public void OkClick_ForceEnterLobby()
    {
        mainLobbyCanvas.SetActive(true);
        NotificationWindowEvent.Instance.NotificationView.gameObject.SetActive(false);
        NotificationWindowEvent.Instance.transparentCover.gameObject.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }

    /// <summary>
    /// Resets the Room's attributes.
    /// </summary>
    private void ResetRoom()
    {
        AssignPlayerOne(false);
        AssignPlayerTwo(false);
        playerOne = -1;
        playerTwo = -1;
        heroP1Text.text = "?";
        heroP2Text.text = "?";
        nickNameP1Text.text = "";
        nickNameP2Text.text = "";
    }

    /// <summary>
    /// Handles actions taken when a Player has left a Room.
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        roomLobbyCanvas.SetActive(false);
        ResetRoom();
        NotificationWindowEvent.Instance.EnableNotificationWindow("A player has left matchmaking. \n Returning to Lobby.");
    }

    public void OnClick_StartMatch()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel(1);
    }

    public void OnClick_LeaveRoom()
    {
        buttonStatusText.text = "Waiting...";
        ResetRoom();
        roomLobbyCanvas.SetActive(false);
        mainLobbyCanvas.SetActive(true);
        PhotonNetwork.LeaveRoom(true);
    }
}
