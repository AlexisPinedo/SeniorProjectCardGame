using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class Vicky : InteractableHero
{
    protected override void HeroPowerEffect()
    {
        if (isInteractable)
        {
            isInteractable = false;
            
            Debug.Log("Swapping currency and power");
            
            Player currentPlayer = TurnManager.Instance.turnPlayer;
            
            int temp = currentPlayer.Currency;

            currentPlayer.Currency = currentPlayer.Power;

            currentPlayer.Power = temp;
        }
        
    }
}
