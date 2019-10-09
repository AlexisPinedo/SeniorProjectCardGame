using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UIViewHandler : MonoBehaviourPunCallbacks
{
    
    [SerializeField]
    public GameObject canvas, startBattleButton, endTurnButton;
    
    private void Awake()
    {
        if (photonView.IsMine)
        {
            startBattleButton.SetActive(true);
            endTurnButton.SetActive(true);
        }
        else
        {
            startBattleButton.SetActive(true);
            endTurnButton.SetActive(true);
        }


        if (!PhotonNetworkManager.IsOffline)
        {
            if (!photonView.IsMine)
            {
                startBattleButton.SetActive(false);
                endTurnButton.SetActive(false);
            }
        }
    }
}
