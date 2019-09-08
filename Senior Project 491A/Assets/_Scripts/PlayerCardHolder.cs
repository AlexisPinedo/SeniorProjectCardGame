using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PlayerCardHolder : CardHolder
{
    public PlayerCard card;
    [SerializeField]
    private TextMeshPro attackText;
    [SerializeField]
    private TextMeshPro costText;
    [SerializeField]
    private TextMeshPro currencyText;
    [SerializeField]
    private SpriteRenderer cardEffectCostsIcons;
    [SerializeField]
    private SpriteRenderer costIcon;
    

    [SerializeField]
    private List<GameObject> cardIcons = new List<GameObject>();

    protected override void LoadCardIntoContainer()
    {
        cardArtDisplay.sprite = card.CardArtwork;
        typeIcon.sprite = card.CardTypeArt;
        cardBorder.sprite = card.BorderArt;
        cardEffectTextBox.sprite = card.CardEffectBoxArt;
        cardNameTextBox.sprite = card.NameBoxArt;
        nameText.text = card.CardName;
        cardEffectText.text = card.CardEffectDisplay;
        attackText.text = card.CardAttack.ToString();
        costText.text = card.CardCost.ToString();
        currencyText.text = card.CardCurrency.ToString();

        LoadCostEffectIcons();
    }

    protected override void ClearCardFromContainer()
    {
        card = null;
        cardArtDisplay.sprite = null;
        typeIcon.sprite = null;
        cardBorder.sprite = null;
        cardEffectTextBox.sprite = null;
        cardNameTextBox.sprite = null;
        nameText.text = null;
        cardEffectText.text = null;
        attackText.text = null;
        costText.text = null;
        currencyText.text = null;
        RemoveCardEffectCostIcons();
        if (this.transform.parent.gameObject.GetComponent<HandContainer>() != null)
        {
            costIcon.gameObject.SetActive(false);
        }
    }

    private void LoadCostEffectIcons()
    {
        Vector3 spawnPoint = new Vector3(-.55f, .23f, 0f);
        foreach (var cardIconSprite in card.cardCostsIcons)
        {
            SpriteRenderer cardIcon = Instantiate(cardEffectCostsIcons, cardEffectTextBox.transform.position, Quaternion.identity, cardEffectTextBox.transform);
            cardIcons.Add(cardIcon.gameObject);
            cardIcon.transform.position += spawnPoint;
            spawnPoint += new Vector3(.10f, 0f, 0f);
            cardIcon.sprite = cardIconSprite;
        }
    }

    private void RemoveCardEffectCostIcons()
    {
        //Debug.Log(cardIcons.Count);
        for (int i = 0; i < cardIcons.Count; i++)
        {
            
            DestroyImmediate(cardIcons[i].gameObject);
            //Debug.Log("Object Destroyed " + i);
        }

        cardIcons.Clear();
    }


}
