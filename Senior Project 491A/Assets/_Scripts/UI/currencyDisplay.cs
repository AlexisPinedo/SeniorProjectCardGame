using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currencyDisplay : MonoBehaviour
{
    private int currency = 0;
    public Text currencyStatusText;

    // Update is called once per frame
    void Update()
    {
        currencyStatusText.text = "CURRENCY: " + currency;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currency+=5;
        }
    }
}
