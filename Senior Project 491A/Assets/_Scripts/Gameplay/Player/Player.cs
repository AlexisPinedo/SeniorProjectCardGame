using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

/**
    Defines the PlayerGraveyard class, i.e, everything that the PlayerGraveyard can do and
    has access to during a match.
 */
[CreateAssetMenu(menuName = "Player Component/Player")]
public class Player : ScriptableObject
{
    [SerializeField] public Hand hand;
    [SerializeField] public PlayerDeck deck;

    public delegate void _CurrencyUpdated();
    public static event _CurrencyUpdated CurrencyUpdated;

    public delegate void _PowerUpdated();
    public static event _PowerUpdated PowerUpdated;

    public string GameName { get; set; }

    [SerializeField]
    private int currency;
    public int Currency
    {
        get => currency;
        set
        {
            currency = value;
            CurrencyUpdated?.Invoke();
        }
    }

    [SerializeField]
    private int power;
    public int Power
    {
        get => power;
        set
        {
            power = value;
            PowerUpdated?.Invoke();
        }
    }

    private void OnEnable()
    {
        power = 0;
        currency = 0;
    }

    public PlayerGraveyard playerGraveyard;
    [SerializeField] private List<PlayerCard> rewardPile;

    [SerializeField]
    private Hero _selectedHero;

    public Hero SelectedHero
    {
        get { return _selectedHero; }
        set { _selectedHero = value; }
    }

}
