using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSelectionEventListener : MonoBehaviour
{
    public delegate void _purchaseEventTriggered();

    public static event _purchaseEventTriggered PurchaseEventTriggered;

    public delegate void _purchaseEventEnded();

    public static event _purchaseEventEnded PurchaseEventEnded;

    private static ShopSelectionEventListener _instance;

    public static ShopSelectionEventListener Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance == null && _instance != this)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void EnableShopSelectionState()
    {
        StartCoroutine(SelectionState());
    }


    IEnumerator SelectionState()
    {
        PurchaseEventTriggered?.Invoke();
        Debug.Log("Purchase event triggered");

        yield return new WaitForSeconds(10);
        
        Debug.Log("Purchase event ended");

        PurchaseEventEnded.Invoke();
    }
}
