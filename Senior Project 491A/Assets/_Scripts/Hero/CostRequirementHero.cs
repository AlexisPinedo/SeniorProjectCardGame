using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CostRequirementHero : Hero
{
    [SerializeField] private int cardEffectRequirementCount = 3;
    [SerializeField] private int cardsSinceLastEffectTrigger = 0;

    [SerializeField] protected List<PlayerCard> cardsPlayedForEffect;

    protected CardType.CardTypes lastCardPlayedType;
    
    protected virtual void OnEnable()
    {
        //History.CardAddedToHistory += ValidateEffectRequirement;
        PlayZone.CardPlayed += OtherValidateEffectRequirement;
        TurnManager.PlayerSwitched += ClearEffectBuffer;
    }

    protected void OnDisable()
    {
        //History.CardAddedToHistory -= ValidateEffectRequirement;
        PlayZone.CardPlayed -= OtherValidateEffectRequirement;
        TurnManager.PlayerSwitched -= ClearEffectBuffer;
    }

    private void ClearEffectBuffer()
    {
        cardsPlayedForEffect.Clear();
    }

    protected virtual void OtherValidateEffectRequirement(PlayerCard cardPlayed)
    {
        if(TurnPlayerHeroManager.Instance.ActiveTurnHero != this)
            return;
        
        if (cardPlayed.CardType == CardType.CardTypes.None)
        {
            //Debug.Log("Card has no type nothing to evaluate");
            return;
        }
        
        cardsPlayedForEffect.Add(cardPlayed);
        lastCardPlayedType = cardPlayed.CardType;
        
        //Debug.Log("Card added to hero effect check");

        if (cardsPlayedForEffect.Count < cardEffectRequirementCount)
        {
            //Debug.Log("Card history too small to evaluate");
            return;
        }
        
        int lastCardPlayedReference = cardsPlayedForEffect.Count - 1;

        
        for (int i = 1; i < cardEffectRequirementCount; i++)
        {
            if (cardsPlayedForEffect[lastCardPlayedReference - i].CardType != cardPlayed.CardType)
            {
                //Debug.Log("last 3 cards were not the same type");
                return;
            }
        }

        TriggerHeroPowerEffect();
        cardsPlayedForEffect.Clear();
        
    }

    
    //Running a more optimized method above
//    protected virtual void ValidateEffectRequirement()
//    {
//        if(TurnPlayerHeroManager.Instance.ActiveTurnHero != this)
//            return;
//        
//        List<PlayerCard> cardHistory = History.Instance.PlayerCardHistory;
//
//        if (cardHistory.Count < cardEffectRequirementCount)
//        {
//            Debug.Log("Card history too small to evaluate");
//            return;
//        }
//
//        int lastCardPlayedReference = cardHistory.Count - 1;
//        
//        PlayerCard lastCardPlayed = cardHistory[lastCardPlayedReference];
//
//        if (lastCardPlayed.CardType == CardType.CardTypes.None)
//        {
//            Debug.Log("Card has no type nothing to evaluate");
//            return;
//        }
//
//        if (cardsSinceLastEffectTrigger != cardEffectRequirementCount)
//        {
//            Debug.Log("The last three cards are correct but the effect just happened need to play " +
//                      + cardsSinceLastEffectTrigger +" more card(s)");
//
//            return;
//        }
//        
//
//        for (int i = 1; i < cardEffectRequirementCount; i++)
//        {
//            if (cardHistory[lastCardPlayedReference - i].CardType != lastCardPlayed.CardType)
//            {
//                Debug.Log("last 3 cards were not the same type");
//                return;
//            }
//        }
//
//        TriggerHeroPowerEffect();
//    }

}
