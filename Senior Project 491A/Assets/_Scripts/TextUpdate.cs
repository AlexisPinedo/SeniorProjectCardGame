using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    [SerializeField] private Player currency;
    [SerializeField] private Player power;

    public Text playerPower;
    public Text playerCurrency;

    public TurnManager turnManager;
    //do the samething for power

    // Start is called before the first frame update
    void Start()
    {
        //UpdatePowerDisplay();
        //UpdateCurrencyDisplay();
    }

    private void UpdatePowerDisplay()
    {
        playerPower.text = "POWER: " + turnManager.turnPlayer.GetPower();
    }

    private void UpdateCurrencyDisplay()
    {
        playerCurrency.text = "CURRENCY: " + turnManager.turnPlayer.GetCurrency();
    }
}
