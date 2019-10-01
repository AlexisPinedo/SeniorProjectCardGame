using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TextUpdate : MonoBehaviourPunCallbacks
{
    public Text playerPower;
    public Text playerCurrency;
    public Text player1NickName;
    public Text player2NickName;

    private static TextUpdate _instance;

    public static TextUpdate Instance
    {
        get => _instance;
    }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    
    private void Start()
    {
        UpdatePower();
        UpdateCurrency();

        player1NickName.text = "Current turn: " + PhotonNetwork.MasterClient.NickName;
        foreach (KeyValuePair<int, Photon.Realtime.Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            if (playerInfo.Value.NickName != PhotonNetwork.MasterClient.NickName)
            {
                player2NickName.text = "Waiting for turn: " + playerInfo.Value.NickName;
            }
        }
    }

    private void OnEnable()
    {
        Player.PowerUpdated += UpdatePower;
        Player.CurrencyUpdated += UpdateCurrency;
    }

    private void OnDisable()
    {
        Player.PowerUpdated -= UpdatePower;
        Player.CurrencyUpdated -= UpdateCurrency;
    }

    public void UpdatePower()
    {
        playerPower.text = "Power: " + TurnManager.Instance.turnPlayer.Power;
    }

    public void UpdateCurrency()
    {
        playerCurrency.text = "Currency: " + TurnManager.Instance.turnPlayer.Currency;
    }
}
