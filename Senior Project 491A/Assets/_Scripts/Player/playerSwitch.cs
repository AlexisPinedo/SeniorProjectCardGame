using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// TODO
/// </summary>
public class playerSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject p1HandSpacePanel;

    [SerializeField]
    private GameObject p2HandSpacePanel;

    public Player player1;

    public Player player2;

    void Awake()
    {
        p2HandSpacePanel.SetActive(false);
        TurnManager.turnPlayer = player1;
        //Debug.Log("playerSwitch awake");
    }

    /// <summary>
    /// TODO
    /// </summary>
    public void ShowHidePanel()
    {
        if (p1HandSpacePanel != null && p2HandSpacePanel != null)
        {
            // Switch to Player One
            if (p2HandSpacePanel.activeSelf)
            {
                p2HandSpacePanel.SetActive(false);
                p1HandSpacePanel.SetActive(true);
                //turnManager.SetPlayerOnesTurn();

                TurnManager.turnPlayer = player1;
                //Debug.Log("Player 1 Turn");
            }
            // Switch to Player Two
            else if (p1HandSpacePanel.activeSelf)
            {
                p1HandSpacePanel.SetActive(false);
                p2HandSpacePanel.SetActive(true);
                //turnManager.SetPlayerTwosTurn();
                TurnManager.turnPlayer = player2;
                //Debug.Log("Player 2 Turn");
            }
        }
    }
}
