using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonInputEventHandler : MonoBehaviour
{
    public delegate void settingsButtonAction();
    public static event settingsButtonAction SettingsClicked;

    public delegate void StartBattleButtonAction();
    public static event StartBattleButtonAction StartClicked;

    public delegate void GraveyardButtonAction();
    public static event GraveyardButtonAction GraveyardClicked;

    public void SettingsButtonClick()
    {
        if(SettingsClicked != null)
        {
            Debug.Log("clicked");
            SettingsClicked();
        }
    }

    public void StartBattleButtonClick()
    {
        if (StartClicked != null)
        {
            Debug.Log("clicked");
            StartClicked();
        }
    }

    public void GraveyardButtonClick()
    {
        if (GraveyardClicked != null)
        {
            Debug.Log("clicked");
            GraveyardClicked();
        }
    }
}