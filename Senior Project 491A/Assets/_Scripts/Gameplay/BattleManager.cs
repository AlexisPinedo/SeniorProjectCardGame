using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    private bool inBattleState = false;

    [SerializeField]
    private Grid EnemyGrid;

    [SerializeField]
    private TextMeshProUGUI PlayZoneText;
    
    private void OnEnable()
    {
        UIHandler.StartClicked += StartBattleState;
        UIHandler.EndTurnClicked += EndBattleState;
        MinionCardHolder.MinionCardClicked += CalculateBattleOutcome;
        BossCardHolder.BossCardClicked += CalculateBossBattleOutcome;
    }

    private void OnDisable()
    {
        UIHandler.StartClicked -= StartBattleState;
        UIHandler.EndTurnClicked -= EndBattleState;
        MinionCardHolder.MinionCardClicked -= CalculateBattleOutcome;
        BossCardHolder.BossCardClicked -= CalculateBossBattleOutcome;
    }

    private void CalculateBattleOutcome(MinionCardHolder cardClicked)
    {
        if (TurnManager.Instance.turnPlayer.Power >= cardClicked.card.HealthValue)
        {
            TurnManager.Instance.turnPlayer.Power -= cardClicked.card.HealthValue;
            Destroy(cardClicked.gameObject);
        }
    }

    private void CalculateBossBattleOutcome(BossCardHolder cardClicked)
    {
        foreach (var keyValuePair in EnemyGrid.cardLocationReference)
        {
            if (keyValuePair.Value != null)
            {
                Debug.Log("You need to kill all minions to attack the boss");
                return;
            }
        }
        
        if (TurnManager.Instance.turnPlayer.Power >= cardClicked.card.HealthValue)
        {
            TurnManager.Instance.turnPlayer.Power -= cardClicked.card.HealthValue;
            Destroy(cardClicked.gameObject);
            Debug.Log("Boss Has been defeated!");
        }
    }

    private void StartBattleState()
    {
        if (inBattleState == true)
            return;
        
        StartCoroutine(BattleState());
    }

    private void EndBattleState()
    {
        inBattleState = false;
    }

    IEnumerator BattleState()
    {
        inBattleState = true;
        PurchaseHandler.Instance.gameObject.transform.position += new Vector3(0f, 20f, 0f);
        PlayZone.Instance.gameObject.SetActive(false);
        PlayZoneText.enabled = false;
        
        foreach (var keyValuePair in EnemyGrid.cardLocationReference)
        {
            if(keyValuePair.Value != null)
                keyValuePair.Value.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        
        while (inBattleState)
        {
            yield return null;
        }
        
        foreach (var keyValuePair in EnemyGrid.cardLocationReference)
        {
            if(keyValuePair.Value != null)
                keyValuePair.Value.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        PlayZoneText.enabled = true;
        PlayZone.Instance.gameObject.SetActive(true);
        PurchaseHandler.Instance.gameObject.SetActive(true);
        PurchaseHandler.Instance.gameObject.transform.position -= new Vector3(0f, 20f, 0f);

    }
}
