using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInputManager : MonoBehaviour
{
    [SerializeField]
    private List<Button> buttonList = new List<Button>();

    [SerializeField]
    private GameObject startBattleButton, endTurnButton;

    private static ButtonInputManager _instance;

    public static ButtonInputManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance == null && _instance != this)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnEnable()
    {
        TurnPhaseManager.PlayerTurnStarted += CurrentTurnButtonSwitch;
    }

    private void OnDisable()
    {
        TurnPhaseManager.PlayerTurnStarted -= CurrentTurnButtonSwitch;
    }

    private void Start()
    {
        CurrentTurnButtonSwitch();
    }

    public void CurrentTurnButtonSwitch()
    {
        if (PhotonNetwork.OfflineMode)
            return;

        if (!NetworkOwnershipTransferManger.currentPhotonPlayer.IsLocal)
        {
            startBattleButton.SetActive(false);
            endTurnButton.SetActive(false);
        }
        else
        {
            startBattleButton.SetActive(true);
            endTurnButton.SetActive(true);
        }
    }


    public void DisableButtonsInList()
    {
        foreach (Button aButton in buttonList)
        {
            aButton.enabled = false;
        }
    }

    public void EnableButtonsInList()
    {
        foreach (Button aButton in buttonList)
        {
            aButton.enabled = true;
        }
    }
}
