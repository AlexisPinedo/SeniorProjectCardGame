using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    public Text playerPower;
    public Text playerCurrency;

    private void OnEnable()
    {
        Player.CurrencyChanged += UpdateCurrency;
        Player.PowerChanged += UpdatePower;
    }

    private void OnDisable()
    {
        Player.CurrencyChanged -= UpdateCurrency;
        Player.PowerChanged -= UpdatePower;
    }

    public void ResetPower()
    {
        playerPower.text = "Power: + " + 0;
    }

    public void ResetCurrency()
    {
        playerCurrency.text = "Currency: + " + 0;
    }

    private void UpdatePower(int value)
    {
        playerPower.text = "Power: + " + value;
    }

    private void UpdateCurrency(int value)
    {
        playerCurrency.text = "Currency: + " + value;

    }
}
