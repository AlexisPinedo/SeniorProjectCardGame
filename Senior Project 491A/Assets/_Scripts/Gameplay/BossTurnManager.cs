using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurnManager : MonoBehaviour
{
    private bool inBossPhase = false;

    public static event Action BossTurnEnded;

    private void OnEnable()
    {
        TurnPhaseManager.PlayerTurnEnded += StartBossPhase;
    }
    
    private void OnDisable()
    {
        TurnPhaseManager.PlayerTurnEnded -= StartBossPhase;
    }

    private void StartBossPhase()
    {
        StartCoroutine(BossPhase());
    }

    IEnumerator BossPhase()
    {
        //Debug.Log("in boss phase");
        inBossPhase = true;

        FieldContainer.Instance.DisplayACard();
        
        ShopDisplayManager.Instance.MoveShopUp();
        
        yield return new WaitForSeconds(3);

        ShopDisplayManager.Instance.MoveShopDown();
        
        //Debug.Log("boss phase ended");
        BossTurnEnded?.Invoke();
    }
}
