using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class Vicky : InteractableHero
{ 
    private void OnEnable()
    {
        _heroPowerMessageDisplay = "Once per turn you can switch your currency and power";
    }

    protected override void HeroPowerEffect()
    {
        if (isInteractable && NetworkOwnershipTransferManger.currentPhotonPlayer.IsLocal)
        {
            isInteractable = false;

            NotificationWindowEvent.Instance.EnableNotificationWindow("Swapped currency and power");

            Player currentPlayer = TurnPlayerManager.Instance.TurnPlayer;
            
            int temp = currentPlayer.Currency;

            currentPlayer.Currency = currentPlayer.Power;

            currentPlayer.Power = temp;
        }

        if (!NetworkOwnershipTransferManger.currentPhotonPlayer.IsLocal)
        {

            Player currentPlayer = TurnPlayerManager.Instance.TurnPlayer;

            int temp = currentPlayer.Currency;

            currentPlayer.Currency = currentPlayer.Power;

            currentPlayer.Power = temp;
        }
        
    }
}
