using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effect/Player Card Effect/Add Power and Currency")]
public class AddPowerCurrency : OnPlayEffects
{
    //Select how much power you want to add. If you want different amounts for other card effects
    //You need to create a new scriptable object to reference. 
    [SerializeField] private int powerAdditionAmount;
    [SerializeField] private int currencyAdditionAmount;


    //This method will add the powerAdditionAmount when called
    void AddPowerByEffect()
    {
        //Debug.Log("Adding Power via card effect");
        TurnPlayerManager.Instance.TurnPlayer.Power += powerAdditionAmount;
        TurnPlayerManager.Instance.TurnPlayer.Currency += currencyAdditionAmount;
    }

    /// <summary>
    /// Override LauchCardEffect so it will add power when called
    /// </summary>
    public override void LaunchCardEffect()
    {
        AddPowerByEffect();
        base.LaunchCardEffect();
    }}
