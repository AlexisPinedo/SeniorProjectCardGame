using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventHandler : MonoBehaviour
{

    public delegate void _StartBattle();

    public static event _StartBattle StartBattle;

    public delegate void _EndTurn();

    public static event _EndTurn EndTurn;

    public void StartBattleButtonClicked()
    {
        if(StartBattle != null)
            StartBattle.Invoke();
    }

    public void EndTurnButtonClicked()
    {
        if(EndTurn != null)
            EndTurn.Invoke();
    }
    
}
