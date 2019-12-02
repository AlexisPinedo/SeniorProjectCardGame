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

    public static event Action HeroChanged;

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
        TurnPlayerManager.PlayerSwitched += UpdateActiveHero;
    }

    private void OnDisable()
    {
        TurnPlayerManager.PlayerSwitched -= UpdateActiveHero;
    }

    private void UpdateActiveHero()
    {
        activeTurnHero = TurnPlayerManager.Instance.TurnPlayer.SelectedHero;
        HeroChanged?.Invoke();
    }
}
