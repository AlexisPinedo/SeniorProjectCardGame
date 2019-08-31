using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public BossTurnCardPlayer bossCardPlayer;
    public TextUpdate textManger;
    public GameObject shop;
    public GameObject bossArea;

    private void Start()
    {
        TurnManager.Instance.turnPlayer.hand.TurnStartDraw();
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
        //Debug.Log("Starting next player turn");
        shop.SetActive(false);
        bossArea.SetActive(true);
        TurnManager.Instance.turnPlayer.hand.SendHandToGraveyard();
        TurnManager.Instance.turnPlayer.SetCurrency(0);
        TurnManager.Instance.turnPlayer.SetPower(0);
        bossCardPlayer.PlayHandler();
        TurnManager.Instance.ShowHidePanel();
        yield return new WaitForSeconds(1f);
        TurnManager.Instance.turnPlayer.hand.TurnStartDraw();
        textManger.ResetCurrency();
        textManger.ResetPower();
        shop.SetActive(true);
        bossArea.SetActive(false);
    }
}
