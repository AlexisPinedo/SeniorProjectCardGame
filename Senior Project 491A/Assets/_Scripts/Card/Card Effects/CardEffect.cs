using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
/// <summary>
/// This is a card effect scriptable object. This will act as a foundation for all card effects that get created.
/// We will need to create each card effect individually then reference each one when needed. Is there a better way?
/// How to use: Create an object that inherits CardEffect. Override LaunchCardEffect with your desired effect logic.
/// Use [[CreateAssetMenu(menuName = "Card Effect/<Effect Name>")]] to create an instance of your effect.
/// In _Scriptable Objects create a Card Effect/<Effect Name> in the appropriate folder.
/// Attach to card.
/// Comment [CreateAssetMenu(menuName = "Card Effect/<Effect Name>")] so you cannot create more instances if needed. 
/// </summary>
public abstract class CardEffect : ScriptableObject
{
    public Card owner;

    /// <summary>
    /// This method is used among all children as the method to call to trigger each card effect
    /// </summary>
    public virtual void LaunchCardEffect()
    {
        
    }
    
    public virtual void CardPlacedIntoPlay()
    {
        
    }

    public virtual void CardRemovedFromPlay()
    {
        
    }
}
