using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This manager component will check if a card effect can be triggered. 
/// </summary>
public class CardEffectEvaluationManager : MonoBehaviour
{
    // We create a delegate-event so anything that cared about the requirements being met, can be notified
    public delegate void _CardCostMet();
    public static event _CardCostMet CardCostMet;
    
    /// <summary>
    /// Here we subscribe to the PlayZone.CardPlayed event.
    /// This event will trigger when a card is dropped into the play zone
    /// We will run EvaluateCardEffect the event is triggered
    /// </summary>
    private void OnEnable()
    {
        PlayZone.CardPlayed += EvaluateCardEffect;
    }

    /// <summary>
    /// Common practice is to unsub from events to avoid memory leak. 
    /// </summary>
    private void OnDisable()
    {
        PlayZone.CardPlayed -= EvaluateCardEffect;
    }

    /// <summary>
    /// This method will take the card dropped into the play zone and begin checking if the requirements have been met
    /// if they have been met it will trigger that cards card effect
    /// </summary>
    /// <param name="cardToEvaluate"></param>
    private void EvaluateCardEffect(PlayerCard cardToEvaluate)
    {
        //If there is no card effect we can exit
        if (cardToEvaluate.CardEffect == null)
            return;
        
        //if a card has a requirement to trigger an effect, requirements will be stored in a list
        //This will get the count of list to determine how many cards to evaluate
        int costCount = cardToEvaluate.cardEffectRequirments.Count;

        //If there is no cost then trigger the effect if it has one. Then exit the method. 
        if (costCount == 0)
        {
            //Debug.Log("No cost needed");
            if(cardToEvaluate.CardEffect != null)
                cardToEvaluate.CardEffect.LaunchCardEffect();
            return;
        }

        //PlayerCardHistory will reset when it is a new players turn.
        //We can use this as a reference to see what cards have been played
        //The end of the list will have the most recently played cards 
        //Here we check if the list of the cards played this turn and see if they have played enough cards
        //if so then exit the method.
        if (History.Instance.PlayerCardHistory.Count < costCount)
        {
            //Debug.Log("History not big enough requirement not met");
            return;
        } 
        
        //Reaching this means there is enough cards in the list and they have a cost to evaluate. 
        //We must check each card played from this point to see if the effect is valid. reaching the end of the for loop 
        //means we could not find anything wrong with cards played. 
        for (int i = 0; i < costCount; i++)
        {
            //we obtain the last card played and store its data 
            PlayerCard cardInHistory =
                History.Instance.PlayerCardHistory[History.Instance.PlayerCardHistory.Count - 1 - i];
            
            //Each card type has a card type: Magic, warrior, etc. These are values assigned from an enumerator
            //The cardToEvaluate.cardEffectRequirments is a list of these enumerator values
            //we will begin checking if the play order requirement matches up with the cards played
            CardTypes typeToCompareAgainst = cardToEvaluate.cardEffectRequirments[cardToEvaluate.cardEffectRequirments.Count - 1 - i];

            //If the card type are not equal we have to check for a few more conditions
            if (cardInHistory.CardType != typeToCompareAgainst)
            {
                //CardType.CardTypes.All means that the card is all types so the two cards are equal  
                if (cardInHistory.CardType == CardTypes.All)
                {
                    //Debug.Log("the card in history was type any so it is equal ");
                }
                //Card effect requirement could also just want a card played in this case no matter what is played 
                //should trigger the card effect. 
                else if (typeToCompareAgainst == CardTypes.All)
                {
                    //Debug.Log("The card requirement was any card to be played. The condition is met.");
                }
                //If these 2 conditions have not been met then the cards are not equal and we exit the loop
                else
                {
                    //Debug.Log("Requirement not met history is big enough");
                    return;
                }
            }
        }
        
        //We can assume the effect is valid at this point and can invoke it. 
        //Debug.Log("Cost has been met");
        cardToEvaluate.CardEffect.LaunchCardEffect();
    }
}
