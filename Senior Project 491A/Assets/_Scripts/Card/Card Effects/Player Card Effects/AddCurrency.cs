using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used as a template for card effects that deal with adding currency
/// To create: In _Scriptable Objects create a Card Effect/Add Currency in the appropriate folder.
/// NOTE: You can reference the currently existing scriptable object if desired. 
/// Attach to card.
/// </summary>
[CreateAssetMenu(menuName = "Card Effect/Player Card Effect/Add Currency")]
public class AddCurrency : OnPlayEffects
{
    //Select how much currency you want to add. If you want different amounts for other card effects
    //You need to create a new scriptable object to reference. 
    [SerializeField] private int currencyAdditionAmount;
    
    //This method will add the currencyAdditionAmount when called
    void AddCurrencyByEffect()
    {
        Debug.Log("Adding currency from effect");
        TurnPlayerManager.Instance.TurnPlayer.Currency += currencyAdditionAmount;
    }

    /// <summary>
    /// Override LauchCardEffect so it will add currency when called
    /// </summary>
    public override void LaunchCardEffect()
    {
        AddCurrencyByEffect();
    }
}
