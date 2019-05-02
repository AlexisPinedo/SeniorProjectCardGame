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

    public delegate void HeroPowerButtonAction();
    public static event HeroPowerButtonAction HeroPowerClicked;

    public delegate void EndTurnButtonAction();
    public static event EndTurnButtonAction EndTurnClicked;

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

    public void HeroPowerButtonOnClick()
    {
        if (HeroPowerClicked != null)
        {
            HeroPowerClicked();
        }
    }

    public void EndTurnButtonOnClick()
    {
        if (HeroPowerClicked != null)
        {
            EndTurnClicked();
        }
    }

    public void test()
    {
        Debug.Log("clicked");
    }

    public void OnEnable()
    {
        SettingsClicked += test;
        StartClicked += test;
        GraveyardClicked += test;
        HeroPowerClicked += test;
        EndTurnClicked += test;
    }

    public void OnDisable()
    {
        SettingsClicked -= test;
        StartClicked -= test;
        GraveyardClicked -= test;
        HeroPowerClicked -= test;
        EndTurnClicked -= test;
    }
}