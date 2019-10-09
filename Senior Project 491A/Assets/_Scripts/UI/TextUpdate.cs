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

        if (!photonView.IsMine)
        {
            player1NickName.text = "Waiting for turn...";
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
        if (photonView.IsMine)
        {
            playerPower.text = "Power: " + TurnManager.Instance.turnPlayer.Power;
            photonView.RPC("RPCUpdatePower", RpcTarget.All, TurnManager.Instance.turnPlayer.Power);
        }

    }

    public void UpdateCurrency()
    {
        if (photonView.IsMine)
        {
            playerCurrency.text = "Currency: " + TurnManager.Instance.turnPlayer.Currency;
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

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(TurnManager.Instance.turnPlayer.Power);
    //        stream.SendNext(TurnManager.Instance.turnPlayer.Currency);
    //    }
    //    if (stream.IsReading)
    //    {
    //        playerPower.text = (string)stream.ReceiveNext();
    //        playerCurrency.text = (string)stream.ReceiveNext();
    //    }
    //}

}
