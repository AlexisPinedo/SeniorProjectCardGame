
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
                AssignPlayerTwo();

                buttonStatusText.text = "Start Match";
                startMatchButton.interactable = true;
            }
            else
            {
                Debug.Log("Pending hero 2 key");
            }

            playerOne = (int)PhotonManager.playerOneHero;
            AssignPlayerOne();
            startMatchButton.gameObject.SetActive(true);

        }
        else if (!PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("playerOneHero"))
            {
                playerOne = (int)PhotonNetwork.CurrentRoom.CustomProperties["playerOneHero"];
                AssignPlayerOne();
            }
            else
            {
                Debug.Log("Pending hero 1 key");
            }

            playerTwo = (int)PhotonManager.playerTwoHero;
            AssignPlayerTwo();

            startMatchButton.interactable = false;
        }

        Debug.Log("Keys: " + PhotonNetwork.CurrentRoom.CustomProperties.Count);
    }


    private void AssignPlayerOne()
    {
        if (playerOne < 0 || assignedOne)
            return;

        Debug.Log("Assigned P1");

        unknownIconP1.gameObject.SetActive(false);

        if (playerOne == 0)
        {
            //Valor
            valorIconP1.gameObject.SetActive(true);
            heroP1Text.text = "Valor";
        }
        else if (playerOne == 1)
        {
            //Vann
            vannIconP1.gameObject.SetActive(true);
            heroP1Text.text = "Vann";
        }
        else if (playerOne == 2)
        {
            //Vaughn
            vaughnIconP1.gameObject.SetActive(true);
            heroP1Text.text = "Vaughn";
        }
        else if (playerOne == 3)
        {
            //Veda
            vedaIconP1.gameObject.SetActive(true);
            heroP1Text.text = "Veda";
        }
        else if (playerOne == 4)
        {
            //Vicky
            vickyIconP1.gameObject.SetActive(true);
            heroP1Text.text = "Vicky";
        }
        else if (playerOne == 5)
        {
            //Vito
            vitoIconP1.gameObject.SetActive(true);
            heroP1Text.text = "Vito";
        }

        assignedOne = true;
    }

    private void AssignPlayerTwo()
    {
        if (playerTwo < 0 || assignedTwo)
            return;

        Debug.Log("Assigned P2");

        unknownIconP2.gameObject.SetActive(false);

        if (playerTwo == 0)
        {
            //Valor
            valorIconP2.gameObject.SetActive(true);
            heroP2Text.text = "Valor";
        }
        else if (playerTwo == 1)
        {
            //Vann
            vannIconP2.gameObject.SetActive(true);
            heroP2Text.text = "Vann";
        }
        else if (playerTwo == 2)
        {
            //Vaughn
            vaughnIconP2.gameObject.SetActive(true);
            heroP2Text.text = "Vaughn";
        }
        else if (playerTwo == 3)
        {
            //Veda
            vedaIconP2.gameObject.SetActive(true);
            heroP2Text.text = "Veda";
        }
        else if (playerTwo == 4)
        {
            //Vicky
            vickyIconP2.gameObject.SetActive(true);
            heroP2Text.text = "Vicky";
        }
        else if (playerTwo == 5)
        {
            //Vito
            vitoIconP2.gameObject.SetActive(true);
            heroP2Text.text = "Vito";
        }

        assignedTwo = true;
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

    public override void OnPlayerLeftRoom(Photon.Realtime.Player leftPlayer)
    {
        mainLobbyCanvas.SetActive(true);
        roomLobbyCanvas.SetActive(false);
        PhotonNetwork.LeaveRoom();
        //string roomName = PhotonNetwork.CurrentRoom.Name;

        //PhotonNetwork.LeaveRoom();

        //System.Random randomNumber = new System.Random();
        //int randomInt = randomNumber.Next();

        //RoomOptions options = new RoomOptions
        //{
        //    IsOpen = true,
        //    IsVisible = true,
        //    MaxPlayers = 2,
        //    CustomRoomProperties = new Hashtable() { { "deckRandomValue", randomInt } }
        //};

        //PhotonNetwork.JoinOrCreateRoom(roomName, options, null);

        //Hashtable roomProps = PhotonNetwork.CurrentRoom.CustomProperties;

        //Heroes heroSelection;

        //if (leftPlayer.IsMasterClient)
        //{
        //    heroSelection = (Heroes)playerOne;
        //}
        //else
        //{
        //    heroSelection = (Heroes)playerTwo;
        //}

        //roomProps.Add("playerOneHero", heroSelection);
        //if (leftPlayer.IsMasterClient)
        //{
        //    unknownIconP1.gameObject.SetActive(true);
        //    nickNameP1Text.text = "";
        //    heroP1Text.text = "?";
        //    playerOne = -1;
        //    PhotonNetwork.CurrentRoom.CustomProperties.Remove("playerOneHero");

        //}
        //else
        //{
        //    unknownIconP2.gameObject.SetActive(true);
        //    nickNameP2Text.text = "";
        //    heroP2Text.text = "?";
        //    playerTwo = -1;
        //    PhotonNetwork.CurrentRoom.CustomProperties.Remove("playerTwoHero");
        //}
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
        mainLobbyCanvas.SetActive(true);
        roomLobbyCanvas.SetActive(false);
        PhotonNetwork.LeaveRoom(true);

        //if(PhotonNetwork.LocalPlayer.IsMasterClient)
        //    PhotonNetwork.CurrentRoom.CustomProperties.Remove("playerOneHero");
        //else
        //    PhotonNetwork.CurrentRoom.CustomProperties.Remove("playerTwoHero");

    }
}
