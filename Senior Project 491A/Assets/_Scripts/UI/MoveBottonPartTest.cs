using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBottonPartTest : MonoBehaviour
{
    [SerializeField]
    private RectTransform image;

    private void OnEnable()
    {
        TurnPhaseManager.BattlePhaseStarted += MoveImageDown;
        TurnPhaseManager.BattlePhaseEnded += MoveImageUp;
    }

    private void OnDisable()
    {
        TurnPhaseManager.BattlePhaseStarted -= MoveImageDown;
        TurnPhaseManager.BattlePhaseEnded -= MoveImageUp;
    }

    void MoveImageDown()
     {
         image.anchoredPosition = new Vector2(0, -1000);
     }

    void MoveImageUp()
    {
        image.anchoredPosition = new Vector2(0, 0);
    }
}
