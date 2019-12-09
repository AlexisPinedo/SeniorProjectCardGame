using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class HeroIconViewed : MonoBehaviourPun
{
    private HeroIconViewed display;

    [SerializeField]
    private TextMeshProUGUI heroPowerText;

    [SerializeField] private Image dispayBackground;

    private void Awake()
    {
        display = this;
        
        //display.gameObject.SetActive(false);

        heroPowerText = GetComponentInChildren<TextMeshProUGUI>();

        dispayBackground = GetComponentInChildren<Image>();

        DecactivateDisplay();
    }

    private void OnEnable()
    {
        TurnPlayerHeroManager.HeroChanged += SetHeroPowerText;

        if(PhotonNetwork.OfflineMode)
            TurnPhaseManager.PlayerTurnStarted += SetHeroPowerText;
    }

    private void OnDisable()
    {
        TurnPlayerHeroManager.HeroChanged -= SetHeroPowerText;

        if (PhotonNetwork.OfflineMode)
            TurnPhaseManager.PlayerTurnStarted -= SetHeroPowerText;
    }

    public void ActivateDisplay()
    {
        dispayBackground.enabled = true;
        heroPowerText.enabled = true;
    }

    public void DecactivateDisplay()
    {
        dispayBackground.enabled = false;
        heroPowerText.enabled = false;
    }

    public void TriggerInteractableHeroEffect()
    {
        if (TurnPlayerHeroManager.Instance.ActiveTurnHero is InteractableHero)
        {
            InteractableHero hero = (InteractableHero)TurnPlayerHeroManager.Instance.ActiveTurnHero;
            hero.TriggerHeroPower();
        }
    }

    private void SetHeroPowerText()
    {
        Hero turnPlayerHero = TurnPlayerHeroManager.Instance.ActiveTurnHero;
        heroPowerText.text = turnPlayerHero.HeroPowerMessageDisplay;

        if (turnPlayerHero is InteractableHero)
        {
            InteractableHero hero = (InteractableHero) turnPlayerHero;
            if (NetworkOwnershipTransferManger.currentPhotonPlayer.IsLocal)
                hero.SetIsInteractableTrue();
        }
    }
}
