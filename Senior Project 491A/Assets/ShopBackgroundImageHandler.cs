using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBackgroundImageHandler : MonoBehaviour
{
    private RectTransform thisRectTransform;

    private Vector3 originalPosition;
    
    private void Awake()
    {
        thisRectTransform = GetComponent<RectTransform>();
        originalPosition = thisRectTransform.transform.position;
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
         thisRectTransform.transform.position = new Vector3(originalPosition.x, originalPosition.y + 20, originalPosition.z);
    }

    private void MoveBackgoundImageDown()
    {
        thisRectTransform.transform.position = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z);

    }
}
