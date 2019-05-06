using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventHanlder : MonoBehaviour
{
    public delegate void settingsButtonAction();
    public static event settingsButtonAction SettingsClicked;

    public delegate void StartBattleButtonAction();
    public static event StartBattleButtonAction StartClicked;

    public delegate void SwitchPlayerButtonAction();
    public static event SwitchPlayerButtonAction SwitchClicked;

    public delegate void GraveyardButtonAction();
    public static event GraveyardButtonAction GraveyardClicked;

    public delegate void HeroPowerButtonAction();
    public static event HeroPowerButtonAction HeroPowerClicked;

    public delegate void EndTurnButtonAction();
    public static event EndTurnButtonAction EndTurnClicked;



    public void SettingsButtonOnClick()
    {
        SettingsClicked?.Invoke();
    }

    public void StartBattleButtonOnClick()
    {
        StartClicked?.Invoke();
    }

    public void SwitchPlayerButtonOnClick()
    {
        SwitchClicked?.Invoke();
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
