using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using UnityEngine;

//[CreateAssetMenu]
public class Veda : CostRequirementHero
{
    private int cardSelectionLimit = 1;

    protected override void OnEnable()
    {
        base.OnEnable();
        _heroPowerMessageDisplay = "If you play 3 cards of the same color: Both players add 1 card from the shop";

    }

    protected override void HeroPowerEffect()
    {
;
        //Debug.Log("Showing turn player notification");
        NotificationWindowEvent.Instance.EnableNotificationWindow("You may select 1 card");

        //Debug.Log("Enabling Shop selection state");
        FreeShopSelectionEvent.Instance.EnableShopSelectionState(cardSelectionLimit);

        //Debug.Log("Changing Active Player");
        PlayerSwitchEvent.Instance.EnablePlayerSwitchEvent();
        
        //Debug.Log("Showing next Notification");
        NotificationWindowEvent.Instance.EnableNotificationWindow(NetworkOwnershipTransferManger.pendingPhotonPlayer.NickName + " is selecting 1 card");

        //Debug.Log("Swap to next shop");
        FreeShopSelectionEvent.Instance.EnableShopSelectionState(cardSelectionLimit);

        PlayerSwitchEvent.Instance.EnablePlayerSwitchEvent();

    }
}
