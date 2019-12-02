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
        playerPower.text = TurnPlayerManager.Instance.TurnPlayer.Power.ToString();

        if (PhotonNetwork.OfflineMode)
        {
            return;
        }

        if (photonView.IsMine)
        {
            photonView.RPC("RPCUpdatePower", RpcTarget.All, TurnPlayerManager.Instance.TurnPlayer.Power);
        }

    }

    public void UpdateCurrency()
    {
        playerCurrency.text = TurnPlayerManager.Instance.TurnPlayer.Currency.ToString();
        
        if (PhotonNetwork.OfflineMode)
        {
            return;
        }
        
        if (photonView.IsMine)
        {
            photonView.RPC("RPCUpdateCurrency", RpcTarget.All, TurnPlayerManager.Instance.TurnPlayer.Currency);
        }
    }

    [PunRPC]
    private void RPCUpdatePower(int power)
    {
        playerPower.text = power.ToString();
    }

    [PunRPC]
    private void RPCUpdateCurrency(int currency)
    {
        playerCurrency.text = currency.ToString();
    }
}
