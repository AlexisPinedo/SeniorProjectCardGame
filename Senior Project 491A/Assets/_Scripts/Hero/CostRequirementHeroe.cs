using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CostRequirementHeroe : Hero
{
    [SerializeField] private int cardEffectRequirementCount = 3;
    
    protected virtual void OnEnable()
    {
        History.CardAddedToHistory += ValidateEffectRequirement;
    }

    protected void OnDisable()
    {
        History.CardAddedToHistory -= ValidateEffectRequirement;
    }

    protected virtual void  ValidateEffectRequirement()
    {
        if(TurnPlayerHeroManager.Instance.ActiveTurnHero != this)
            return;
        
        List<PlayerCard> cardHistory = History.Instance.PlayerCardHistory;

        if (cardHistory.Count < cardEffectRequirementCount)
        {
            Debug.Log("Card history too small to evaluate");
            return;
        }

        int lastCardPlayedReference = cardHistory.Count - 1;
        
        PlayerCard lastCardPlayed = cardHistory[lastCardPlayedReference];

        if (lastCardPlayed.CardType == CardType.CardTypes.None)
        {
            Debug.Log("Card has no type nothing to evaluate");
            return;
        }
        

        for (int i = 1; i < cardEffectRequirementCount; i++)
        {
            if (cardHistory[lastCardPlayedReference - i].CardType != lastCardPlayed.CardType)
            {
                Debug.Log("last 3 cards were not the same type");
                return;
            }
        }

        TriggerHeroPowerEffect();
    }

}
