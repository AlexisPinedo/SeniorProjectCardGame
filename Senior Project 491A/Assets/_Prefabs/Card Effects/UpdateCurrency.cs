using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCurrency : CardEffect
{
    private int _currentCurrency;

    public int CurrentCurrency
    {
        get { return _currentCurrency; }
        set { _currentCurrency = value; }
    }
}
