using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class TurnPlayerHeroManager : MonoBehaviour
{
    [SerializeField]
    private Hero activeTurnHero;

    public Hero ActiveTurnHero
    {
        get => activeTurnHero;
    }

    private static TurnPlayerHeroManager _instance;

    public delegate void _heroChanged();

    public static event _heroChanged HeroChanged;

    public static TurnPlayerHeroManager Instance
    {
        get => _instance;
    }

    private void Awake()
    {
        if (_instance != this && _instance != null)
            Destroy(this.gameObject);
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        UpdateActiveHero();
    }

    private void OnEnable()
    {
        TurnManager.PlayerSwitched += UpdateActiveHero;
        TurnManager.EffectSwitchedPlayer += UpdateActiveHero;
    }

    private void OnDisable()
    {
        TurnManager.PlayerSwitched -= UpdateActiveHero;
        TurnManager.EffectSwitchedPlayer -= UpdateActiveHero;
    }

    private void UpdateActiveHero()
    {
        activeTurnHero = TurnManager.Instance.turnPlayer.SelectedHero;
        HeroChanged?.Invoke();
    }
}
