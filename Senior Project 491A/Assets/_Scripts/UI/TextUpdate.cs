using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    public Text playerPower;
    public Text playerCurrency;

    private void Start()
    {
        UpdatePower();
        UpdateCurrency();
    }

    private void OnEnable()
    {
        PlayZone.CardPlayed += UpdateTextValues;
    }

    private void OnDisable()
    {
        PlayZone.CardPlayed -= UpdateTextValues;
    }

    public void UpdateTextValues()
    {
        UpdatePower();
        UpdateCurrency();
    }
    public void ResetPower()
    {
        playerPower.text = "Power: + " + 0;
    }

    public void ResetCurrency()
    {
        playerCurrency.text = "Currency: + " + 0;
    }

    private void UpdatePower()
    {
        playerPower.text = "Power: " + TurnManager.Instance.turnPlayer.Power;
    }

    private void UpdateCurrency()
    {
        playerCurrency.text = "Currency: " + TurnManager.Instance.turnPlayer.Currency;
    }
}
