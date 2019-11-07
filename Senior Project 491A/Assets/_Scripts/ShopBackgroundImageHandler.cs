using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBackgroundImageHandler : MonoBehaviour
{
    private RectTransform thisRectTransform;

    
    private void Awake()
    {
        thisRectTransform = GetComponent<RectTransform>();
        //originalPosition = thisRectTransform.transform.position;
    }

    private void OnEnable()
    {
        BattleManager.BattleStarted += MoveBackgroundImageUp;
        BattleManager.BattleEnded += MoveBackgoundImageDown;

    }

    private void OnDisable()
    {
        BattleManager.BattleStarted -= MoveBackgroundImageUp;
        BattleManager.BattleEnded -= MoveBackgoundImageDown;

    }

    private void MoveBackgroundImageUp()
    {
         
    }

    private void MoveBackgoundImageDown()
    {

    }
}
