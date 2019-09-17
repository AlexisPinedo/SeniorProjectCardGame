using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public abstract class EnemyCardHolder : CardHolder
{
    public delegate void _cardDestroyed(EnemyCardHolder destroytedCard);
    public static event _cardDestroyed CardDestroyed;

    public EnemyCard card;
    [SerializeField] private TextMeshPro healthText;
    [SerializeField] private TextMeshPro rewardText;

    protected override void LoadCardIntoContainer()
    {
        cardArtDisplay.sprite = card.CardArtwork;
        typeIcon.sprite = card.CardTypeArt;
        cardBorder.sprite = card.BorderArt;
        cardEffectTextBox.sprite = card.CardEffectBoxArt;
        cardNameTextBox.sprite = card.NameBoxArt;
        nameText.text = card.CardName;
        cardEffectText.text = card.CardEffectDisplay;
        healthText.text = card.HealthValue.ToString();
        rewardText.text = card.RewardValue.ToString();
    }

    protected override void OnDisable()
    {
        card = null;
        cardArtDisplay.sprite = null;
        nameText.text = null;
        cardEffectText.text = null;
        healthText.text = null;
        rewardText.text = null;
    }
}
