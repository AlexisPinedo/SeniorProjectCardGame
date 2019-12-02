
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
    private Text buttonStatusText, heroP1Text, heroP2Text, nickNameP1Text, nickNameP2Text;

    [SerializeField]
    private Button startMatchButton;

    int playerOne, playerTwo = -1;
	bool assignedOne, assignedTwo = false;

    private void Update()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount != 2)
            {
                startMatchButton.interactable = false;
            }

            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("playerTwoHero"))
            {
                playerTwo = (int)PhotonNetwork.CurrentRoom.CustomProperties["playerTwoHero"];
                AssignPlayerTwo(true);

                buttonStatusText.text = "Start Match";
                startMatchButton.interactable = true;
            }

            playerOne = (int)PhotonManager.playerOneHero;
            AssignPlayerOne(true);
            startMatchButton.gameObject.SetActive(true);

        }
        else if(!PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("playerOneHero"))
            {
                playerOne = (int)PhotonNetwork.CurrentRoom.CustomProperties["playerOneHero"];
                AssignPlayerOne(true);
            }

            playerTwo = (int)PhotonManager.playerTwoHero;
            AssignPlayerTwo(true);

            startMatchButton.interactable = false;
        }
    }

   
    private void AssignPlayerOne(bool Switch)
    {
        //if(playerOne < 0 || assignedOne)
        //    return;

        unknownIconP1.gameObject.SetActive(!Switch);

        if (playerOne == 0)
        {
            //Valor
            valorIconP1.gameObject.SetActive(Switch);
            heroP1Text.text = "Valor";
        }
        else if (playerOne == 1)
        {
            //Vann
            vannIconP1.gameObject.SetActive(Switch);
            heroP1Text.text = "Vann";
        }
        else if (playerOne == 2)
        {
            //Vaughn
            vaughnIconP1.gameObject.SetActive(Switch);
            heroP1Text.text = "Vaughn";
        }
        else if (playerOne == 3)
        {
            //Veda
            vedaIconP1.gameObject.SetActive(Switch);
            heroP1Text.text = "Veda";
        }
        else if (playerOne == 4)
        {
            //Vicky
            vickyIconP1.gameObject.SetActive(Switch);
            heroP1Text.text = "Vicky";
        }
        else if (playerOne == 5)
        {
            //Vito
            vitoIconP1.gameObject.SetActive(Switch);
            heroP1Text.text = "Vito";
        }

		assignedOne = Switch;
    }

    private void AssignPlayerTwo(bool Switch)
    {
        //if (playerTwo < 0  || assignedTwo)
        //    return;

        unknownIconP2.gameObject.SetActive(!Switch);

        if (playerTwo == 0)
        {
            //Valor
            valorIconP2.gameObject.SetActive(Switch);
            heroP2Text.text = "Valor";
        }
        else if (playerTwo == 1)
        {
            //Vann
            vannIconP2.gameObject.SetActive(Switch);
            heroP2Text.text = "Vann";
        }
        else if (playerTwo == 2)
        {
            //Vaughn
            vaughnIconP2.gameObject.SetActive(Switch);
            heroP2Text.text = "Vaughn";
        }
        else if (playerTwo == 3)
        {
            //Veda
            vedaIconP2.gameObject.SetActive(Switch);
            heroP2Text.text = "Veda";
        }
        else if (playerTwo == 4)
        {
            //Vicky
            vickyIconP2.gameObject.SetActive(Switch);
            heroP2Text.text = "Vicky";
        }
        else if (playerTwo == 5)
        {
            //Vito
            vitoIconP2.gameObject.SetActive(Switch);
            heroP2Text.text = "Vito";
        }

		assignedTwo = Switch;
    }


    private void Awake()
    {
        GetCurrentRoomPlayers();
    }

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
            }
            else
            {
                nickNameP2Text.text = player.NickName;
            }
        }
    }


    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (newPlayer.IsMasterClient)
        {
            nickNameP1Text.text = newPlayer.NickName;
        }
        else
        {
            nickNameP2Text.text = newPlayer.NickName;
        }
        Debug.Log("Adding player...");
    }

    public void OkClick_ForceEnterLobby()
    {
        PhotonNetwork.LeaveRoom();
        mainLobbyCanvas.SetActive(true);
        NotificationWindowEvent.Instance.NotificationView.gameObject.SetActive(false);
        NotificationWindowEvent.Instance.transparentCover.gameObject.SetActive(false);
    }

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
