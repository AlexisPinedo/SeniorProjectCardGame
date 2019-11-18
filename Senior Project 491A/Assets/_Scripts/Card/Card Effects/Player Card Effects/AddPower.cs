using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used as a template for card effects that deal with adding power
/// To create: In _Scriptable Objects create a Card Effect/Add Power in the appropriate folder.
/// NOTE: You can reference the currently existing scriptable object if desired. 
/// Attach to card.
/// </summary>.

//This will enable the ability to create scriptable objects in the Project tab
[CreateAssetMenu(menuName = "Card Effect/Player Card Effect/Add Power")]
public class AddPower : OnPlayEffects
{
    //Select how much power you want to add. If you want different amounts for other card effects
    //You need to create a new scriptable object to reference. 
    [SerializeField] private int powerAdditionAmount;

    //This method will add the powerAdditionAmount when called
    void AddPowerByEffect()
    {
        //Debug.Log("Adding Power via card effect");
        TurnPlayerManager.Instance.TurnPlayer.Power += powerAdditionAmount;
    }

    /// <summary>
    /// Override LauchCardEffect so it will add power when called
    /// </summary>
    public override void LaunchCardEffect()
    {
        AddPowerByEffect();
        base.LaunchCardEffect();
    }
}
