using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurnManager : MonoBehaviour
{
    private bool inBossTurn = false;
    
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
        
        //PurchaseHandler.Instance.gameObject.transform.position = new Vector3(0f, 20f, 0f);
        
        
        yield return new WaitForSeconds(1);
        
        //PurchaseHandler.Instance.gameObject.transform.position -= new Vector3(0f, 20f, 0f);


    }
}
