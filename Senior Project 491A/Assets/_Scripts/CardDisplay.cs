using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] private Card card;

    [SerializeField] private Text nameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text effectText;

    [SerializeField] private Text costText;
    [SerializeField] private Text currencyText;
    [SerializeField] private Text attackText;

    [SerializeField] private Image artImage;
    [SerializeField] private Image elementImage;


    void Start()
    {
        nameText.text = card.cardName;
        descriptionText.text = card.cardDescription;
        effectText.text = card.cardEffect;

        costText.text = card.cardCost.ToString();
        currencyText.text = card.cardCurrency.ToString();
        attackText.text = card.cardAttack.ToString();

        artImage.sprite = card.cardArtwork;
        elementImage.sprite = card.cardElement;
    }
}
