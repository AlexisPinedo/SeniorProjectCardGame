using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class GameplayManager : MonoBehaviourPun
{
    public TextUpdate textManger;
    public GameObject shop;
    public GameObject bossArea;
}




/// <summary>
/// TODO
/// </summary>
//public class GameplayManager : MonoBehaviourPun
//{
    //public playerSwitch playerSwitcher;
    //public BossTurnCardPlayer bossCardPlayer;
    //public TextUpdate textManger;
    //public GameObject shop;
    //public GameObject bossArea;

    //private Player turnPlayer;
    //private Hand tpHand;

    //private void Awake()
    //{
    //    //PhotonNetwork.ConnectUsingSettings();
    //}

    //private void Start()
    //{
    //    Debug.Log("Room: " + PhotonNetwork.CurrentRoom.Name);
    //    Debug.Log("Players in room: " + PhotonNetwork.CurrentRoom.PlayerCount);
    //    turnPlayer = TurnManager.turnPlayer;
    //    tpHand = turnPlayer.hand;

    //    tpHand.TurnStartDraw();
    //    bossArea.SetActive(false);
    //}

    //private void OnEnable()
    //{
    //    UIHandler.EndTurnClicked += HandleGameState;
    //    UIHandler.StartClicked += HandleBattleState;
    //}

    //private void OnDisable()
    //{
    //    UIHandler.EndTurnClicked -= HandleGameState;
    //    UIHandler.StartClicked += HandleBattleState;

    //}

    ///// <summary>
    ///// TODO
    ///// </summary>
    //private void HandleBattleState()
    //{
    //    shop.SetActive(false);
    //    bossArea.SetActive(true);
    //}

    ///// <summary>
    ///// TODO
    ///// </summary>
    //private void HandleGameState()
    //{
    //    StartCoroutine("EnemyTurn");
    //}

    ///// <summary>
    ///// TODO
    ///// </summary>
    ///// <returns></returns>
    //IEnumerator EnemyTurn()
    //{
    //    shop.SetActive(false);
    //    bossArea.SetActive(true);
        
    //    // Handle current Player's stats
    //    TurnManager.turnPlayer.hand.SendHandToGraveyard();
    //    TurnManager.turnPlayer.SetCurrency(0);
    //    TurnManager.turnPlayer.SetPower(0);

    //    // Handle Boss' turn
    //    bossCardPlayer.PlayHandler();

    //    // Switch players
    //    Debug.Log("Starting next player turn");
    //    playerSwitcher.ShowHidePanel();
    //    yield return new WaitForSeconds(1f);
    
    //    // Change players
    //    TurnManager.turnPlayer.hand.TurnStartDraw();
    //    textManger.ResetCurrency();
    //    textManger.ResetPower();
    //    shop.SetActive(true);
    //    bossArea.SetActive(false);
    //}
//}
