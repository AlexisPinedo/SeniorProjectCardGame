using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInputManager : MonoBehaviour
{
    [SerializeField]
    private List<Button> buttonList = new List<Button>();

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

        MyTurn();
    }

    private void OnEnable()
    {
        UIHandler.EndTurnClicked += MyTurn;
    }

    private void OnDisable()
    {
        UIHandler.EndTurnClicked -= MyTurn;
    }


    public void MyTurn()
    {
        //Debug.Log("Switching button control to: " + TurnManager.currentPhotonPlayer.NickName);
        foreach (Button abutton in buttonList)
        {
            if(abutton.name == "Start Battle Button" || abutton.name == "End Turn Button")
            {
                if (TurnManager.currentPhotonPlayer.IsLocal)
                    abutton.gameObject.SetActive(true);
                else
                    abutton.gameObject.SetActive(false);
            }
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
