using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerDisplay : MonoBehaviour
{
    private int power;
    public Text powerStatusText;

    public void Start()
    {
        power = 0;
        UpdatePower();
    }

    public void AddPower(int newPowerValue)
    {
        power += newPowerValue;
        UpdatePower();
    }

    public void UpdatePower()
    {
        powerStatusText.text = "Power: " + power;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddPower(100);
        }
    }
}
