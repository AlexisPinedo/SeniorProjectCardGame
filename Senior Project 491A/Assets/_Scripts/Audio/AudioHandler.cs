using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private AudioClipContainer container;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void PlayBuySound(PlayerCardDisplay cardBought)
    {
        audioSource.clip = container.ShopBuyingClip;
        audioSource.Play();
    }

    void PlayStartSound()
    {
        audioSource.clip = container.StartGameClip;
        audioSource.Play();
    }

    void PlayEndSound()
    {
        audioSource.clip = container.EndGameClip;
        audioSource.Play();
    }
    

    private void OnEnable()
    {
        PlayerCardDisplay.CardPurchased += PlayBuySound;
        UIHandler.StartBattleClicked += PlayStartSound;
        UIHandler.EndTurnClicked += PlayEndSound;
    }

    private void OnDisable()
    {
        PlayerCardDisplay.CardPurchased -= PlayBuySound;
        UIHandler.StartBattleClicked -= PlayStartSound;
        UIHandler.EndTurnClicked -= PlayEndSound;
    }
}