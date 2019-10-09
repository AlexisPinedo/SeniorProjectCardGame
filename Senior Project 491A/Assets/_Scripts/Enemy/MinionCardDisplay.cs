using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds visual information specific to minion cards. Extends EnemyCardDisplay.
/// Class will load in text, sprites, card art, and values into the card display
/// this loads depending on the card attached to it. 
/// </summary>
//[ExecuteInEditMode]
public class MinionCardDisplay : EnemyCardDisplay
{
    //Delegate event to handle anything that cares if the card has been destroyed
    public delegate void _cardDestroyed(EnemyCardDisplay destroytedCard);
    public static event _cardDestroyed CardDestroyed;
    
    //Delegate event to handle anything that cares if the card has been clicked
    public delegate void _MinionCardClicked(MinionCardDisplay cardClicked);

    public static event _MinionCardClicked MinionCardClicked;
    
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    //This method will invoke the CardDestroyed event 
    protected override void OnDestroy()
    {
        Debug.Log("minion card was destroyed");
        base.OnDestroy();
        if(CardDestroyed != null)
            CardDestroyed.Invoke(this);
    }

    //This method will invoke the MinionCardClicked event
    protected override void OnMouseDown()
    {
        MinionCardClicked?.Invoke(this);
    }
}
