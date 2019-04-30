using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // Player References
    public GameObject player1;
    public GameObject player2;

    private bool isPlayerOnesTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerOnesTurn = true;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerOnesTurn()
    {
        if (!isPlayerOnesTurn)
        {
            isPlayerOnesTurn = true;
        }
    }

    public void SetPlayerTwosTurn()
    {
        if (isPlayerOnesTurn)
        {
            isPlayerOnesTurn = !isPlayerOnesTurn;
        }
    }
    
    public bool IsPlayerTwosTurn()
    {
        return !isPlayerOnesTurn;
    }

    public bool IsPlayerOnesTurn()
    {
        return isPlayerOnesTurn;
    }
}
