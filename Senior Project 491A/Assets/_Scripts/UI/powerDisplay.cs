using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerDisplay : MonoBehaviour
{
    private int power = 0;
    public Text powerStatusText;

    // Update is called once per frame
    void Update()
    {
        powerStatusText.text = "POWER: " + power;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            power += 100;
        }
    }
}
