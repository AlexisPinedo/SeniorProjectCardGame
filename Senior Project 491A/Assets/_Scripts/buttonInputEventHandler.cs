using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInputEventHandler : MonoBehaviour
{
    public delegate void settingsButtonAction();
    public static event settingsButtonAction SettingsClicked;

    public delegate void StartBattleButtonAction();
    public static event StartBattleButtonAction StartClicked;

    public delegate void GraveyardButtonAction();
    public static event GraveyardButtonAction GraveyardClicked;

    public void SettingsButtonOnClick()
    {
        //Debug.Log("clicked");
        if (SettingsClicked != null)
        {
            Debug.Log("clicked");
            SettingsClicked();
        }
    }

    public void StartBattleButtonOnClick()
    {
        if (StartClicked != null)
        {
            Debug.Log("clicked");
            StartClicked();
        }
    }

    public void GraveyardButtonOnClick()
    {
        if (GraveyardClicked != null)
        {
            Debug.Log("clicked");
            GraveyardClicked();
        }
    }

    public void OnEnable()
    {
        ButtonInputEventHandler.SettingsClicked += SettingsButtonOnClick;
        ButtonInputEventHandler.StartClicked += StartBattleButtonOnClick;
        ButtonInputEventHandler.GraveyardClicked += GraveyardButtonOnClick;
    }

    public void OnDisable()
    {
        ButtonInputEventHandler.SettingsClicked -= SettingsButtonOnClick;
        ButtonInputEventHandler.StartClicked -= StartBattleButtonOnClick;
        ButtonInputEventHandler.GraveyardClicked -= GraveyardButtonOnClick;
    }
}