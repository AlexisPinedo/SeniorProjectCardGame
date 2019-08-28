/*
    Created by: David Taitingfong
    Date:       2019-04-11
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A Boss card represents ...well... the boss of a level.
/// </summary>
public class Boss : EnemyCard
{
    /// <summary>
    /// The Boss' health. Can't be lowered, only obliterated!
    /// </summary>
    private readonly int _health;

    /// <summary>
    /// What the Boss needs to have happen in the game in order for him/her/it to win!
    /// </summary>
    public Goal goal;

    /// <summary>
    /// The Boss' deck of cards (they are indeed different than yours).
    /// </summary>
    public EnemyDeck bossDeck;

    /// <summary>
    /// A reference to the Enemy's grid.
    /// </summary>
    public CreateGrid EnemyGrid;

    public BossTurnCardPlayer cardPlayer;

    void Start()
    {
        // TODO: Set the goal?
        // TODO: Populate the Boss' deck?

        this.transform.position = EnemyGrid.GetNearestPointOnGrid(new Vector2(6, 2));
    }

    public override void OnMouseDown()
    {
        if (cardPlayer.filledCardZones == 0)
        {
            Debug.Log("Player can attack the Boss");
            if (TurnManager.turnPlayer.GetPower() >= this._health)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("Player lacks the power to defeat the Boss!");
            }

        }
    }
}
