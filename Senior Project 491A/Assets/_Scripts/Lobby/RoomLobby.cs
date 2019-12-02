using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// This class defines all characteristics and functions of the lobby menu.
/// </summary>
public class RoomLobby : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject mainLobbyCanvas, roomLobbyCanvas;

    [SerializeField]
    private Text buttonStatus;

    [SerializeField]
    private Button startMatchButton;

    private void Update()
    {
        string playerOne = "";
        string playerTwo = "";

        if (PhotonNetwork.IsMasterClient)
        {
            startMatchButton.gameObject.SetActive(true);

            if (PhotonNetwork.CurrentRoom.PlayerCount != 2)
            {
                startMatchButton.interactable = false;
            }
            else
            {
                playerOne = PhotonNetwork.CurrentRoom.CustomProperties["playerOneHero"].ToString();
                playerTwo = PhotonNetwork.CurrentRoom.CustomProperties["playerTwoHero"].ToString();

                if (playerOne != "" && playerTwo != "")
                {
                    buttonStatus.text = "Start Match";
                    startMatchButton.interactable = true;
                }

            }
        }
        else
        {
            startMatchButton.gameObject.SetActive(false);
        }
    }

    public void OnClick_StartMatch()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel(1);
    }

    public void OnClick_LeaveRoom()
    {
        buttonStatus.text = "Waiting...";
        mainLobbyCanvas.SetActive(true);
        roomLobbyCanvas.SetActive(false);
        PhotonNetwork.LeaveRoom(true);
    }
}
