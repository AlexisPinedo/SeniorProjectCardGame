using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public playerSwitch playerSwitcher;
    public BossTurnCardPlayer bossCardPlayer;
    public TurnManager turnManager;
    public TextUpdate textManger;
    public GameObject shop;
    public GameObject bossArea;

    private void Start()
    {
        turnManager.turnPlayer.hand.TurnStartDraw();
        bossArea.SetActive(false);
    }

    private void OnEnable()
    {
        UIHandler.EndTurnClicked += HandleGameState;
        UIHandler.StartClicked += HandleBattleState;
    }

    private void OnDisable()
    {
        UIHandler.EndTurnClicked -= HandleGameState;
        UIHandler.StartClicked += HandleBattleState;

    }

    private void HandleBattleState()
    {
        shop.SetActive(false);
        bossArea.SetActive(true);
    }

    private void HandleGameState()
    {

        StartCoroutine("EnemyTurn");


    }

    IEnumerator EnemyTurn()
    {
        Debug.Log("Starting next player turn");
        shop.SetActive(false);
        bossArea.SetActive(true);
        turnManager.turnPlayer.hand.SendHandToGraveyard();
        turnManager.turnPlayer.SetCurrency(0);
        turnManager.turnPlayer.SetPower(0);
        bossCardPlayer.PlayHandler();
        playerSwitcher.ShowHidePanel();
        yield return new WaitForSeconds(1f);
        turnManager.turnPlayer.hand.TurnStartDraw();
        textManger.ResetCurrency();
        textManger.ResetPower();
        shop.SetActive(true);
        bossArea.SetActive(false);
    }
}
