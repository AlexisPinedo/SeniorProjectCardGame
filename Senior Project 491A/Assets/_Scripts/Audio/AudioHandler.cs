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

    void playBySound(PlayerCardDisplay cardBought)
    {
        audioSource.clip = container.ShopBuyingClip;
        audioSource.Play();
    }
    

    private void OnEnable()
    {
        PlayerCardDisplay.CardPurchased += playBySound;
    }

    private void OnDisable()
    {
        PlayerCardDisplay.CardPurchased -= playBySound;
    }
}
