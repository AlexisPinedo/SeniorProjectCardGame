using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject p1HandSpacePanel;
    
    [SerializeField]
    private GameObject p2HandSpacePanel;

    public TurnManager turnPlayer;

    public Player player1;

    public Player player2;

    //[SerializeField]
    //private TurnManager turnManager;

    public void SwapPlayers()
    {
        if (p1HandSpacePanel != null && p2HandSpacePanel != null)
        {
            // Switch to Player Two
            if (p1HandSpacePanel.activeSelf)
            {
                p1HandSpacePanel.SetActive(false);
                p2HandSpacePanel.SetActive(true);
                turnPlayer.turnPlayer = player2;
                turnPlayer.turnPlayer.hand.TurnStartDraw();
            }
            // Switch to Player One
            else if (p2HandSpacePanel.activeSelf)
            {
                p2HandSpacePanel.SetActive(false);
                p1HandSpacePanel.SetActive(true);
                turnPlayer.turnPlayer = player1;
                turnPlayer.turnPlayer.hand.TurnStartDraw();
            }
        }
    }
}
