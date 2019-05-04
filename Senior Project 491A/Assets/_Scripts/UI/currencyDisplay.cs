using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currencyDisplay : MonoBehaviour
{
    //private int currency = 0;
    public Text currencyStatusText;

    private Player player;

    void Awake()
    {
        player = FindObjectOfType<TurnManager>().turnPlayer;
        currencyStatusText.text = "CURRENCY: " + player.GetCurrency();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCurrencyDisplay()
    {
        player = FindObjectOfType<TurnManager>().turnPlayer;
        currencyStatusText.text = "CURRENCY: " + player.GetCurrency();
    }
}
