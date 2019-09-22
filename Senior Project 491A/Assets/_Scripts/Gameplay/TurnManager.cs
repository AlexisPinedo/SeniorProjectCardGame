using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // Player References
    public Player turnPlayer;

    [SerializeField]
    private GameObject p1HandSpacePanel;

    [SerializeField]
    private GameObject p2HandSpacePanel;

    public delegate void _PlayerSwitched();

    public static event _PlayerSwitched PlayerSwitched;

    public Player player1;

    public Player player2;

    private static TurnManager _instance;

    public static TurnManager Instance
    {
        get { return _instance; }
    }

    private void OnEnable()
    {
        UIHandler.EndTurnClicked += ShowHidePanel;
    }

    private void OnDisable()
    {
        UIHandler.EndTurnClicked -= ShowHidePanel;
    }


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        p2HandSpacePanel.SetActive(false);
        turnPlayer = player1;
        
    }

    private void Start()
    {
        PlayerSwitched?.Invoke();
    }

    public void ShowHidePanel()
    {
        if (p1HandSpacePanel != null && p2HandSpacePanel != null)
        {
            turnPlayer.Currency = 0;
            turnPlayer.Power = 0;
            
            // Switch to Player Two
            if (p1HandSpacePanel.activeSelf)
            {
                p1HandSpacePanel.SetActive(false);
                p2HandSpacePanel.SetActive(true);
                //turnManager.SetPlayerTwosTurn();
                turnPlayer = player2;
            }
            // Switch to Player One
            else if (p2HandSpacePanel.activeSelf)
            {
                p2HandSpacePanel.SetActive(false);
                p1HandSpacePanel.SetActive(true);
                //turnManager.SetPlayerOnesTurn();

                turnPlayer = player1;
            }
            
            PlayerSwitched?.Invoke();

            TextUpdate.Instance.UpdateCurrency();
            TextUpdate.Instance.UpdatePower();
        }
    }

}
