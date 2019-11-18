using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Holds visual information specific to enemy cards. Extends CardDisplay.
/// Class will load in text, sprites, card art, and values into the card display
/// this loads depending on the card attached to it. 
/// </summary>
public abstract class EnemyCardDisplay<T> : CardDisplay where T : EnemyCard
{
    public T card;
    [SerializeField] private TextMeshPro healthText;
    [SerializeField] private TextMeshPro rewardText;

    //This method will load the display based on the information stored within the card
    protected override void LoadCardIntoDisplay()
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

    /// <summary>
    /// Removes all references to the Card's information from this display.
    /// </summary>
    protected override void OnDisable()
    {
        base.OnDisable();
        
        card = null;
        cardArtDisplay.sprite = null;
        nameText.text = null;
        cardEffectText.text = null;
        healthText.text = null;
        rewardText.text = null;
    }
}
