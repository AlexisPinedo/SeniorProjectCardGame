using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effect/Minion Card Effect/Passive Enemy Buff")]
public class PassiveEnemyBuffEffect : PassiveEffect
{
    [SerializeField] private int buffAmount = 2;
    
    private void BuffMinionsOnField()
    {
        foreach (KeyValuePair<Vector2, CardDisplay> keyValuePair in FieldContainer.Instance.containerGrid.cardLocationReference)
        {
            Debug.Log("Enemy buffed");
            EnemyCardDisplay enemyCardDisplay = (EnemyCardDisplay)keyValuePair.Value;
            EnemyCard enemyCard = enemyCardDisplay.card;

            enemyCard.HealthValue += buffAmount;
        }
    }

    private void onDisable()
    {
        foreach (KeyValuePair<Vector2, CardDisplay> keyValuePair in FieldContainer.Instance.containerGrid.cardLocationReference)
        {
            EnemyCardDisplay enemyCardDisplay = (EnemyCardDisplay)keyValuePair.Value;
            EnemyCard enemyCard = enemyCardDisplay.card;

            enemyCard.HealthValue -= buffAmount;
        }
    }

    private void BuffMinionPlayed(EnemyCard cardPlayed)
    {
        cardPlayed.HealthValue += buffAmount;
    }
    
    public override void LaunchCardEffect()
    {
        BuffMinionsOnField();
    }
}
