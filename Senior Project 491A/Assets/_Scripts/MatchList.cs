using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchList : MonoBehaviourPunCallbacks
{
	[SerializeField]
	private Text heroPlayer1, heroPlayer2, player1NickName, player2NickName;

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

        foreach (var playerData in PhotonNetwork.PlayerListOthers)
        {
            if (!playerData.IsMasterClient)
            {
                player2NickName.text = playerData.NickName;
            }
            else
            {
                player1NickName.text = playerData.NickName;
            }

        }
    }


	public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
	{
        if (!newPlayer.IsMasterClient)
        {
            player2NickName.text = newPlayer.NickName;
        }
        else
        {
            player1NickName.text = newPlayer.NickName;
        }
    }

	public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
	{
        if (!otherPlayer.IsMasterClient)
        {
            player2NickName.text = "Waiting";
        }
        else
        {
            player1NickName.text = "Waiting";
        }
    }

}
