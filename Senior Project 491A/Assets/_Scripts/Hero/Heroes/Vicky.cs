﻿using System;
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
        if (isInteractable)
        {
            isInteractable = false;
            
            NotificationWindowEvent.Instance.EnableNotificationWindow("Swapping currency and power");
            
            Player currentPlayer = TurnManager.Instance.turnPlayer;
            
            int temp = currentPlayer.Currency;

            currentPlayer.Currency = currentPlayer.Power;

            currentPlayer.Power = temp;
        }
        
    }
}
