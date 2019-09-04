using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PlayerCardContainer : MonoBehaviour
{
    public PlayerCard card;
    [SerializeField]
    private SpriteRenderer cardArtDisplay;
    [SerializeField]
    private TextMeshPro attackText;
    [SerializeField]
    private TextMeshPro costText;
    [SerializeField]
    private TextMeshPro currencyText;
    [SerializeField]
    private SpriteRenderer typeIcon;
    [SerializeField]
    private SpriteRenderer cardBorder;
    [SerializeField]
    private SpriteRenderer cardEffectTextBox;
    [SerializeField]
    private SpriteRenderer cardNameTextBox;
    [SerializeField]
    private TextMeshPro nameText;
    [SerializeField]
    private TextMeshPro cardEffectText;
    [SerializeField]
    private SpriteRenderer cardEffectCostsIcons;
    [SerializeField]
    private SpriteRenderer costIcon;

    [SerializeField]
    private List<GameObject> cardIcons = new List<GameObject>();

    private void Awake()
    {
        LoadCardIntoContainer();
    }

    private void OnDestroy()
    {
        ClearCardFromContainer();
    }

    private void OnEnable()
    {
        

        LoadCardIntoContainer();
    }

    private void OnDisable()
    {

        ClearCardFromContainer();
    }

    private void LoadCardIntoContainer()
    {
        if(card == null)
            return;
        
        cardArtDisplay.sprite = card.CardArtwork;
        attackText.text = card.CardAttack.ToString();
        costText.text = card.CardCost.ToString();
        currencyText.text = card.CardCurrency.ToString();
        typeIcon.sprite = card.CardTypeArt;
        cardBorder.sprite = card.BorderArt;
        cardEffectTextBox.sprite = card.CardEffectBoxArt;
        cardNameTextBox.sprite = card.NameBoxArt;
        nameText.text = card.CardName;
        cardEffectText.text = card.CardEffectDisplay;
        LoadCostEffectIcons();

        if (this.transform.parent.gameObject.GetComponent<HandContainer>() != null)
        {
            costIcon.gameObject.SetActive(false);
        }

    }

    private void ClearCardFromContainer()
    {
        card = null;
        cardArtDisplay.sprite = null;
        attackText.text = null;
        costText.text = null;
        currencyText.text = null;
        typeIcon.sprite = null;
        cardBorder.sprite = null;
        cardEffectTextBox.sprite = null;
        cardNameTextBox.sprite = null;
        nameText.text = null;
        cardEffectText.text = null;
        RemoveCardEffectCostIcons();
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
