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
            SettingsClicked();
        }
    }

    public void StartBattleButtonOnClick()
    {
        if (StartClicked != null)
        {
            StartClicked();
        }
    }

    public void GraveyardButtonOnClick()
    {
        if (GraveyardClicked != null)
        {
            GraveyardClicked();
        }
    }

    public void test()
    {
        Debug.Log("clicked");
    }

    public void OnEnable()
    {
        ButtonInputEventHandler.SettingsClicked += test;
        ButtonInputEventHandler.StartClicked += test;
        ButtonInputEventHandler.GraveyardClicked += test;
    }

    public void OnDisable()
    {
        ButtonInputEventHandler.SettingsClicked -= test;
        ButtonInputEventHandler.StartClicked -= test;
        ButtonInputEventHandler.GraveyardClicked -= test;
    }
}