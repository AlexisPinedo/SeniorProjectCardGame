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

        if (PhotonNetwork.OfflineMode)
        {
            return;
        }

        if (photonView.IsMine)
        {
            photonView.RPC("RPCUpdatePower", RpcTarget.All, TurnManager.Instance.turnPlayer.Power);
        }

    }

    public void UpdateCurrency()
    {
        playerCurrency.text = "Currency: " + TurnManager.Instance.turnPlayer.Currency;
        
        if (PhotonNetwork.OfflineMode)
        {
            return;
        }
        
        if (photonView.IsMine)
        {
            photonView.RPC("RPCUpdateCurrency", RpcTarget.All, TurnManager.Instance.turnPlayer.Currency);
        }
    }

    [PunRPC]
    private void RPCUpdatePower(int power)
    {
        playerPower.text = "Power: " + power;
    }

    [PunRPC]
    private void RPCUpdateCurrency(int currency)
    {
        playerCurrency.text = "Currency " + currency;
    }
}
