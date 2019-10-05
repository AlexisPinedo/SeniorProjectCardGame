using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UIHandler : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public GameObject startBattleButton, endTurnButton;

    public delegate void settingsButtonAction();
    public static event settingsButtonAction SettingsClicked;

    public delegate void StartBattleButtonAction();
    public static event StartBattleButtonAction StartClicked;

    public delegate void GraveyardButtonAction();
    public static event GraveyardButtonAction GraveyardClicked;

    public delegate void HeroPowerButtonAction();
    public static event HeroPowerButtonAction HeroPowerClicked;

    public delegate void EndTurnButtonAction();
    public static event EndTurnButtonAction EndTurnClicked;

    private void Awake()
    {
        Debug.Log("ui handle awake");
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("im master");
            startBattleButton.SetActive(true);
            endTurnButton.SetActive(true);
        }
        else
            Debug.Log("not master??");
    }

    public void SettingsButtonOnClick()
    {
        //Debug.Log("clicked");
        SettingsClicked?.Invoke();
    }

    public void StartBattleButtonOnClick()
    {
        StartClicked?.Invoke();
    }

    public void GraveyardButtonOnClick()
    {
        GraveyardClicked?.Invoke();
    }

    public void HeroPowerButtonOnClick()
    {
        HeroPowerClicked?.Invoke();
    }

    public void EndTurnButtonOnClick()
    {
        EndTurnClicked?.Invoke();
    }

}
