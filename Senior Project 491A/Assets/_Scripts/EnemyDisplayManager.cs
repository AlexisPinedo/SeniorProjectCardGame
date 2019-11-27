using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisplayManager : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyDisplay;

    private void Awake()
    {
        MoveEnemiesUp();
    }

    private void OnEnable()
    {
        UIHandler.StartBattleClicked += MoveEnemiesDown;
        UIHandler.EndTurnClicked += MoveEnemiesUp;
    }

    private void OnDisable()
    {
        UIHandler.StartBattleClicked -= MoveEnemiesDown;
        UIHandler.EndTurnClicked -= MoveEnemiesUp;
    }

    public void MoveEnemiesUp()
    {
        Debug.Log("moving enemies up");
        enemyDisplay.transform.position = new Vector3(0, 40, 0);
    }

    public void MoveEnemiesDown()
    {
        Debug.Log("moving enemies down");
        enemyDisplay.transform.position = new Vector3(0, 0, 0);
    }


}