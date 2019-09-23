using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectEvaluationManager : MonoBehaviour
{
    public delegate void _CardCostMet();

    public static event _CardCostMet CardCostMet;
    private void OnEnable()
    {
        PlayZone.CardPlayed += EvaluateCardEffect;
    }

    private void OnDisable()
    {
        PlayZone.CardPlayed -= EvaluateCardEffect;
    }

    private void EvaluateCardEffect(PlayerCard cardToEvaluate)
    {
        int costCount = cardToEvaluate.cardEffectRequirments.Count;

        if (costCount == 0)
        {
            Debug.Log("No cost needed");
            return;
        }

        if (History.Instance.PlayerCardHistory.Count < costCount)
        {
            Debug.Log("History not big enough requirement not met");
            return;
        } 
        
        for (int i = 0; i < costCount; i++)
        {

            PlayerCard cardInHistory =
                History.Instance.PlayerCardHistory[History.Instance.PlayerCardHistory.Count - 1 - i];
            
            CardType.CardTypes typeToCompareAgainst = cardToEvaluate.cardEffectRequirments[cardToEvaluate.cardEffectRequirments.Count - 1 - i];

            if (cardInHistory.CardType != typeToCompareAgainst)
            {
                if (cardInHistory.CardType == CardType.CardTypes.All)
                {
                    Debug.Log("the card in history was type any so it is equal ");
                }
                else if (typeToCompareAgainst == CardType.CardTypes.All)
                {
                    Debug.Log("The card requirement was any card to be played. The condition is met.");
                }
                else
                {
                    Debug.Log("Requirement not met history is big enough");
                    return;
                }
            }
        }
        
        Debug.Log("Cost has been met");
    }
}
