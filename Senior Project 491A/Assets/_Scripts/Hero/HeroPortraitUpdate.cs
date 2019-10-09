using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HeroPortraitUpdate : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer playerAvatar;

    private void Start()
    {
        //ChangeHeroPortrait();
    }

    private void OnEnable()
    {
        //Debug.Log("hero changed event subbed");
        TurnPlayerHeroManager.HeroChanged += ChangeHeroPortrait;
    }

    private void OnDisable()
    {
        TurnPlayerHeroManager.HeroChanged -= ChangeHeroPortrait;
    }

    private void ChangeHeroPortrait()
    {
        //Debug.Log("attempting to swap ");

        playerAvatar.sprite = TurnPlayerHeroManager.Instance.ActiveTurnHero.HeroPortrait;
    }
}
