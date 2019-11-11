using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// This class defines all characteristics and functions of the lobby menu.
/// </summary>
public class RoomQuickLobby : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private Text buttonStatus;

    [SerializeField]
    private Button startMatchButton;

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startMatchButton.gameObject.SetActive(true);
            if (PhotonNetwork.CurrentRoom.PlayerCount != 2)
            {
                startMatchButton.interactable = false;
            }
            else
            {
                buttonStatus.text = "Start Match";
                startMatchButton.interactable = true;
            }
        }
        else
        {
            startMatchButton.interactable = false;
        }
    }

    public void OnClick_StartMatch()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel(1);
    }
}
