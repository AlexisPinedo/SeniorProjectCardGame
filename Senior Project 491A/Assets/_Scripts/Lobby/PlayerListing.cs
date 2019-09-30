using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    public Photon.Realtime.Player PlayerData { get; private set; }

    public void SetPlayerInfo(Photon.Realtime.Player player)
    {
        PlayerData = player;
        _text.text = player.NickName;
    }

}
