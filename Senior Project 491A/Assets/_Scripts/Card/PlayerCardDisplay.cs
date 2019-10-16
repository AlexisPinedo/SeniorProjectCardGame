using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

/// <summary>
/// Holds visual information specific to PlayerCards. Extends CardDisplay.
/// Class will load in text, sprites, card art, and values into the card display
/// this loads depending on the card attached to it. 
/// </summary>
//[ExecuteInEditMode]
public class PlayerCardDisplay : CardDisplay
{
    public PlayerCard card;

    [SerializeField] private TextMeshPro attackText;
    [SerializeField] private TextMeshPro costText;
    [SerializeField] private TextMeshPro currencyText;
    [SerializeField] private SpriteRenderer cardEffectCostsIcons;
    [SerializeField] private SpriteRenderer costIcon;
    [SerializeField] private List<GameObject> cardIcons = new List<GameObject>();
    
    public delegate void _CardPurchased(PlayerCardDisplay cardBought);

    public static event _CardPurchased CardPurchased;


    //When the PlayerCardDisplay is loaded we want to load in the components into the display
//    protected override void Awake()
//    {
//        LoadCardIntoDisplay();
//    }

    /// <summary>
    /// Called when this object is enabled. Adds EventReceived to the Networking Client.
    /// </summary>
    //[ExecuteInEditMode]
    protected override void OnEnable()
    {
        base.OnEnable();
        ShopSelectionEventListener.PurchaseEventTriggered += DisablePlayerCardCollider;
        ShopSelectionEventListener.PurchaseEventEnded += EnablePlayerCardCollider;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ShopSelectionEventListener.PurchaseEventTriggered -= DisablePlayerCardCollider;
        ShopSelectionEventListener.PurchaseEventEnded -= EnablePlayerCardCollider;
    }

    /// <summary>
    /// Called when this object is disabled. Removes EventReceived from the Networking Client.
    /// </summary>
    //[ExecuteInEditMode]
//    protected override void OnDisable()
//    {
//        ClearCardFromDisplay();
//    }

    //THis method will load the display based on the information stored within the card
    protected override void LoadCardIntoDisplay()
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

    /// <summary>
    /// Removes all references to the Card's information from this display.
    /// </summary>
    protected override void ClearCardFromDisplay()
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
    
    /// <summary>
    /// this method will look at sprite list of required costs icons and place them appropriately into the scene
    /// 
    /// </summary>
    private void LoadCostEffectIcons()
    {
        // Initial spawn point that changes with each card added
        Vector3 spawnPoint = new Vector3(-.55f, .23f, 0f);

        foreach (var cardIconSprite in card.cardCostsIcons)
        {
            SpriteRenderer cardIcon = Instantiate(cardEffectCostsIcons, cardEffectTextBox.transform.position, Quaternion.identity, cardEffectTextBox.transform);
            cardIcon.sortingLayerName = "Player Card" ;
            cardIcons.Add(cardIcon.gameObject);
            cardIcon.transform.position += spawnPoint;
            spawnPoint += new Vector3(.10f, 0f, 0f);
            cardIcon.sprite = cardIconSprite;
        }
    }

    /// <summary>
    /// This method will remove the icons from the display
    /// </summary>
    private void RemoveCardEffectCostIcons()
    {
        // Debug.Log(cardIcons.Count);
        for (int i = 0; i < cardIcons.Count; i++)
        {
            DestroyImmediate(cardIcons[i].gameObject);
            // Debug.Log("Object Destroyed " + i);
        }

        cardIcons.Clear();
    }

    private void DisablePlayerCardCollider()
    {
        if (this.gameObject.GetComponentInParent<HandContainer>() != null)
        {
            //Debug.Log("Disabling player card collider for" + nameText.text);

            cardDisplayCollider.enabled = false;
        }
    }
    
    private void EnablePlayerCardCollider()
    {

        if (this.gameObject.GetComponentInParent<HandContainer>() != null)
        {
            //Debug.Log("Enabling player card collider for " + nameText.text);

            cardDisplayCollider.enabled = true;
        }
    }

    public void TriggerCardPurchasedEvent()
    {
        CardPurchased?.Invoke(this);

    }
}
