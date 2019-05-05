using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currencyDisplay : MonoBehaviour
{
    private int currency;
    public Text currencyStatusText;

    private Player player;

    public void Start()
    {
        currency = 0;
        UpdateCurrencyDisplay();
    }

    void Awake()
    {
        player = FindObjectOfType<TurnManager>().turnPlayer;
        currencyStatusText.text = "CURRENCY: " + player.GetCurrency();
    }

    public void UpdateCurrencyDisplay()
    {
        player = FindObjectOfType<TurnManager>().turnPlayer;
        currencyStatusText.text = "CURRENCY: " + player.GetCurrency();
    }
}
