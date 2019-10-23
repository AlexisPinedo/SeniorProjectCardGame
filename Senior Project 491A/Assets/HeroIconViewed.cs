using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroIconViewed : MonoBehaviour
{
    private HeroIconViewed display;

    [SerializeField]
    private TextMeshProUGUI heroPowerText;

    private void Awake()
    {
        display = this;
        display.gameObject.SetActive(false);

        heroPowerText = GetComponentInChildren<TextMeshProUGUI>();
        
        //heroPowerText.text = "";

    }

    private void OnEnable()
    {
        TurnPlayerHeroManager.HeroChanged += SetHeroPowerText;
    }

    private void OnDisable()
    {
        TurnPlayerHeroManager.HeroChanged -= SetHeroPowerText;
    }

    private void Start()
    {
    }

    public void ActivateDisplay()
    {
        display.gameObject.SetActive(true);
    }

    public void DecactivateDisplay()
    {
        display.gameObject.SetActive(false);
    }

    private void SetHeroPowerText()
    {
        heroPowerText.text = TurnPlayerHeroManager.Instance.ActiveTurnHero.HeroPowerMessageDisplay;
    }
}
