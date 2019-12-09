using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;
/// <summary>
/// This class will handle the logic for the battle state
/// </summary>
public class BattleManager : MonoBehaviour
{
    private bool inBattleState = false;

    [SerializeField]
    private CardGrid enemyCardGrid;

    [SerializeField]
    private TextMeshProUGUI PlayZoneText;

    public static event Action EnemyBattlePhaseStarted; 
    public static event Action EnemyBattlePhaseEnded;

    private void OnEnable()
    {
        TurnPhaseManager.BattlePhaseStarted += StartBattlePhase;
        TurnPhaseManager.BattlePhaseEnded += EndBattlePhase;
        MinionCardDisplay.MinionCardClicked += CalculateBattleOutcome;
        BossCardDisplay.BossCardClicked += CalculateBossBattleOutcome;
    }

    private void OnDisable()
    {
        TurnPhaseManager.BattlePhaseStarted -= StartBattlePhase;
        TurnPhaseManager.BattlePhaseEnded -= EndBattlePhase;
        MinionCardDisplay.MinionCardClicked -= CalculateBattleOutcome;
        BossCardDisplay.BossCardClicked -= CalculateBossBattleOutcome;
    }

    //This method subs to the MinionCardClicked events taking in the minion card that was clicked
    private void CalculateBattleOutcome(MinionCardDisplay cardClicked)
    {
        //if the turn player has equal or more power than the minions health value
        //we will decrement the power by the health then destroy the minion
        if (TurnPlayerManager.Instance.TurnPlayer.Power >= cardClicked.card.HealthValue)
        {
            TurnPlayerManager.Instance.TurnPlayer.Power -= cardClicked.card.HealthValue;
            Destroy(cardClicked.gameObject);
        }
    }

    //This method subs to the BossCardClicked events taking in the boss card that was clicked
    private void CalculateBossBattleOutcome(BossCardDisplay cardClicked)
    {
        //We need to check if there are any other minions on the field
        //If there is a minion we cannot attack the boss
        foreach (var keyValuePair in enemyCardGrid.cardLocationReference)
        {
            if (keyValuePair.Value != null)
            {
                Debug.Log("You need to kill all minions to attack the boss");
                return;
            }
        }
        
        // There are no other minions on the field so now we can check to see if we can kill the boss
        //if the turn player has equal or more power than the boss health value
        //we will decrement the power by the health then destroy the boss
        if (TurnPlayerManager.Instance.TurnPlayer.Power >= cardClicked.card.HealthValue)
        {
            TurnPlayerManager.Instance.TurnPlayer.Power -= cardClicked.card.HealthValue;
            Destroy(cardClicked.gameObject);
            EndGameHandler.TriggerEndGame();
            return;
            Debug.Log("Boss Has been defeated!");
        }
    }

    //This method subs to the start battle button.
    private void StartBattlePhase()
    {
        EnemyBattlePhaseStarted?.Invoke();
        //we want to check if we are in the battle state. If we are we do nothing. 
        if (inBattleState == true)
            return;
        //This will begin the BattlePhase coroutine
        StartCoroutine(BattlePhase());
    }

    //THis method is used to end the battle phase
    private void EndBattlePhase()
    {
        inBattleState = false;
    }

    IEnumerator BattlePhase()
    {
        //This can be used to validate battle state anywhere
        inBattleState = true;
        
        //This will move the shop up showing the grid
        ShopDisplayManager.Instance.MoveShopUp();
        
        //We deactivate the playzone and it's text
        PlayZone.Instance.gameObject.SetActive(false);
        
        //We then want to enable all the enemy cards on the field
        foreach (var keyValuePair in enemyCardGrid.cardLocationReference)
        {
            if(keyValuePair.Value != null)
                keyValuePair.Value.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        
        //This will keep the player in the battle state until inBattleState goes false
        while (inBattleState)
        {
            //This will keep the battle state stuck processing this while loop each frame
            yield return null;
        }
        
        
        //We are now exiting battle state we need to deactivate the colliders
        foreach (var keyValuePair in enemyCardGrid.cardLocationReference)
        {
            if(keyValuePair.Value != null)
                keyValuePair.Value.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        //set the components to true again
        PlayZone.Instance.gameObject.SetActive(true);

        ShopDisplayManager.Instance.MoveShopDown();
        
        //Debug.Log("Ending battle state");
        EnemyBattlePhaseEnded?.Invoke();
    }
}
