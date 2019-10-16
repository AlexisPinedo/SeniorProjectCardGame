﻿using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using UnityEngine;

[CreateAssetMenu]
public class Veda : CostRequirementHero
{
    private int cardSelectionLimit = 1;

    protected override void HeroPowerEffect()
    {
        //Debug.Log("Showing turn player notification");
        NotificationWindowEvent.Instance.EnableNotificationWindow("Turn Player Select 1 card");
        
        //Debug.Log("Enabling Shop selection state");
        FreeShopSelectionEvent.Instance.EnableShopSelectionState(cardSelectionLimit);
        
        //Debug.Log("Changing Active Player");
        //TurnManager.Instance.QuickChangeActivePlayer();
        
        //Debug.Log("Showing next Notification");
        NotificationWindowEvent.Instance.EnableNotificationWindow("Other Player may Select 1 card");
        
        //Debug.Log("Swap to next shop");
        FreeShopSelectionEvent.Instance.EnableShopSelectionState(cardSelectionLimit);
        
    }
}