using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds visual information specific to boss cards. Extends EnemyCardDisplay.
/// Class will load in text, sprites, card art, and values into the card display
/// this loads depending on the card attached to it. 
/// </summary>
//[ExecuteInEditMode]
public class BossCardDisplay : EnemyCardDisplay
{
    //We create a delegate event for the boss card to handle what happens when clicked
    public delegate void _BossCardClicked(BossCardDisplay cardClicked);

    public static event _BossCardClicked BossCardClicked;
    
    protected override void Awake()
    {
        //base.Awake();
        this.enabled = true;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    //this handles the boss card being clicked 
    protected override void OnMouseDown()
    {
        Debug.Log("boss has been clicked");
        BossCardClicked?.Invoke(this);
    }

}
