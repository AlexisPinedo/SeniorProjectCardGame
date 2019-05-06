using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public TurnManager turnManager;
    public BossTurnCardPlayer bossManager;
    public playerSwitch playerSwitcher;
    void Start()
    {
        turnManager.turnPlayer.hand.TurnStartDraw();
    }

    private void OnEnable()
    {
        UIEventHanlder.EndTurnClicked += HandleEndTurnPhase;
    }

    private void OnDisable()
    {
        UIEventHanlder.EndTurnClicked -= HandleEndTurnPhase;
    }

    private void HandleEndTurnPhase()
    {
        Debug.Log("Ending Turn");
        //Discard hand
        turnManager.turnPlayer.hand.SendHandToGraveyard();

        //Enemy plays a card
        bossManager.HandlePlayEnemyCard();

        //Swap to the next Player and draw hands
        playerSwitcher.SwapPlayers();

    }

}
