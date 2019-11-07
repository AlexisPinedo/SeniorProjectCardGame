using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurnManager : MonoBehaviour
{
    private bool inBossTurn = false;

    public static event Action BossTurnEnded;

    private void OnEnable()
    {
        UIHandler.EndTurnClicked += StartBossTurn;
    }
    
    private void OnDisable()
    {
        UIHandler.EndTurnClicked -= StartBossTurn;
    }

    private void StartBossTurn()
    {
        StartCoroutine(BossTurn());
    }

    IEnumerator BossTurn()
    {
        inBossTurn = true;

        FieldContainer.Instance.DisplayACard();
        
        ShopDisplayManager.Instance.MoveShopUp();
        //PurchaseHandler.Instance.gameObject.transform.position = new Vector3(0f, 20f, 0f);
        
        yield return new WaitForSeconds(1);

        //PurchaseHandler.Instance.gameObject.transform.position -= new Vector3(0f, 20f, 0f);
        ShopDisplayManager.Instance.MoveShopDown();
        BossTurnEnded();
    }
}
